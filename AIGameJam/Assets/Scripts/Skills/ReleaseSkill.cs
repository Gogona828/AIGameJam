using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseSkill : MonoBehaviour
{
    public static ReleaseSkill instance;
    
    [SerializeField, Tooltip("スキルの解放数")]
    private int skillReleaseNum = 0;

    [SerializeField, Tooltip("スキルの画像")]
    private CanvasGroup[] skillImages;

    [SerializeField, Tooltip("スキルの能力")]
    private SaveItem saveItem;

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        for (int i = 0; i < skillImages.Length; i++) {
            skillImages[i].alpha = 0;
        }
    }

    public void AddSkillReleaseCount()
    {
        skillReleaseNum++;
        if (skillImages.Length < skillReleaseNum) return;
        ActivateSkill();
    }

    private void ActivateSkill()
    {
        skillImages[skillReleaseNum - 1].alpha = 1;
        switch (skillReleaseNum)
        {
            case 1:
                saveItem.isSkillReleased = true;
                saveItem.gameObject.tag = "SavePosition";
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
