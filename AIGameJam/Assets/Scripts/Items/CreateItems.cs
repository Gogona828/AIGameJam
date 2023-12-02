using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreateItems : MonoBehaviour
{
    [SerializeField, Tooltip("生成するItemPrefab")]
    private GameObject itemPrefab;

    [SerializeField, Tooltip("生成する爆弾")]
    private GameObject bombPrefab;

    [SerializeField, Tooltip("爆弾生成割合"), Range(0, 50)]
    private int bombGenerationRate = 0;

    [SerializeField, Tooltip("ブロンズメダルのライン")]
    private int bornzeLine = 50000;

    [SerializeField, Tooltip("移動距離の取得")]
    private CalcMoveDistance calcMoveDistance;
    
    [SerializeField, Tooltip("生成時間間隔")]
    private float generatingTimeSpacing;
    
    [SerializeField, Tooltip("進行方向")]
    private bool isLeftGo;


    private GameObject generatedItem;
    private ItemBase itemBase;
    private int itemCount;
    private float elapsedTime;

    private int randomNum;
    private GameObject generatingItem;

    private void Start()
    {
        itemCount = DataBaseManager.instance.GetItemDataCount();
        CreateItem();
    }

    private void FixedUpdate()
    {
        if (ExitGame.hasFinishedGame || !StartGame.shouldStartingGame) return;
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= generatingTimeSpacing / ManageCarrySystem.instance.CalcVelocityDiameter()) {
            CreateItem();
            elapsedTime = 0;
        }
    }

    private void CreateItem()
    {
        randomNum = Random.Range(0, 100);
        bombGenerationRate = (int)calcMoveDistance.GetTotalMoveDistance() / bornzeLine * 30;
        if (bombGenerationRate >= 20) bombGenerationRate = 20;
        if (randomNum < bombGenerationRate)
        {
            generatingItem = bombPrefab;
        }
        else
        {
            generatingItem = itemPrefab;
        }
        generatedItem = Instantiate(generatingItem, transform.position, Quaternion.identity, transform);
        generatedItem.GetComponent<MoveStage>().SetTravelingDirection(isLeftGo);
        itemBase = generatedItem.GetComponent<ItemBase>();
        if (randomNum >= bombGenerationRate) itemBase.SetItemID(Random.Range(0, itemCount));
    }
}
