using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items;

    [SerializeField]
    private Transform slotParent;

    public Slot[] slots;

#if UNITY_EDITOR
    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }
#endif

    void Awake()
    {
        FreshSlot();
    }

    public void FreshSlot()
    {
        int i = 0;
        for (; i < items.Count && i < slots.Length; i++)
            slots[i].item = items[i];
        for (; i < slots.Length; i++)
            slots[i].item = null;
    }

    public void AddItem(Item _item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
            {
                items[i] = _item;
                FreshSlot();
                i = items.Count;
            }
        }
    }


    public void ReLoad()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }
}
