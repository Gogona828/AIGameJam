using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BlackOutRemove : MonoBehaviour
{
    [SerializeField]RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;

        if(rectTransform!=null)
        rectTransform.DOAnchorPosY(1000,1.0f).SetUpdate(true).SetEase(Ease.OutQuad).SetLink(gameObject).OnComplete(() =>Time.timeScale = 1.0f);

        else Time.timeScale = 1.0f;
    }
}
