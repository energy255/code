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
    [Header("�κ��丮")]
    public Inventory Inven;

    //��� ����
    private int beforeIndex;
    private bool HitCheck;
    private Collider hit;

    //�ҷ����� ����
    private Slot S;// Ŭ���� ������ ��ũ��Ʈ�� �ҷ��� ����
    private Item SelectSlotItem;// Ŭ���� ������ ������ �� ���� ����
    private EquipManager equipManager;// â�� ������Ʈ�� �ҷ����� ���� ����
    public GameObject SelectSlot;// ������ ������ �ȱ�� �� ó�� ���̱� ���� ����
    public GameObject ItemTip;// ������ �̸�/���� â
    public GameObject ItemEquipTip;// ������ ��� â

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

    public void OnDrag(PointerEventData eventData)// ���� �巡�� ���콺 ��ǥ�� �̵�
    {
        if (eventData.pointerDrag.tag != "slot") return;

        GameObject.FindWithTag("MoveSlot").transform.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)// ���� �巡�׽� �巡���� ������ ������ �ҷ�����
    {
        if (eventData.pointerDrag.tag != "slot") return;

        ItemTipOnOff(0, false, ItemEquipTip);
        int Sindex = DragEventDataIndex(eventData);
        SelectSlotItem = Inven.items[Sindex];
        SelectSlotSeting(true, SelectSlotItem);

        beforeIndex = Sindex;
        SlotItemdataGet(beforeIndex, null);
    }

    public void OnEndDrag(PointerEventData eventData)// ���� �巡�� ���� �� üŷ
    {
        DropItemCheck(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)// ���Կ� ���콺�� ����� �� ������ ���� â ���̱�
    {
        if (eventData.pointerEnter.tag != "slot") return;

        int Sindex = EnterEventDataIndex(eventData);
        if (Inven.items[Sindex] == null) return;

        ItemTipOnOff(Sindex, true, ItemTip);
        ItemTextSeting("NameText").text = Inven.items[Sindex].itemName;
        ItemTextSeting("TipText").text = Inven.items[Sindex].itemMethod;
    }

    public void OnPointerExit(PointerEventData eventData)// ���콺�� ���Կ� ����� ������ ���� â �����
    {
        if (eventData.pointerEnter.tag != "slot") return;

        ItemTipOnOff(0, false, ItemTip);
    }

    public void OnPointerClick(PointerEventData eventData)// ���� Ŭ������ �� ������ ��� â ���̱�
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
    public Text ItemTextSeting(string TextName)// �������� ���� �ҷ�����
    {
        Text text = GameObject.FindWithTag(TextName).GetComponent<Text>();
        return text;
    }

    private void ItemTipOnOff(int Sindex, bool OnOff, GameObject itemTipObject)// ���� / ��� â �Ÿ� ���� �� ���̱� 
    {
        ItemTipSetData itemTipData = itemTipObject.GetComponent<ItemTipSetData>();
        float ItemMethodDistance = itemTipData.ItemMethodDistance;
        float x = itemTipData.Distance_x;
        float y = itemTipData.Distance_y;

        itemTipObject.transform.position = Inven.slots[Sindex].gameObject.transform.position + new Vector3(ItemMethodDistance * x, -ItemMethodDistance * y);
        itemTipObject.SetActive(OnOff);
    }

    private void DropItemCheck(PointerEventData eventData)// �巡�� �� ���Կ� �������� üũ �� �ҷ�����
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

    private int DragEventDataIndex(PointerEventData eventData)//�巡�� ���� �ε��� ã��
    {
        S = eventData.pointerDrag.transform.gameObject.GetComponent<Slot>();
        int Sindex = Array.IndexOf(Inven.slots, S);
        return Sindex;
    }

    private int EnterEventDataIndex(PointerEventData eventData)//�ȿ� �ִ� ���� �ε��� ã��
    {
        S = eventData.pointerEnter.transform.gameObject.GetComponent<Slot>();
        int Sindex = Array.IndexOf(Inven.slots, S);
        return Sindex;
    }

    private int ColliderDataIndex(Collider hitCollider)//�������� ���� ���� �ε��� ã��
    {
        S = hitCollider.transform.gameObject.GetComponent<Slot>();
        int Sindex = Array.IndexOf(Inven.slots, S);
        return Sindex;
    }

    #endregion

}
