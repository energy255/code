using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemButton : MonoBehaviour
{
    [Header("�κ��丮")]
    public Inventory inventory;

    public Item item;

    
    void click()
    {
        //print($"{item.itemName}");
        inventory.AddItem(item);
    }
}
