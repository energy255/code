using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectItem : MonoBehaviour, IObjectItem
{
    [Header("������")]
    public Item item;

    void Start()
    {
    }
    public Item ClickItem()
    {
        return this.item;
    }
}
