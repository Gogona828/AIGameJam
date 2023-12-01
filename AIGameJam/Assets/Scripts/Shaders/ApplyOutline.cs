using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyOutline : MonoBehaviour
{
    [SerializeField, Tooltip("アウトラインの色の配列")]
    private Material[] outlines;
    private ItemBase itemBase;
    private ItemDataBase.ItemType itemType;
    private Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        itemBase = GetComponent<ItemBase>();
        itemType = itemBase.GetItemType();
        image = GetComponent<Image>();
        
        switch (itemType)
        {
            case ItemDataBase.ItemType.Red:
                image.material = outlines[0];
                break;
            case ItemDataBase.ItemType.Yellow:
                image.material = outlines[1];
                break;
            case ItemDataBase.ItemType.Blue:
                image.material = outlines[2];
                break;
            default: 
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
