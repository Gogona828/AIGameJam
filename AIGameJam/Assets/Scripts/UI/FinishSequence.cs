using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class FinishSequence : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Finish;
    [SerializeField] GameObject Result;
    [SerializeField] RectTransform BlackOutExit;

    // Start is called before the first frame update
    void OnEnable()
    {
        Finish.DOFade(0f,1.0f).SetUpdate(true).SetLink(gameObject).SetDelay(2.0f);
        BlackOutExit.DOAnchorPosY(520, 2.0f).SetUpdate(true).SetLink(gameObject).OnComplete(() => Result.SetActive(true));
        BlackOutExit.DOAnchorPosY(-800, 1.5f).SetUpdate(true).SetLink(gameObject).SetDelay(3.0f);
    }

}
