using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ItemDataBase")]
public class ItemDataBase : ScriptableObject
{
    public List<ItemData> itemDatas = new List<ItemData>();
    
    public enum ItemType
    {
        Red, Yellow, Blue, Other
    }
    [System.Serializable]
    public class ItemData
    {
        [Tooltip("itemのid")] public int id = 1;
        [Tooltip("itemの属性")] public ItemType type;
        [Tooltip("itemの量")] public int among = 1;
        [Tooltip("itemの画像")] public Sprite sprite;
    }
}
