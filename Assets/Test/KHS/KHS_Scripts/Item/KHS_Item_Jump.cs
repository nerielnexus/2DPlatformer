using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 설명 : 소지형 아이템, 일정시간 동안 더블점프(기본점프 포함) 가능하며 4초뒤 다시 1회로 변함

public class KHS_Item_Jump : UseItem
{
    void Start()
    {
        Player.instance.jumpMaxCnt++;
        KHS_ItemManager.instance.CoolTime_Jump(gameObject, 4);
    }
}
