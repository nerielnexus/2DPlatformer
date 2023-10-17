using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 설명 : 즉발형 아이템, 무적상태로 체력감소 안함

public class KHS_Item_Invincibility : UseItem
{
    bool stopHP = false;
    int hp;

    void Start()
    {
        hp = Player.instance.playerHp;
        stopHP = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
        gameObject.tag = "Untagged";

        StartCoroutine(ItemTime());
    }

    void Update()
    {
        if (stopHP == true)
        {
            Player.instance.playerHp = 10;
        }
    }

    IEnumerator ItemTime()
    {
        yield return new WaitForSeconds(5f);
        stopHP = false;
        Player.instance.playerHp = hp;
    }

}
