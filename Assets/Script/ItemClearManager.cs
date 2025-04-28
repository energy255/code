using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemClearManager : MonoBehaviour, IPointerClickHandler
{
    //불러오기 변수
    public GameObject ItemClearTip;
    private Slot S;// 클릭한 슬롯의 스크립트를 불러온 변수
    private EquipManager equipManager;// 창의 오브젝트를 불러오기 위한 변수

    void Start()
    {
        equipManager = GameObject.FindWithTag("EquipInven").GetComponent<EquipManager>();
        ItemClearTip = equipManager.ItemClearTip;
    }

    public void OnPointerClick(PointerEventData eventData)// 슬롯 클릭했을 때 아이템 해제 창 보이기
    {
        equipManager.ClearItemNum = EnterEventDataIndex(eventData);
        ItemTipOnOff(true, eventData);
    }

    private void ItemTipOnOff(bool OnOff, PointerEventData eventData)
    {
        ItemTipSetData ItemClearData = ItemClearTip.GetComponent<ItemTipSetData>();
        float ItemMethodDistance = ItemClearData.ItemMethodDistance;
        float x = ItemClearData.Distance_x;
        float y = ItemClearData.Distance_y;

        ItemClearTip.transform.position = eventData.pointerEnter.gameObject.transform.position + new Vector3(ItemMethodDistance * x, -ItemMethodDistance * y);
        ItemClearTip.SetActive(OnOff);
    }

    private int EnterEventDataIndex(PointerEventData eventData)//슬롯 인덱스 찾기
    {
        S = eventData.pointerEnter.transform.gameObject.GetComponent<Slot>();
        int Sindex = Array.IndexOf(equipManager.EquipSlots, S);
        return Sindex;
    }
}
