using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("인벤토리")]
    public Inventory Inven;

    //사용 변수
    private int beforeIndex;
    private bool HitCheck;
    private Collider hit;

    //불러오기 변수
    private Slot S;// 클릭한 슬롯의 스크립트를 불러온 변수
    private Item SelectSlotItem;// 클릭한 슬롯의 아이템 값 저장 변수
    private EquipManager equipManager;// 창의 오브젝트를 불러오기 위한 변수
    public GameObject SelectSlot;// 선택한 슬롯을 옴기는 것 처럼 보이기 위한 슬롯
    public GameObject ItemTip;// 아이템 이름/설명 창
    public GameObject ItemEquipTip;// 아이템 사용 창

    void Start()
    {
        TipObjectLoad();
        SelectSlot.gameObject.GetComponent<Slot>().item = null;
    }

    private void TipObjectLoad()
    {
        equipManager = GameObject.FindWithTag("EquipInven").GetComponent<EquipManager>();
        ItemTip = equipManager.ItemTip;
        ItemEquipTip = equipManager.ItemEquipTip;
    }

    #region MouseEventSystems

    public void OnDrag(PointerEventData eventData)// 슬롯 드래그 마우스 좌표로 이동
    {
        if (eventData.pointerDrag.tag != "slot") return;

        GameObject.FindWithTag("MoveSlot").transform.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)// 슬롯 드래그시 드래그한 아이템 데이터 불러오기
    {
        if (eventData.pointerDrag.tag != "slot") return;

        ItemTipOnOff(0, false, ItemEquipTip);
        int Sindex = DragEventDataIndex(eventData);
        SelectSlotItem = Inven.items[Sindex];
        SelectSlotSeting(true, SelectSlotItem);

        beforeIndex = Sindex;
        SlotItemdataGet(beforeIndex, null);
    }

    public void OnEndDrag(PointerEventData eventData)// 슬롯 드래그 끝날 때 체킹
    {
        DropItemCheck(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)// 슬롯에 마우스를 대었을 때 아이템 설명 창 보이기
    {
        if (eventData.pointerEnter.tag != "slot") return;

        int Sindex = EnterEventDataIndex(eventData);
        if (Inven.items[Sindex] == null) return;

        ItemTipOnOff(Sindex, true, ItemTip);
        ItemTextSeting("NameText").text = Inven.items[Sindex].itemName;
        ItemTextSeting("TipText").text = Inven.items[Sindex].itemMethod;
    }

    public void OnPointerExit(PointerEventData eventData)// 마우스가 슬롯에 벗어나면 아이템 설명 창 숨기기
    {
        if (eventData.pointerEnter.tag != "slot") return;

        ItemTipOnOff(0, false, ItemTip);
    }

    public void OnPointerClick(PointerEventData eventData)// 슬롯 클릭했을 때 아이템 사용 창 보이기
    {
        int Sindex = EnterEventDataIndex(eventData);
        if (Inven.items[Sindex] == null) return;

        EquipManager equipManager = GameObject.FindWithTag("EquipInven").GetComponent<EquipManager>();
        equipManager.Sindex = Sindex;
        equipManager.ItemNum = Inven.slots[Sindex].item.itemTagNum;
        ItemTipOnOff(Sindex, true, ItemEquipTip);
    }

    #endregion

    #region CreateFunctions
    public Text ItemTextSeting(string TextName)// 아이템의 정보 불러오기
    {
        Text text = GameObject.FindWithTag(TextName).GetComponent<Text>();
        return text;
    }

    private void ItemTipOnOff(int Sindex, bool OnOff, GameObject itemTipObject)// 설명 / 사용 창 거리 조정 및 보이기 
    {
        ItemTipSetData itemTipData = itemTipObject.GetComponent<ItemTipSetData>();
        float ItemMethodDistance = itemTipData.ItemMethodDistance;
        float x = itemTipData.Distance_x;
        float y = itemTipData.Distance_y;

        itemTipObject.transform.position = Inven.slots[Sindex].gameObject.transform.position + new Vector3(ItemMethodDistance * x, -ItemMethodDistance * y);
        itemTipObject.SetActive(OnOff);
    }

    private void DropItemCheck(PointerEventData eventData)// 드래그 한 슬롯에 아이템을 체크 및 불러오기
    {
        Vector3 origin = eventData.position;
        float radius = 1f;

        Collider[] hitColliders = Physics.OverlapSphere(origin, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.transform.gameObject.tag == "slot" && Inven.items[ColliderDataIndex(hitCollider)] == null)
            {
                HitCheck = true;
                hit = hitCollider;
                break;
            }
            else
                HitCheck = false;
        }

        HitNotNull(HitCheck, hit); 
        HitNull(HitCheck);
    }

    private void HitNull(bool Checking)
    {
        if (Checking) return;
        SlotItemdataGet(beforeIndex, SelectSlotItem);
        SelectSlotSeting(false, null);
    }

    private void HitNotNull(bool Checking, Collider collider)
    {
        if (!Checking) return;

        int Sindex = ColliderDataIndex(collider);
        SlotItemdataGet(Sindex, SelectSlotItem);
        SelectSlotSeting(false, null);
    }

    private void SelectSlotSeting(bool Setactive, Item item)
    {
        SelectSlot.gameObject.GetComponent<Slot>().item = item;
        SelectSlot.SetActive(Setactive);
    }

    private void SlotItemdataGet(int Sindex, Item GetItem)
    {
        Inven.items[Sindex] = GetItem;
        Inven.slots[Sindex].item = GetItem;
    }

    private int DragEventDataIndex(PointerEventData eventData)//드래그 슬롯 인덱스 찾기
    {
        S = eventData.pointerDrag.transform.gameObject.GetComponent<Slot>();
        int Sindex = Array.IndexOf(Inven.slots, S);
        return Sindex;
    }

    private int EnterEventDataIndex(PointerEventData eventData)//안에 있는 슬롯 인덱스 찾기
    {
        S = eventData.pointerEnter.transform.gameObject.GetComponent<Slot>();
        int Sindex = Array.IndexOf(Inven.slots, S);
        return Sindex;
    }

    private int ColliderDataIndex(Collider hitCollider)//아이템을 놔둘 슬롯 인덱스 찾기
    {
        S = hitCollider.transform.gameObject.GetComponent<Slot>();
        int Sindex = Array.IndexOf(Inven.slots, S);
        return Sindex;
    }

    #endregion

}
