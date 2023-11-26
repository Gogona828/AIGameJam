using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveItem : MonoBehaviour
{
    public bool isSkillReleased = false;
    
    [SerializeField, Tooltip("保持する場所")]
    private Transform savePosition;
    
    private ItemBase itemBase;
    private CustomButton customButton;
    private bool canBeSave;
    [SerializeField]
    private bool alreadyItemExists = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void PutItem(GameObject _item)
    {
        _item.GetComponent<BoxCollider2D>().enabled = false;
        Destroy(_item.GetComponent<Rigidbody2D>());
        _item.transform.position = savePosition.position;
        alreadyItemExists = true;
        _item.tag = "SaveItem";
        itemBase.isDragged = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!itemBase) return;
        if (!itemBase.isDuringDrag || !isSkillReleased || alreadyItemExists || !other.gameObject.CompareTag("SaveItem")) return;
        Debug.Log("置けるよ！");

        if (!customButton) customButton = other.gameObject.GetComponent<CustomButton>();
        else if (customButton.shouldUpPointer)
        {
            PutItem(other.gameObject);
            itemBase.RemoveCopyItem();
        }
        
        Debug.Log($"{customButton} / {other.gameObject.name}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isSkillReleased && alreadyItemExists) return;
        if (other.gameObject.TryGetComponent(out ItemBase _itemBase)) {
            if (!_itemBase.isDuringDrag) return;
            itemBase = _itemBase;
            canBeSave = true;
            other.gameObject.tag = "SaveItem";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (canBeSave ) {
            canBeSave = false;
        }

        if (alreadyItemExists) alreadyItemExists = false;
        if (other.gameObject.CompareTag("SaveItem")) other.gameObject.tag = "Item";
    }
}
