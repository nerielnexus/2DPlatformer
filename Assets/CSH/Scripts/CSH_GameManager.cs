using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSH_GameManager : MonoBehaviour
{
    public Camera mainCam; //카메라
    public Transform player; //플레이어

    public int playerLife = 2; //플레이어 목숨
    public Text lifeText; //목숨 텍스트

    public int playerHp = 10; //플레이어 체력
    public Image hpBar; //체력바

    Vector3 spawnPos; //스폰포인트 벡터값


    public static CSH_GameManager instance;
    private void Awake()
    {
        instance = this;
        //spawnPoint = player;
        spawnPos = player.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        lifeText.text = playerLife.ToString();
        //hpBar.fillAmount = playerHp * 0.1f;
        mainCam.transform.position = new Vector3(player.position.x, player.position.y + 2f, -10);
    }

    // Update is called once per frame
    void Update()
    {
        mainCam.transform.position = new Vector3(player.position.x, mainCam.transform.position.y, -10);

        hpBar.fillAmount = playerHp * 0.1f;
    }

    //스테이지 클리어
    public void GoalIn()
    {
        Debug.Log("스테이지 클리어");
    }

    //스폰포인트를 덮어씀
    public void SpawnPointOverload()
    {
        spawnPos = player.position;
    }

    //플레이어 데미지 처리
    public void PlayerDamage(int dmg)
    {
        Debug.Log("데미지를 입고, 무적상태에 들어갑니다");

        playerHp = playerHp - dmg;
        //hpBar.fillAmount = playerHp * 0.1f;
        if(playerHp <= 0)
        {
            RespawnPlayer();
        }
    }

    //플레이어를 리스폰
    public void RespawnPlayer()
    {
        //목숨 감소 처리
        playerLife--;
        lifeText.text = playerLife.ToString();

        if(playerLife >= 0) //플레이어의 목숨이 0 이상이면
        {
            //스폰 포인트에 리스폰
            player.position = spawnPos;
            playerHp = 10;
            
        }
        else
        {
            Gameover();
        }
    }

    //게임오버
    public void Gameover()
    {
        player.gameObject.SetActive(false);
        lifeText.text = "";
        Debug.Log("게임오버");
    }
}
