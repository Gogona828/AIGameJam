using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlDustBox : MonoBehaviour
{
    public bool canBeDiscard = false;
    
    [SerializeField, Tooltip("箱の属性")]
    private ItemDataBase.ItemType boxType;
    [SerializeField, Tooltip("格納している量")]
    private int storingQuantity;
    [SerializeField, Tooltip("閉じている差分")]
    private Sprite closedDifferencial;
    [SerializeField, Tooltip("開いている差分")]
    private Sprite openingDifferencial;

    private Image image;
    private bool isOpend = false;
    private ItemBase itemBase;
    private CustomButton customButton;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OpenDustBox()
    {
        Debug.Log("open!");
        isOpend = true;
        image.sprite = openingDifferencial;
    }

    public void CloseDustBox()
    {
        image.sprite = closedDifferencial;
    }

    private void StoreGarbage()
    {
        storingQuantity += itemBase.GetItemAmong();
        Debug.Log($"{gameObject.name}: {storingQuantity}");
        // TODO:ゲージに反映させる。
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!itemBase) return;
        if (itemBase.GetItemType() != boxType || boxType.ToString() == "Other") return;
        canBeDiscard = true;
        
        if (!customButton) customButton = other.gameObject.GetComponent<CustomButton>();
        
        if (customButton.shouldUpPointer)
        {
            StoreGarbage();
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out ItemBase _itemBase)) {
            if (!_itemBase.isDuringDrag) return;
            itemBase = _itemBase;
            OpenDustBox();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isOpend) {
            CloseDustBox();
            isOpend = false;
            canBeDiscard = false;
        }
    }
}
