using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class TweenButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] float ScaleMin = 0.9f;
    [SerializeField] float ScaleMax = 1.0f;


    public System.Action onClickCallback;

    [SerializeField] private CanvasGroup _canvasGroup;

    public void OnPointerClick(PointerEventData eventData)
    {
        onClickCallback?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(ScaleMin, 0.24f).SetEase(Ease.OutCubic).SetUpdate(true).SetLink(gameObject);
        _canvasGroup.DOFade(0.8f, 0.24f).SetEase(Ease.OutCubic).SetUpdate(true).SetLink(gameObject);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(ScaleMax, 0.24f).SetEase(Ease.OutCubic).SetUpdate(true).SetLink(gameObject);
        _canvasGroup.DOFade(1f, 0.24f).SetEase(Ease.OutCubic).SetUpdate(true).SetLink(gameObject);
    }
}
