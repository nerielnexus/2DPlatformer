using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 설명 : 즉발형 아이템, 플레이어 스피드 증가

public class KHS_Item_PlayerSpeedUp : UseItem
{
    // 쿨타임 조절은 ItemManager에서 관리함 (개별 코루틴을 부여하면 아이템 중복발동이 불가)
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        KHS_ItemManager.instance.CoolTime_Speed(player, gameObject, 8);
    }
}
