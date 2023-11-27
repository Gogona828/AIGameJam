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

    private void Start()
    {
        itemBase = GetComponent<ItemBase>();
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
        // TODO: 近いアイテムを返せる
        itemBase.MixItem(closestItem, closestItem.GetComponent<ItemBase>());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            touchingItemsList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            touchingItemsList.Remove(other.gameObject);
        }
    }
}
