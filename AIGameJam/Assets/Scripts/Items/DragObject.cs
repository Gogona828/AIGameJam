using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IDragHandler
{
    private Vector3 mousePosition;
    public GameObject itemInDrag;
    
    public void OnDrag(PointerEventData data)
    {
        mousePosition = Input.mousePosition;
        transform.position = mousePosition;
    }
}
