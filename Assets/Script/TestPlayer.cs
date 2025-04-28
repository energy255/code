using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPlayer : MonoBehaviour
{
    [Header("인벤토리")]
    public Inventory Inven;

    //불러오기 변수
    private Slot S;// 클릭한 슬롯의 스크립트를 불러온 변수
    private Item SelectSlotItem;// 클릭한 슬롯의 아이템 값 저장 변수
    public GameObject SelectSlot;


    private void Start()
    {
        SelectSlot.gameObject.GetComponent<Slot>().item = null;
    }


    void Update()
    {
        LeftMouseEvent();
    }

    private void LeftMouseEvent()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.collider != null && hit.transform.gameObject.tag == "slot")
        {
            S = hit.transform.gameObject.GetComponent<Slot>();
            int Sindex = Array.IndexOf(Inven.slots, S);
            print(Sindex);
            SelectSlotItem = Inven.items[Sindex];
            SelectSlot.gameObject.GetComponent<Slot>().item = SelectSlotItem;
        }
    }

    void HitCheckObject(RaycastHit2D hit)
    {
        IObjectItem clickInterface = hit.transform.gameObject.GetComponent<IObjectItem>();

        if (clickInterface != null)
        {
            Item item = clickInterface.ClickItem();
            print($"{item.itemName}");
            Inven.AddItem(item);
        }
    }
}
