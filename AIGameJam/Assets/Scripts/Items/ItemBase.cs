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

    [SerializeField, Tooltip("アイテムが大きくなる倍率")]
    private float rateGettingLarge = 10;

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

    public void SetItemID(int id)
    {
        itemID = id;
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
        if (copyItem.name == other.gameObject.name && isDragged) {
            Destroy(gameObject);
            return;
        }
        if (other.gameObject.TryGetComponent(out ItemBase _itemBase))
        {
            if (itemType != _itemBase.itemType) {
                Destroy(gameObject);
                return;
            }
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
        _itemBase.itemAmong += itemAmong;
        _mixTarget.transform.localScale += new Vector3(itemAmong, itemAmong, itemAmong) / rateGettingLarge;
        Debug.Log(_mixTarget.transform.localScale);
        Destroy(gameObject);
        if (!copyItem) return;
        Destroy(copyItem);
    }

    public void DecideWhetherDestroy()
    {
        if (!isTouchingAny || !copyItem) Destroy(gameObject);
    }
}
