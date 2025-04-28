using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPlayer : MonoBehaviour
{
    [Header("�κ��丮")]
    public Inventory Inven;

    //�ҷ����� ����
    private Slot S;// Ŭ���� ������ ��ũ��Ʈ�� �ҷ��� ����
    private Item SelectSlotItem;// Ŭ���� ������ ������ �� ���� ����
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
