using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    [Header("½½·Ô °¹¼ö")]
    public int SlotCount;

    public GameObject GO;
    public Inventory IVT;

    void Start()
    {
        for (int i = 0; i < SlotCount - 1; i++)
        {
            IVT.items.Add(null);
            Instantiate(GO, GO.transform.parent);
        }
        IVT.items.Add(null);
        IVT.ReLoad();
    }
}
