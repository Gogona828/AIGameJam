using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public System.Action onClickCallBack;
    private bool wasDuplicated;
    private GameObject replicatedObjects;
    private Transform parentObject;

    private void Start()
    {
        parentObject = transform.parent.transform;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClickCallBack?.Invoke();
        Debug.Log("押された");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("長押し");
        if (wasDuplicated) return;
        replicatedObjects = Instantiate(gameObject, transform.position, Quaternion.identity, parentObject);
        gameObject.AddComponent<DragObject>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Destroy(gameObject);
    }
}
