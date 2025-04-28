using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemClearManager : MonoBehaviour, IPointerClickHandler
{
    //�ҷ����� ����
    public GameObject ItemClearTip;
    private Slot S;// Ŭ���� ������ ��ũ��Ʈ�� �ҷ��� ����
    private EquipManager equipManager;// â�� ������Ʈ�� �ҷ����� ���� ����

    void Start()
    {
        equipManager = GameObject.FindWithTag("EquipInven").GetComponent<EquipManager>();
        ItemClearTip = equipManager.ItemClearTip;
    }

    public void OnPointerClick(PointerEventData eventData)// ���� Ŭ������ �� ������ ���� â ���̱�
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

    private int EnterEventDataIndex(PointerEventData eventData)//���� �ε��� ã��
    {
        S = eventData.pointerEnter.transform.gameObject.GetComponent<Slot>();
        int Sindex = Array.IndexOf(equipManager.EquipSlots, S);
        return Sindex;
    }
}
