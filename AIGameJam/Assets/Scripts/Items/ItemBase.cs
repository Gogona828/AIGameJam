using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBase : MonoBehaviour
{
    public bool isDragged = false;
    public bool isDuringDrag = false;
    // 複製されたものならtrue
    public bool isCopied = false;
    
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
    // 複製されたもの
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

    private void Update()
    {
        if (transform.position.x <= -1000 || transform.position.x >= 3000 || transform.position.y <= -500) Destroy(gameObject);
    }

    private void GetItemData()
    {
        itemType = itemData.type;
        if (!isCopied) itemAmong = itemData.among;
        itemImage.sprite = itemData.sprite;
    }
    
    public int GetItemAmong()
    {
        return itemAmong;
    }

    public ItemDataBase.ItemType GetItemType()
    {
        return itemType;
    }

    public void SetItemID(int _id)
    {
        itemID = _id;
    }
    
    public void SetItemAmong(int _among)
    {
        itemAmong = _among;
    }

    public void SetCopyItem(GameObject _copyItem)
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
            if (itemType != _itemBase.itemType || itemAmong >= 10 || _itemBase.itemAmong >= 10) {
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
        _mixTarget.transform.localScale += transform.localScale * _itemBase.itemAmong / rateGettingLarge;
        Debug.Log($"{_itemBase.itemAmong} / {_mixTarget.transform.localScale.x}");
        Destroy(gameObject);
        if (!copyItem) return;
        Destroy(copyItem);
    }

    public void DecideWhetherDestroy()
    {
        if (!isTouchingAny || !copyItem) Destroy(gameObject);
    }

    public void RemoveCopyItem()
    {
        Destroy(copyItem);
    }
}
