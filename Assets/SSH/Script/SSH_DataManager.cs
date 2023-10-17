using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//몬스터 데이터 클래스
[System.Serializable]
public class MonsterData
{
    // 이름
    public string name;

    //고유번호
    public int address;

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
}

//몬스터 데이터 리스트 클래스
public class MonsterDataList
{
    public MonsterData[] monsterList;
}


//데이터 매니저
public class SSH_DataManager : MonoBehaviour
{
    //싱글톤
    public static SSH_DataManager dataManager;

    //몬스터 베이스 데이터 json
    public TextAsset data;

    public MonsterDataList monsterDataList = new MonsterDataList();
    public Dictionary<int, MonsterData> monDataDic = new Dictionary<int, MonsterData>();


    //몬스터 기타 데이터 SO
    public SSH_MonSO monsterDataEx;

    public Dictionary<int, SSH_MonSO.MonsterDataEx> monDataExDic = new Dictionary<int, SSH_MonSO.MonsterDataEx>();


    void Awake()
    {
        dataManager = this;
        SetData();
        SetDataEx();
    }

    //몬스터 베이스 데이터 입력
    void SetData()
    {
        monsterDataList = JsonUtility.FromJson<MonsterDataList>(data.text);

        foreach (var monData in monsterDataList.monsterList)
        {
            monDataDic.Add(monData.address, monData);
        }
    }

    //몬스터 기타 데이터 입력
    void SetDataEx()
    {
        foreach (var monDataEx in monsterDataEx.monDataEx)
        {
            monDataExDic.Add(monDataEx.address, monDataEx);
        }
    }
}
