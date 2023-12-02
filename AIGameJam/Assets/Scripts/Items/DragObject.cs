using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IDragHandler
{
    private Vector3 mousePosition;
    private List<GameObject> touchingItemsList = new List<GameObject>();
    private GameObject closestItem;
    private float closestDistance = Mathf.Infinity;
    private ItemBase itemBase;
    private ItemBase otherItemBase;

    private SEPlayer _SEPlayer;

    private void Start()
    {
        itemBase = GetComponent<ItemBase>();
        _SEPlayer = GameObject.Find("SEPlayer").GetComponent<SEPlayer>(); //絶対名前変えるなよ
    }

    public void OnDrag(PointerEventData data)
    {
        mousePosition = Input.mousePosition;
        transform.position = mousePosition;
    }

    public void FindNearItem()
    {
        float rangeWithMouse;
        foreach (GameObject _hittingItem in touchingItemsList)
        {
            rangeWithMouse = (mousePosition - _hittingItem.transform.position).magnitude;
            if (closestDistance > rangeWithMouse)
            {
                closestDistance = rangeWithMouse;
                closestItem = _hittingItem;
            }
        }
        Debug.Log($"一番近い: {closestItem}");
        if (!closestItem)
        {
            Destroy(gameObject);
            return;
        }

        otherItemBase = closestItem.GetComponent<ItemBase>();
        // アイテムが複製物だったりタグがないものだったら削除
        if (itemBase?.GetCopyItem() == closestItem || closestItem.CompareTag("Untagged"))
        {
            Destroy(gameObject);
            return;
        }
        
        // 一番近いアイテムがDustBoxであれば中に入れる
        if (closestItem.TryGetComponent(out ControlDustBox _controlDustBox))
        {
            if (itemBase.GetItemType() != _controlDustBox.GetBoxType() && _controlDustBox.GetBoxType() != ItemDataBase.ItemType.Other)
            {
                Destroy(gameObject);
                return;
            }
            _SEPlayer.SEPlayRequest(4);

            _controlDustBox.StoreGarbage();
            itemBase.RemoveCopyItem();
            Destroy(gameObject);
        }
        // 爆弾と混ぜたら爆発する
        else if (otherItemBase?.GetItemType() == ItemDataBase.ItemType.Black || itemBase.GetItemType() == ItemDataBase.ItemType.Black)
        {
            GameObject.Find("BombEffecter").GetComponent<BombExplode>().BombExplodeEffect(closestItem);//追記
            itemBase.RemoveCopyItem();
            Destroy(closestItem);
            Destroy(gameObject);



            //DustManager.instance.ExplodeDustBox();



        }
        // アイテム同士のタイプが違えば混ぜられない
        else if (otherItemBase?.GetItemType() != itemBase?.GetItemType())
        {
            Debug.Log($"can't mix {otherItemBase?.GetItemType()} and {itemBase?.GetItemType()}");
            _SEPlayer.SEPlayRequest(5);
            return;
        }
        // アイテム同士のタイプが同じであれば混ぜる
        else
        {
            if (itemBase.GetItemAmong() >= 10 || otherItemBase.GetItemAmong() >= 10) return;
            Debug.Log("mixed!");
            itemBase.MixItem(closestItem, closestItem.GetComponent<ItemBase>());
            _SEPlayer.SEPlayRequest(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item") || other.gameObject.CompareTag("DustBox"))
        {
            touchingItemsList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item") || other.gameObject.CompareTag("DustBox"))
        {
            touchingItemsList.Remove(other.gameObject);
        }
    }
}
