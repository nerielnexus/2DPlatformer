using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    /**********************
    [설명]
    : 다음씬으로 넘어가야할 스크립트 부분에 LoadManager.LoadScene(희망씬 넘버) 추가하면 끝
    ***********************/

    static int sceneNum;

    [SerializeField]
    Image fill;

    void Start()
    {
        StartCoroutine(loadSceneProcess());
    }

    public static void LoadScene(int loadsceneNum)
    {
        sceneNum = loadsceneNum;
        SceneManager.LoadScene(1);
    }

    IEnumerator loadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneNum);
        op.allowSceneActivation = false;

        float timer = 0f;
        while(!op.isDone)
        {
            yield return null;

            // 90% 이전까지는 로딩진행도에 따라 그래프바를 채움
            if(op.progress < 0.9f)
            { 
                fill.fillAmount = op.progress;
            }
            // 90% 이상부터는 lerp(선형보간)을 이용해 부드럽게 그래프바를 채우고 씬을 넘어감
            else
            {
                timer += Time.unscaledDeltaTime;
                fill.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if(fill.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
