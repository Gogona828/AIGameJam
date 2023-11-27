using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    [SerializeField] TweenButton tweenButton;
    [SerializeField] GameObject CloseGameObject;
    void Start()
    {
        tweenButton.onClickCallback= () => { CloseGameObject.SetActive(false); };
    }

}
