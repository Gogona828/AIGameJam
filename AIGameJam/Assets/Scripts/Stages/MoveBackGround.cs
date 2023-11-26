using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackGround : MonoBehaviour
{
    [SerializeField, Tooltip("移動速度")]
    private float moveSpeed;

    private StageDataBase stageDB;
    private Vector3 movePosision;
    
    // Start is called before the first frame update
    void Start()
    {
        stageDB = DataBaseManager.instance.GetStageData();
        SetMoveSpeed(stageDB.moveStageSpd);
        movePosision = new Vector3(0, moveSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= movePosision * Time.deltaTime;
    }

    private void SetMoveSpeed(float _spd)
    {
        moveSpeed = _spd;
    }
}
