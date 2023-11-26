using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteItem : MonoBehaviour
{
    private ItemBase itemBase;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Item")) return;
        itemBase = other.gameObject.GetComponent<ItemBase>();
        if (itemBase.isDuringDrag) return;
        else Destroy(other.gameObject);
    }
}
