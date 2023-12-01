using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShaker : MonoBehaviour
{
    public GameObject _maincamera;

    public void CameraShake()
    {
        //_maincamera.transform.DOKill();
        _maincamera.transform.DOShakePosition(2.0f,10f);
    }

}
