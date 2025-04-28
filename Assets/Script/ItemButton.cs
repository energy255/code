using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Inventory _inventory;

    public Item[] _itemDataArray;

    [Header("아이템 버튼")]
    public Button _AddApple;
    public Button _AddHelmet;
    public Button _AddChestArmor;
    public Button _AddShose;
    public Button _AddSword;
    public Button _AddShield;
    

    private void Start()
    {
        OnClickButton();
    }

    private void OnClickButton()
    {
        _AddApple.onClick.AddListener(() => _inventory.AddItem(_itemDataArray[0]));
        _AddHelmet.onClick.AddListener(() => _inventory.AddItem(_itemDataArray[1]));
        _AddChestArmor.onClick.AddListener(() => _inventory.AddItem(_itemDataArray[2]));
        _AddShose.onClick.AddListener(() => _inventory.AddItem(_itemDataArray[3]));
        _AddSword.onClick.AddListener(() => _inventory.AddItem(_itemDataArray[4]));
        _AddShield.onClick.AddListener(() => _inventory.AddItem(_itemDataArray[5]));
    }
}
