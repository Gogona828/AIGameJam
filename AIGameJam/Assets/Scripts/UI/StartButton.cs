using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class StartButton : MonoBehaviour
{
    [SerializeField] TweenButton tweenButton;
    [SerializeField] CanvasGroup StartBoard;
    [SerializeField] TextMeshProUGUI ReadyGo;

    void Start()
    {
        if (tweenButton != null)
        {
            tweenButton.onClickCallback = StartButtonCallBack; 
        }
        else tweenButton.onClickCallback = () => StartCoroutine("LoadYourAsyncScene");

    }


    void StartButtonCallBack()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(StartBoard.DOFade(0, 1.0f).SetLink(gameObject).SetUpdate(true).SetEase(Ease.OutQuad).SetDelay(0.5f)
            .OnComplete(() => { StartBoard.gameObject.SetActive(false); ReadyGo.gameObject.SetActive(true); }))

                .Append(ReadyGo.DOFade(0, 2.0f).SetLink(gameObject).SetUpdate(true).SetEase(Ease.OutQuad).SetDelay(1.0f)
            .OnComplete(() => ReadyGo.text = "Go!!"))

                .Append(ReadyGo.DOFade(1.0f, 0.3f).SetLink(gameObject).SetUpdate(true).SetEase(Ease.OutQuad))

                .Append(ReadyGo.DOFade(0f, 0.2f).SetLink(gameObject).SetUpdate(true).SetEase(Ease.OutQuad).SetDelay(0.5f)
                .OnComplete(() => {
                    ReadyGo.gameObject.SetActive(false);
                    Time.timeScale = 1.0f;
                }));
    }
}


