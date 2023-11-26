using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBase : MonoBehaviour
{
    public bool isDragged = false;
    
    [SerializeField, Tooltip("アイテムのIDを入れる")]
    private int itemID;

    [SerializeField, Tooltip("アイテムの種別")]
    private ItemDataBase.ItemType itemType;

    [SerializeField, Tooltip("アイテムの量")]
    private int itemAmong;

    private ItemDataBase.ItemData itemData;
    private Image itemImage;
    private GameObject copyItem;
    private bool canBeMixed = false;
    private bool isTouchingAny = false;
    
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

    public void SetcopyItem(GameObject _copyItem)
    {
        copyItem = _copyItem;
    }

    private void OnTriggerStay2D(Collider2D other) => OnTriggerEnter2D(other);

    private void OnTriggerEnter2D(Collider2D other)
    {
        isTouchingAny = true;
        canBeMixed = (copyItem && isDragged) ? true : false;
        if (!canBeMixed) return;
        if (copyItem.name == other.gameObject.name) return;
        if (other.gameObject.TryGetComponent(out ItemBase _itemBase))
        {
            if (itemType != _itemBase.itemType) return;
            MixItem(other.gameObject, _itemBase);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isTouchingAny = false;
    }

    public void MixItem(GameObject _mixTarget, ItemBase _itemBase)
    {
        Debug.Log("mix!");
        itemAmong += _itemBase.itemAmong;
        _mixTarget.transform.localScale += new Vector3(itemAmong, itemAmong, itemAmong) / 10;
        Destroy(gameObject);
        Destroy(copyItem);
    }

    public void DecideWhetherDestroy()
    {
        if (!isTouchingAny) Destroy(gameObject);
    }
}
