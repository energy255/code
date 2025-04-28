using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipManager : MonoBehaviour
{
    //사용 변수
    public int Sindex;
    public int ItemNum;
    public int ClearItemNum;

    //불러오기 변수
    public Slot[] EquipSlots;
    public Inventory Inven;
    public GameObject ItemClearTip;// 해제 창 오브젝트 불러오기
    public GameObject ItemEquipTip;// 사용 창 오브젝트 불러오기
    public GameObject ItemTip;// 설명 창 오브젝트 불러오기

    [Header("아이템 사용 창 버튼")]
    public Button EquipItem;
    public Button UseItem;
    public Button DeleteItem;

    [Header("아이템 해제 창 버튼")]
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

        print("사용");
        Inven.items[Sindex] = null;
        Inven.slots[Sindex].item = null;
    }

    private void DeleteSlotSeting()
    {
        ItemEquipTip.SetActive(false);

        print("삭제");
        Inven.items[Sindex] = null;
        Inven.slots[Sindex].item = null;
    }
}
