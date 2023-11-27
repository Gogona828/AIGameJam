using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEListen : MonoBehaviour
{
    [SerializeField] AudioClip SE;
    [SerializeField] AudioSource audioSource;

    public void AudioLoad()
    {
        audioSource.PlayOneShot(SE);
    }
}
