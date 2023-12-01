using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustManager : MonoBehaviour
{
    public static DustManager instance;
    
    [SerializeField, Tooltip("DustBoxの配列")]
    private ControlDustBox[] dustBoxes;

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this);
    }

    public void ExplodeDustBox()
    {
        for (int i = 0; i < dustBoxes.Length; i++)
        {
            dustBoxes[i].DecreaseStoringQuantity();
        }
    }
}
