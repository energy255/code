using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [Header("아이템 이름")]
    public string itemName;

    [Header("아이템 이미지")]
    public Sprite itemImage;

    [Multiline][Header("아이템 설명")]
    public string itemMethod;

    [Header("아이템 종류\n0 : body, 1 : hand1(왼손), 2 : hand2(오른손),\n 3 : head, 4 : shoes, -1 : 소모품")]
    public int itemTagNum;
}
