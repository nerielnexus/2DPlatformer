using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameOption : MonoBehaviour
{
    [Header("UI관련")]
    public GameObject option;
    public GameObject soundbox;
    public GameObject endUIBox;
    public GameObject endUIBox2;
    public GameObject playerDie;

    [Header("이펙트")]
    public Transform fireEfcStock;
    public Transform waterEfcStock;
    public Transform lightEfcStock;
    public Transform darkEfcStock;

    public static InGameOption instant;

    private void Awake()
    {
        instant = this;
    }


    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex >= 2)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                option.SetActive(true);
                Time.timeScale = 0;
            }
        }

        //if (Input.GetKeyDown(KeyCode.Alpha9))
        //{
        //    endUIBox.SetActive(true);
        //}
        
        if(GameObject.Find("PlayerMan")!=null)
        {
            if (Player.instance.playerLife == 0)
            {
                playerDie.SetActive(true);
            }
        }


    }

    // 메인화면 이동 버튼
    public void ReturnMain()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        CloseOptionBox();
    }

    // 사운드 설정 버튼
    public void SoundBoxOpen()
    {
        soundbox.SetActive(true);
    }
    
    // 메뉴 창 닫기
    public void CloseOptionBox()
    {
        option.SetActive(false);
        endUIBox.SetActive(false);
        endUIBox2.SetActive(false);
        Time.timeScale = 1;
    }

    // enforce, Craft 창 닫기
    public void CloseUpgradeBox(GameObject optionBox)
    {
        optionBox.SetActive(false);
    }

    // 다음씬으로 넘어가기
    public void NextScene()
    {
        if(SceneManager.GetActiveScene().buildIndex != 6)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Time.timeScale = 1;
            endUIBox.SetActive(false);
        }
    }

    // 플레이어 죽고 게임 재시작
    public void ReTry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Player.instance.playerLife = 2;

        // 체력 50은 임시임 기존에 max를 가져와야해
        Player.instance.playerHp = 50;
    }
    
}
