using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 설명 : 즉발형 아이템, 현재 체력의 1을 증가

public class KHS_Item_HP : UseItem
{
    int maxHp = 50;

    void Start()
    {
        if (!(Player.instance.playerHp == maxHp || Player.instance.playerHp == 0))
        {
            Player.instance.playerHp++;
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
        gameObject.tag = "Untagged";
    }
}
