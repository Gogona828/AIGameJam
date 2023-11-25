using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    [SerializeField, Tooltip("ステージのデータベース")]
    private StageDataBase stageDB;

    public static DataBaseManager instance;

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this);
    }

    public StageDataBase GetStageData()
    {
        return stageDB;
    }
}
