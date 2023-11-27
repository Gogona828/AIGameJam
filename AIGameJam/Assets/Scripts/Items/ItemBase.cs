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

    [SerializeField, Tooltip("Mixしたときにオブジェクトを変更する")]
    private List<Sprite> imagesWhenMixed = new List<Sprite>();

    private ItemDataBase.ItemData itemData;
    private Image itemImage;
    // 複製されたもの
    [SerializeField]
    private GameObject copyItem;
    [SerializeField]
    private bool canBeMixed = false;
    [SerializeField]
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

    #region 取得系
    private void GetItemData()
    {
        itemType = itemData.type;
        if (!isCopied) itemAmong = itemData.among;
        if (!isCopied) itemImage.sprite = itemData.sprite;
    }
    
    public int GetItemAmong()
    {
        return itemAmong;
    }

    public ItemDataBase.ItemType GetItemType()
    {
        return itemType;
    }
    #endregion
    #region 設定系
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
    #endregion

    private void OnTriggerStay2D(Collider2D other) => OnTriggerEnter2D(other);

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 何かに触れているか
        isTouchingAny = true;
        // 混ぜられるかを判定
        canBeMixed = (copyItem && isDragged) ? true : false;
        // 混ぜられないならリターン
        if (!canBeMixed) return;
        // 「対象が複製物」でかつ「離されたアイテム」かつ「対象が保持されたアイテムでない」であれば自身を破壊
        // = 離されたアイテムの場所が複製物の上で、通常アイテムと保持アイテム以外の上であれば離したものを破壊する。
        if (copyItem.name == other.gameObject.name && isDragged && !(other.gameObject.CompareTag("Item") ||　other.gameObject.CompareTag("SaveItem")))
        {
            Debug.Log($"変なところで置いた：{copyItem.name} : {other.gameObject.name}");
            Destroy(gameObject);
            return;
        }
        else if (other.gameObject.CompareTag("Untagged")) {
            Destroy(gameObject);
        }
        if (other.gameObject.TryGetComponent(out ItemBase _itemBase))
        {
            if (!gameObject.CompareTag("SaveItem") && itemType != _itemBase.itemType || itemAmong >= 10 || _itemBase.itemAmong >= 10) {
                Debug.Log($"不可: {_itemBase.gameObject.tag} & {gameObject.tag}");
                Destroy(gameObject);
                return;
            }
            if (!gameObject.CompareTag("Item") || !other.gameObject.CompareTag("Item")) return;
            // MixItem(other.gameObject, _itemBase);
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
        _mixTarget.gameObject.GetComponent<Image>().sprite = imagesWhenMixed[(int)itemType];
        Destroy(gameObject);
        if (!copyItem) return;
        Destroy(copyItem);
    }

    public void DecideWhetherDestroy()
    {
        if (!isTouchingAny || !copyItem)
        {
            Debug.Log($"捨てられた: {gameObject.name}");
            Destroy(gameObject);
        }
    }

    public void RemoveCopyItem()
    {
        Destroy(copyItem);
    }
}
