using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEPlayer : MonoBehaviour
{
    [SerializeField] AudioSource _SEAudioSource;

    /// <summary>
    /// Audio Clip ���i����
    /// 
    /// 0:�N���b�N��
    /// 1:�}�[�W��
    /// 2:�S�~���J��
    /// 3:�S�~������
    /// 4:�S�~���ɓ����
    /// 5:�u�u
    /// 
    /// ���X�g�̒��g�����ւ�����AAudioClip���������z�͎E���B
    /// </summary>

    [SerializeField] List<AudioClip> SEList = new List<AudioClip>();

    // Update is called once per frame
    public void SEPlayRequest(int SENumber)
    {
        _SEAudioSource.PlayOneShot(SEList[SENumber]);
    }
}
