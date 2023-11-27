using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowResultText : MonoBehaviour
{
    [SerializeField] private CalcMoveDistance calcMoveDistance;

    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private GameObject[] medals;
    // Start is called before the first frame update
    void Start()
    {
        text.text = calcMoveDistance.GetTotalMoveDistance().ToString("0#,0");
        if (calcMoveDistance.GetTotalMoveDistance() >= 50000) medals[0].SetActive(true);
        if (calcMoveDistance.GetTotalMoveDistance() >= 1000000) medals[1].SetActive(true);
        if (calcMoveDistance.GetTotalMoveDistance() >= 1000000000) medals[2].SetActive(true);
    }
}
