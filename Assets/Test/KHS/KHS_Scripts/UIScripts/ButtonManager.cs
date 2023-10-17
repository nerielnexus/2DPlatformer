using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject gameCanvas;
    public GameObject stageGame;

    public GameObject startMenu;
    //StageManager stagemanager;

    SoundManager soundManager;
    public Button[] buttons;

    void Start()
    {
        gameCanvas = GameObject.Find("GameCanvas");
        //stagemanager = GameObject.Find("StageManager").GetComponent<StageManager>();
        soundManager = GameObject.Find("SoundManager(●)").GetComponent<SoundManager>();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(() => ClickButtonSound());
        }
    }

    // 메뉴창 열기
    public void ButtonOpenMenu(GameObject menu)
    {
        menu.SetActive(true);
    }

    // 메뉴창 닫기
    public void ButtonCloseMenu(GameObject menu)
    {
        menu.SetActive(false);
    }

    // 다음씬 이동
    public void NextScene(int sceneNum)
    {
        LoadManager.LoadScene(sceneNum);
    }

    // 강화박스 열기
    public void UpgradeBoxOpen(string objectname)
    {
        gameCanvas.transform.Find(objectname).gameObject.SetActive(true);
    }

    // 강화박스 닫기
    public void UpgradeBoxClose(string objectname)
    {
        gameCanvas.transform.Find(objectname).gameObject.SetActive(false);
    }

    // GameStartButton에서 이어한 부분이 없을 경우 StageMenu 열음
    //public void StageCheck()
    //{
    //    bool existence = false;
        
    //    for(int i=0; i< stagemanager.stage.Length; i++)
    //    {
    //        // 클리어한 씬이 있다면
    //        if (stagemanager.stage[i])
    //        {
    //            existence = true;
    //            break;
    //        }
    //    }
        
    //    if(existence)
    //    {
    //        startMenu.SetActive(true); // StartMenu 활성화
    //    }
    //    else
    //    {
    //        stageGame.SetActive(true);
    //    }
    //}

    public void ClickButtonSound()
    {
        soundManager.ClickSound();
    }
}
