using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MeasureTime : MonoBehaviour
{
    [SerializeField, Tooltip("時間表示")]
    private TextMeshProUGUI text;
    [SerializeField, Tooltip("時間表示")]
    private int limitTime = 60;

    private int minute = 0;
    private float seconds = 0;
    private float oldSeconds = 0;
    private float remainingTime;

    // Start is called before the first frame update
    void Start()
    {
        minute = limitTime / 60;
        seconds = limitTime - minute * 60;
        remainingTime = limitTime;
        ShowTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (!StartGame.shouldStartingGame) return;
        if (remainingTime <= 0) {
            ExitGame.instance.EndGame();
            return;
        }

        remainingTime = minute * 60 + seconds;
        remainingTime -= Time.deltaTime;

        minute = (int)remainingTime / 60;
        seconds = remainingTime - minute * 60;

        ShowTime();
        oldSeconds = seconds;
    }

    private void ShowTime()
    {
        if ((int)seconds != (int)oldSeconds) {
            text.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        }
    }
}
