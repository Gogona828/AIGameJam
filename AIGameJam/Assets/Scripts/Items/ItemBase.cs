using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBase : MonoBehaviour
{
    public bool isTouchingSameType = false;
    
    [SerializeField, Tooltip("アイテムのIDを入れる")]
    private int itemID;

    [SerializeField, Tooltip("アイテムの種別")]
    private ItemDataBase.ItemType itemType;

    [SerializeField, Tooltip("アイテムの量")]
    private int itemAmong;

    private ItemDataBase.ItemData itemData;
    private Image itemImage;
    
    // Start is called before the first frame update
    void Start()
    {
        itemData = DataBaseManager.instance.GetItemData(itemID);
        itemImage = GetComponent<Image>();
        GetItemData();
    }

    private void GetItemData()
    {
        itemType = itemData.type;
        itemAmong = itemData.among;
        itemImage.sprite = itemData.sprite;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out ItemBase _itemBase)) return;
        Debug.Log(other.gameObject.name);
    }
}
