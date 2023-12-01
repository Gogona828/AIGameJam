using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneChangeButton : MonoBehaviour
{
    [SerializeField] TweenButton tweenButton;
    [SerializeField] string LoadSceneName;
    [SerializeField] RectTransform BlackOut;

    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;

    [SerializeField] [Tooltip("???????????g?????W?V????????????????")] bool TransitionSwitch;

    void Start()
    {
        if (tweenButton != null)
        {
            

            if (TransitionSwitch)
            {
                tweenButton.onClickCallback = () => { BlackOut.DOAnchorPosY(520, 1.0f).SetLink(gameObject).OnStart(()=> { if (button1 != null || button2 != null) { button1.SetActive(false); button2.SetActive(false); } }).SetUpdate(true).SetEase(Ease.OutQuad).OnComplete(() => StartCoroutine("LoadYourAsyncScene")); };
            }
            else tweenButton.onClickCallback = () => StartCoroutine("LoadYourAsyncScene");
            Time.timeScale = 1.0f;
        }
        else
        {
            Debug.Log(gameObject.name + "??TweenButton???A?^?b?`???????????????B");
        }
    }

    IEnumerator LoadYourAsyncScene()
    {
        yield return new WaitForSeconds(1.0f);
        StartGame.shouldStartingGame = false;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(LoadSceneName);



        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
