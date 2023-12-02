using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEPlayer : MonoBehaviour
{
    [SerializeField] AudioSource _SEAudioSource;

    /// <summary>
    /// Audio Clip お品書き
    /// 
    /// 0:クリック音
    /// 1:マージ音
    /// 2:ゴミ箱開く
    /// 3:ゴミ箱閉じる
    /// 4:ゴミ箱に入れる
    /// 5:ブブ
    /// 
    /// リストの中身を入れ替えたり、AudioClipを消した奴は殺す。
    /// </summary>

    [SerializeField] List<AudioClip> SEList = new List<AudioClip>();

    // Update is called once per frame
    public void SEPlayRequest(int SENumber)
    {
        _SEAudioSource.PlayOneShot(SEList[SENumber]);
    }
}
