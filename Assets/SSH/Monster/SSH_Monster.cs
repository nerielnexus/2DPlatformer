using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSH_Monster : MonoBehaviour
{
    //몬스터 베이스 정보 참조
    public MonsterData monsterData;

    //몬스터 기타 정보 참조
    public SSH_MonSO.MonsterDataEx monsterDataEx;

    [Header("변경주소값")]
    //고유번호 (값변경할것)
    public int address;

    [Header("몬스터 베이스 정보 확인 및 변경")]
    // 이름
    public string monsterName;

    // 체력   
    public int hp;

    // 마나량
    public int mp;

    // 넉백정도와 힘
    public int power;

    // 이동유무
    public bool move;

    // 이동속도
    public int speed;

    // 공격유무
    public bool isAttack;

    // 공격 데미지
    public int attackDamage;


    //몬스터 기타정보들
    SSH_MonMove MonsterMove;
    Animator MonsterAni;


    //데이터매니저로부터 데이터 가져오기
    void DataBase()
    {
        if(SSH_DataManager.dataManager.monDataDic[address] != null)
        {
            monsterData = SSH_DataManager.dataManager.monDataDic[address];
            monsterDataEx = SSH_DataManager.dataManager.monDataExDic[address];
            SetDataBase();
        }
        else
        {
            Debug.Log("몬스터 데이터가 없습니다.");
        }
    }

    //기본 데이터 셋팅
    void SetDataBase()
    {
        //베이스정보
        monsterName = monsterData.name;
        hp = monsterData.hp;
        mp = monsterData.mp;
        power = monsterData.power;
        move = monsterData.move;
        speed = monsterData.speed;
        isAttack = monsterData.isAttack;
        attackDamage = monsterData.attackDamage;

        //기타정보
        MonsterMove = monsterDataEx.monMove;
        MonsterAni = monsterDataEx.monAni;

        //이미지변환
        SpriteRenderer monSprite = GetComponent<SpriteRenderer>();
        monSprite.sprite = monsterDataEx.monSprite;

        MonsterMove.pos = this.transform;
    }


    void Start()
    {
        DataBase();
    }

    //이동 = Ex 데이터 이동함수
    void Move()
    {
        MonsterMove.Move();
    }

    
    void Update()
    {
        Move();
    }

    
}
