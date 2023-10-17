using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 설명 : 즉발형 아이템, 플레이어의 중력을 1로 낮춰 낙하속도를 늦춤

public class KHS_Item_NoGravity : UseItem
{
    void Start()
    {
        KHS_ItemManager.instance.CoolTime_Gravity(gameObject, 0.3f); // 플레이어 중력값 확인하고 값 변경해야함
    }

}
