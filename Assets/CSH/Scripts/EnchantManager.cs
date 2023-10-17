using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnchantManager : MonoBehaviour
{
    GameObject player;
    int playerLevel;

    int targetItem;

    public Slider expBar;
    Text levelText;

    public Button[] enchantBtn = new Button[3];
    public Button changeBtn;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        levelText = expBar.transform.GetChild(3).GetComponent<Text>();
    }


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(player.name);
        targetItem = 0; //슬롯 빈 상태로 시작
        for (int i = 0; i < enchantBtn.Length; i++)
        {
           enchantBtn[i].transform.GetChild(0).GetComponent<Text>().text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        ExpBar();
        GetEnchantLevel();

        

        TestFunc();
    }

    //테스트용 함수
    void TestFunc()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("무기 올려짐");
            targetItem = 1;
            GetEnchantType();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("장비 올려짐");
            targetItem = 2;
            GetEnchantType();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("슬롯 비워짐");
            targetItem = 0;
            GetEnchantType();
        }

        if(targetItem == 0)
        {
            changeBtn.enabled = false;
            changeBtn.transform.GetChild(0).GetComponent<Text>().text = "";

            for(int i = 0; i < enchantBtn.Length; i++)
            {
                enchantBtn[i].enabled = false;
            }
        }
        else
        {
            changeBtn.enabled = true;
            changeBtn.transform.GetChild(0).GetComponent<Text>().text = "Change";

            for (int i = 0; i < enchantBtn.Length; i++)
            {
                enchantBtn[i].enabled = true;
            }
        }
    }

    //플레이어 경험치바
    void ExpBar()
    {
        playerLevel = player.GetComponent<PlayerExp>().exp / 100;
        int leftExp = player.GetComponent<PlayerExp>().exp % 100;

        levelText.text = playerLevel.ToString();
        expBar.value = (float)leftExp / 100f;
    }

    //인챈트 레벨
    //1~15, 16~49, 50~
    int GetEnchantLevel()
    {
        if(playerLevel >= 50)
        {
            return 3;
        }
        else if(50 > playerLevel && playerLevel >= 16)
        {
            return 2;
        }
        else if(16 > playerLevel && playerLevel >= 1)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    //무기 인챈트      / 장비 인챈트
    //관통, 밀침, 출혈 / 방벽, 가시, 회복
    public void GetEnchantType()
    {
        if(targetItem == 1)
        {
            for (int i = 0; i < enchantBtn.Length; i++)
            {
                int rand = Random.Range(0, 3);

                string typeName = "";
                if (rand == 0) typeName = "관통";
                else if (rand == 1) typeName = "밀침";
                else typeName = "출혈";

                enchantBtn[i].transform.GetChild(0).GetComponent<Text>().text = typeName + " Lv" + GetEnchantLevel();
            }
        }
        else if(targetItem == 2)
        {
            for (int i = 0; i < enchantBtn.Length; i++)
            {
                int rand = Random.Range(0, 3);

                string typeName = "";
                if (rand == 0) typeName = "방벽";
                else if (rand == 1) typeName = "가시";
                else typeName = "회복";

                enchantBtn[i].transform.GetChild(0).GetComponent<Text>().text = typeName + " Lv" + GetEnchantLevel();
            }
        }
        else if(targetItem == 0)
        {
            for (int i = 0; i < enchantBtn.Length; i++)
            {
                enchantBtn[i].transform.GetChild(0).GetComponent<Text>().text = "";
            }
        }
    }

    public void Enchanting()
    {
        int useExp = 0;

        if(GetEnchantLevel() == 3)
        {
            useExp = 2000;
        }
        else if (GetEnchantLevel() == 2)
        {
            useExp = 1000;
        }
        else if (GetEnchantLevel() == 1)
        {
            useExp = 100;
        }

        player.GetComponent<PlayerExp>().LostExp(useExp);


        targetItem = 0;
        GetEnchantType();
    }
}
