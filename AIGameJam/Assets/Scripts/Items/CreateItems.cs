using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreateItems : MonoBehaviour
{
    [SerializeField, Tooltip("生成するItemPrefab")]
    private GameObject itemPrefab;

    [SerializeField, Tooltip("生成時間間隔")]
    private float generatingTimeSpacing;
    
    [SerializeField, Tooltip("進行方向")]
    private bool isLeftGo;
    [SerializeField, Tooltip("青いゴミ箱")]
    private ControlDustBox controlDustBox;

    private GameObject generatedItem;
    private ItemBase itemBase;
    private int itemCount;
    private float elapsedTime;

    private void Start()
    {
        itemCount = DataBaseManager.instance.GetItemDataCount();
        CreateItem();
    }

    private void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= generatingTimeSpacing / ManageCarrySystem.instance.CalcVelocityDiameter()) {
            CreateItem();
            elapsedTime = 0;
        }
    }

    private void CreateItem()
    {
        generatedItem = Instantiate(itemPrefab, transform.position, Quaternion.identity, transform);
        generatedItem.GetComponent<MoveStage>().SetTravelingDirection(isLeftGo);
        itemBase = generatedItem.GetComponent<ItemBase>();
        itemBase.SetItemID(Random.Range(0, itemCount));
    }
}
