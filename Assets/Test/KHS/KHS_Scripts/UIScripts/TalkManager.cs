﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TalkManager : MonoBehaviour, IPointerDownHandler
{
    // ■■■■■■■ 큐 ■■■■■■■
    public Queue<string> NPC_Contents;

    // ■■■■■■■ 변수 ■■■■■■■
    public Text text;
    public CanvasGroup textGroup;   // UI 텍스트창의 그룹
    public bool istyping;           // 타이필 중인지 확인
    string currentContents;         // 큐안에 있는 문자열 파악

    GameObject upGradeBox;

    // ■■■■■■■ 싱글톤 ■■■■■■■
    public static TalkManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // ■■■■■■■ 큐 초기화 ■■■■■■■
    void Start()
    {
        NPC_Contents = new Queue<string>();

        upGradeBox = GameObject.Find("KHS").transform.Find("KHS_CreateItem").gameObject;
    }

    // ■■■■■■■ 큐 지속적 체크 ■■■■■■■
    void Update()
    {
        // 다음 큐의 문자열 나열 or 대화 끝
        if (text.text.Equals(currentContents))
        {
            istyping = false;
        }
    }

    // ■■■■■■■ NPC_Talk 클래스 대화 가져오기 ■■■■■■■
    public void OnTalk(string[] write)
    {
        // 큐 데이터 정리
        NPC_Contents.Clear();

        // 받아온 문자열을 큐안에 넣는다
        foreach (string text in write)
        {
            NPC_Contents.Enqueue(text);
        }

        textGroup.alpha = 1;
        textGroup.blocksRaycasts = true;  // blocksRaycats >> ray가 UI창에 막히는지(체크) 안막히는지(논체크)

        NextContents();
    }

    // ■■■■■■■ 다음 대화 출력 ■■■■■■■
    public void NextContents()
    {
        // 만약 타이핑 중이 아니라면..
        if (!istyping)
        {
            // (큐 갯수 != 0)
            if (NPC_Contents.Count != 0)
            {
                currentContents = NPC_Contents.Dequeue(); // 먼저 들어온 데이터 반환
                istyping = true;
                StartCoroutine(typing(currentContents));
            }
            else
            {
                textGroup.alpha = 0;
                textGroup.blocksRaycasts = false;
                upGradeBox.SetActive(true);
            }
        }
    }

    IEnumerator typing(string write)
    {
        text.text = "";
        foreach (char cnt in write.ToCharArray()) // ToCharArray는 문자열을 char형 배열로 변환
        {
            text.text += cnt;
            yield return new WaitForSeconds(0.05f);
        }
    }

    // IPointerDownHandler랑 같이 선언되는 메소드로, 터치가 있을시 호출댐
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!istyping)
        {
            NextContents();
        }
    }
}
