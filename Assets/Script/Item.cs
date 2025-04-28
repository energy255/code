using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [Header("������ �̸�")]
    public string itemName;

    [Header("������ �̹���")]
    public Sprite itemImage;

    [Multiline][Header("������ ����")]
    public string itemMethod;

    [Header("������ ����\n0 : body, 1 : hand1(�޼�), 2 : hand2(������),\n 3 : head, 4 : shoes, -1 : �Ҹ�ǰ")]
    public int itemTagNum;
}
