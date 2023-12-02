using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CalcMoveDistance : MonoBehaviour
{
    public bool vertexUntilBeenIs = false;
    [SerializeField, Tooltip("移動距離")]
    private float moveDistance;

    [SerializeField, Tooltip("合計移動距離")]
    private float totalMoveDistance;

    [SerializeField, Tooltip("移動距離を表示")]
    private TextMeshProUGUI text;
    [SerializeField, Tooltip("赤いゴミ箱を参照する")]
    private ControlDustBox controlDustBox;

    [SerializeField, Tooltip("時間の取得")]
    private MeasureTime measureTime;

    private float pastPosition;
    private float elapsedTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        pastPosition = transform.localPosition.y;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    public void CalcDistance(float _nowPositionY)
    {
        if (controlDustBox.GetStoringQuantity() == 0 || ExitGame.hasFinishedGame) return;
        moveDistance = pastPosition - _nowPositionY;

        if (moveDistance > 0)
        {
            totalMoveDistance += moveDistance * measureTime.GetElapsedTime();
            ShowText();
            pastPosition = _nowPositionY;
        }
        else pastPosition = transform.localPosition.y;
    }

    private void ShowText()
    {
        text.text = totalMoveDistance.ToString("F0");
    }

    public float GetTotalMoveDistance()
    {
        return totalMoveDistance;
    }
}
