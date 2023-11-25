using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    [SerializeField, Tooltip("アイテムのデータベース")]
    private ItemDataBase itemDB;
    [SerializeField, Tooltip("ステージのデータベース")]
    private StageDataBase stageDB;

    public static DataBaseManager instance;

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this);
    }

    public ItemDataBase.ItemData GetItemData(int _itemID)
    {
        foreach (ItemDataBase.ItemData _data in itemDB.itemDatas)
        {
            if (_data.id == _itemID) return _data;
        }
        return null;
    }

    public StageDataBase GetStageData()
    {
        return stageDB;
    }
}
