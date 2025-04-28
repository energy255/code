using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipManager : MonoBehaviour
{
    //��� ����
    public int Sindex;
    public int ItemNum;
    public int ClearItemNum;

    //�ҷ����� ����
    public Slot[] EquipSlots;
    public Inventory Inven;
    public GameObject ItemClearTip;// ���� â ������Ʈ �ҷ�����
    public GameObject ItemEquipTip;// ��� â ������Ʈ �ҷ�����
    public GameObject ItemTip;// ���� â ������Ʈ �ҷ�����

    [Header("������ ��� â ��ư")]
    public Button EquipItem;
    public Button UseItem;
    public Button DeleteItem;

    [Header("������ ���� â ��ư")]
    public Button ClearItem;

    void Awake()
    {
        TipSeting();
        EquipItem.onClick.AddListener(() => EquipSlotSeting());
        ClearItem.onClick.AddListener(() => ClearSeting());
        UseItem.onClick.AddListener(() => UseSlotSeting());
        DeleteItem.onClick.AddListener(() => DeleteSlotSeting());
    }

    private void TipSeting()
    {
        ItemClearTip = TipObjectFind("ClearTip");
        ItemEquipTip = TipObjectFind("EquipTip");
        ItemTip = TipObjectFind("ItemTip");
    }

    private GameObject TipObjectFind(string TagName)
    {
        GameObject ItemTipObject = GameObject.FindWithTag(TagName);
        ItemTipObject.SetActive(false);
        return ItemTipObject;
    }

    private void ClearSeting()
    {
        ItemClearTip.SetActive(false);

        if (EquipSlots[ClearItemNum].item == null) return;

        Inven.AddItem(EquipSlots[ClearItemNum].item);
        EquipSlots[ClearItemNum].item = null;
    }

    private void EquipSlotSeting()
    {
        ItemEquipTip.SetActive(false);

        if (0 > ItemNum) return;

        if(EquipSlots[ItemNum].item == null)
        {
            EquipSlots[ItemNum].item = Inven.items[Sindex];
            Inven.items[Sindex] = null;
            Inven.slots[Sindex].item = null;
        }
    }

    private void UseSlotSeting()
    {
        ItemEquipTip.SetActive(false);

        if (-1 != ItemNum) return;

        print("���");
        Inven.items[Sindex] = null;
        Inven.slots[Sindex].item = null;
    }

    private void DeleteSlotSeting()
    {
        ItemEquipTip.SetActive(false);

        print("����");
        Inven.items[Sindex] = null;
        Inven.slots[Sindex].item = null;
    }
}
