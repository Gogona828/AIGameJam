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

    private float pastPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        pastPosition = transform.localPosition.y;
    }

    public void CalcDistance(float _nowPositionY)
    {
        if (controlDustBox.GetStoringQuantity() == 0) return;
        moveDistance = pastPosition - _nowPositionY;

        if (moveDistance > 0)
        {
            totalMoveDistance += moveDistance;
            ShowText();
            pastPosition = _nowPositionY;
        }
        else pastPosition = transform.localPosition.y;
    }

    private void ShowText()
    {
        text.text = totalMoveDistance.ToString("F0");
    }
}
