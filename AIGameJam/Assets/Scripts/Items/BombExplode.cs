using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BombExplode : MonoBehaviour
{
    [SerializeField] GameObject explodeAnim;
    [SerializeField] CameraShaker cameraShaker;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip se;

    private GameObject instancePrefab;


    public void BombExplodeEffect(GameObject closestItem)
    {
        cameraShaker.CameraShake();
        instancePrefab = Instantiate(explodeAnim,transform);
        instancePrefab.transform.position = closestItem.transform.position;

        audioSource.PlayOneShot(se);




        StartCoroutine("WaitFiveSeconds");


    }

    IEnumerator WaitFiveSeconds()
    {
        yield return new WaitForSeconds(5f);
        Destroy(instancePrefab);
        yield return null;
    }
}
