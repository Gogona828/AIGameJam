using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionOnOff : MonoBehaviour
{
    public void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }
}
