using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemButton : MonoBehaviour
{
    [Header("인벤토리")]
    public Inventory inventory;

    public Item item;

    
    void click()
    {
        //print($"{item.itemName}");
        inventory.AddItem(item);
    }
}
