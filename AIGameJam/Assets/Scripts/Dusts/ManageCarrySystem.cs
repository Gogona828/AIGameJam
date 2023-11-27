using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageCarrySystem : MonoBehaviour
{
    public static ManageCarrySystem instance;

    [SerializeField, Tooltip("最大速度倍率")]
    private float maxDiameter = 5;
    [SerializeField, Tooltip("最小速度倍率")]
    private float minDiameter = 0.5f;

    private ControlDustBox controlDustBox;

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        controlDustBox = GetComponent<ControlDustBox>();
    }

    public float CalcVelocityDiameter()
    {
        return controlDustBox.GetStoringQuantity() / (100 / (maxDiameter - 1)) + minDiameter;
    }
}
