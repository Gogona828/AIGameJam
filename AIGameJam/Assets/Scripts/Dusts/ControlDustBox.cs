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
    [SerializeField, Tooltip("格納できる最大量")]
    private int storingMaxQuantity = 100;
    [SerializeField, Tooltip("ゲージ")]
    private Slider storingGauge;
    [SerializeField, Tooltip("ゲージが減少する間隔")]
    private float decreaseInterval = 2;
    [SerializeField, Tooltip("閉じている差分")]
    private Sprite closedDifferencial;
    [SerializeField, Tooltip("開いている差分")]
    private Sprite openingDifferencial;

    private Image image;
    private bool isOpend = false;
    private ItemBase itemBase;
    private CustomButton customButton;
    private float elapsedTime;

    private void Start()
    {
        image = GetComponent<Image>();
        if (boxType != ItemDataBase.ItemType.Other) storingQuantity = storingMaxQuantity / 2;
        else storingQuantity = 0;
        MoveGauge();
    }

    private void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= decreaseInterval && boxType != ItemDataBase.ItemType.Other)
        {
            storingQuantity--;
            MoveGauge();
            elapsedTime = 0;
        }
    }

    private void MoveGauge()
    {
        storingGauge.value = (float)storingQuantity / (float)storingMaxQuantity;
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
        if (storingQuantity >= storingMaxQuantity) {
            storingQuantity = storingMaxQuantity;
            if (boxType != ItemDataBase.ItemType.Other) return;
            ReleaseSkill.instance.AddSkillReleaseCount();
            storingQuantity = 0;
        }
        Debug.Log($"{gameObject.name}: {storingQuantity}");
        MoveGauge();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!itemBase) return;
        if (itemBase.GetItemType() != boxType && boxType != ItemDataBase.ItemType.Other) return;
        canBeDiscard = true;
        Debug.Log($"{boxType}");
        
        if (!customButton) customButton = other.gameObject.GetComponent<CustomButton>();
        
        if (customButton.shouldUpPointer)
        {
            StoreGarbage();
            itemBase.RemoveCopyItem();
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
