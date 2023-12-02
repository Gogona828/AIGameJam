using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSoueceSet : MonoBehaviour
{
    static public float BGMVolume;
    static public float SEVolume;

    [SerializeField] AudioSource audioSource;
    [SerializeField] Slider slider;
    // Start is called before the first frame update
    void Update()
    {
        audioSource.volume = slider.value;
    }

    
}
