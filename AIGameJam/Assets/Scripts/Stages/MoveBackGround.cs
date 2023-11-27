using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackGround : MonoBehaviour
{
    [SerializeField, Tooltip("移動速度")]
    private float moveSpeed;
    [SerializeField, Tooltip("速度倍率")]
    private float velocityDiameter = 1;

    private StageDataBase stageDB;
    private Vector3 movePosision;
    private CalcMoveDistance calcMoveDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        stageDB = DataBaseManager.instance.GetStageData();
        SetMoveSpeed(stageDB.moveStageSpd);
        movePosision = new Vector3(0, moveSpeed, 0);
        calcMoveDistance = GetComponent<CalcMoveDistance>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!StartGame.shouldStartingGame) return;
        transform.position -= movePosision * Time.deltaTime * velocityDiameter;
        calcMoveDistance.CalcDistance(transform.localPosition.y);
    }

    public void SetVelocityDimater(float _diameter)
    {
        velocityDiameter = _diameter / 50;
    }

    private void SetMoveSpeed(float _spd)
    {
        moveSpeed = _spd;
    }
}
