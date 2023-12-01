using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] Camera _camera;

    public void CameraShake()
    {
        _camera.DOShakePosition(1.0f);
    }

}
