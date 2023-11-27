using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public System.Action onClickCallBack;
    public bool shouldUpPointer = false;
    private GameObject replicatedObjects;
    private Transform parentObject;
    private ItemBase itemBase;
    private ItemBase otherItemBase;
    private DragObject dragObject;

    private void Start()
    {
        parentObject = transform.parent.transform;
        itemBase = GetComponent<ItemBase>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClickCallBack?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("長押し開始");
        Debug.Log(gameObject.tag);
        shouldUpPointer = false;
        gameObject.layer = 6;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        if (transform.parent.parent != null) transform.SetParent(transform.parent.parent.parent.parent.parent);
        
        replicatedObjects = Instantiate(gameObject, transform.position, Quaternion.identity, parentObject);
        replicatedObjects.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
        itemBase.SetCopyItem(replicatedObjects);
        itemBase.isDuringDrag = true;
        
        otherItemBase = replicatedObjects.GetComponent<ItemBase>();
        otherItemBase.isCopied = true;
        otherItemBase.isDuringDrag = true;
        otherItemBase.SetItemAmong(itemBase.GetItemAmong());

        if (!gameObject.GetComponent<Rigidbody2D>()) gameObject.AddComponent<Rigidbody2D>();
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        Destroy(gameObject.GetComponent<MoveStage>());
        
        if (!gameObject.GetComponent<DragObject>()) dragObject = gameObject.AddComponent<DragObject>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragObject.FindNearItem();
        itemBase.isDragged= true;
        itemBase.DecideWhetherDestroy();
        shouldUpPointer = true;
        gameObject.layer = 5;
    }
}
