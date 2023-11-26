using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseSkill : MonoBehaviour
{
    public static ReleaseSkill instance;
    
    [SerializeField, Tooltip("スキルの解放数")]
    private int skillReleaseNum = 0;
    [SerializeField, Tooltip("スキルの")]

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this);
    }

    public void AddSkillReleaseCount()
    {
        skillReleaseNum++;
        ActivateSkill();
    }

    private void ActivateSkill()
    {
        switch (skillReleaseNum)
        {
            case 1:
                Debug.Log("ストック可能！");
                break;
            case 2:
                Debug.Log("レーン速度一時アップ！");
                break;
            case 3:
                Debug.Log("ゴミ袋自体をプレゼント！");
                break;
            default:
                Debug.Log("もう出すものはない");
                break;
        }
    }
}
