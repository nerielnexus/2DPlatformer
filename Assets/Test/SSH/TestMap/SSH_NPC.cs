using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SSH_NPC : MonoBehaviour
{
    // NPC 대사 창 온오프용
    public GameObject textBox;

    // NPC 대사 텍스트
    public Text talkText;

    // NPC 대사 내용 인스펙터 입력용
    public string[] talk;

    // NPC 대사 인덱스
    int talkIndex = 0;

    // NPC 대사 초기화 코루틴
    IEnumerator resetNpc;

    // NPC 메뉴 UI
    public GameObject selectService;

    private void Start()
    {
        resetNpc = ResetNPC();
    }


    // NPC 클릭시 이벤트함수
    private void OnMouseDown()
    {
        // 대사 내용이 존재할 시
        if (talk != null)
        {
            // 대사가 더 없을 시
            if (talkIndex >= talk.Length)
            {
                OpenMenu();

                StartCoroutine(resetNpc);
            }
            else
            {
                //코루틴 재귀
                StopCoroutine(resetNpc);

                resetNpc = ResetNPC();

                talkText.text = talk[talkIndex];

                textBox.SetActive(true);

                talkIndex++;

                StartCoroutine(resetNpc);
            }
            
        }
    }

    // NPC의 대사 초기화 코루틴
    IEnumerator ResetNPC()
    {
        // NPC 클릭 후 2초동안 다시 클릭 안할 시 초기화
        yield return new WaitForSeconds(2.0f);

        talkIndex = 0;
        textBox.SetActive(false);
    }


    // NPC UI메뉴 열기
    public void OpenMenu()
    {
        selectService.SetActive(true);
    }

    // NPC UI메뉴 닫기
    public void CloseMenu()
    {
        selectService.SetActive(false);
    }

    // 강화 메뉴 UI 열기
    public void OpenEnforce()
    {
        GameObject.Find("GameCanvas").transform.Find("Enforce").gameObject.SetActive(true);
    }

    // 제작 메뉴 UI 열기
    public void OpenCraft()
    {
        GameObject.Find("GameCanvas").transform.Find("CraftUI").gameObject.SetActive(true);
    }
}
