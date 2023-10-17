using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
     CoolTime 함수를 나눈 이유는 두개의 포션은 다른 역할을 하므로 중복으로 적용 시키기 위해서
 */

public class KHS_ItemManager : MonoBehaviour
{
    public static KHS_ItemManager instance;
    IEnumerator enumerator_speed;
    IEnumerator enumerator_jump;
    IEnumerator enumerator_gravity;
    IEnumerator enumerator_bigbody;

    void Awake()
    {
        instance = this;
    }

    // 스피드 쿨타임 확인 함수
    public void CoolTime_Speed(GameObject obj, GameObject item, int speed)
    {
        if (enumerator_speed != null)
        {
            StopCoroutine(enumerator_speed);
        }

        item.GetComponent<SpriteRenderer>().sprite = null;
        item.tag = "Untagged";

        enumerator_speed = SpeedPotion(obj, item, speed);
        StartCoroutine(enumerator_speed);
    }

    // 점프 쿨타임 확인 함수
    public void CoolTime_Jump(GameObject item, int jump)
    {
        if (enumerator_jump != null)
        {
            StopCoroutine(enumerator_jump);
        }
        item.GetComponent<SpriteRenderer>().sprite = null;
        item.tag = "Untagged";

        enumerator_jump = JumpPotion(item, jump);
        StartCoroutine(enumerator_jump);
    }

    // 중력 쿨타임 확인 함수
    public void CoolTime_Gravity(GameObject item, float gravity)
    {
        if (enumerator_gravity != null)
        {
            StopCoroutine(enumerator_gravity);
        }

        item.GetComponent<SpriteRenderer>().sprite = null;
        item.tag = "Untagged";

        enumerator_gravity = GravityPotion(item, gravity);
        StartCoroutine(enumerator_gravity);
    }

    // 플레이어 스케일 확인 함수
    public void CoolTime_BigBody(GameObject item)
    {
        if (enumerator_bigbody != null)
        {
            StopCoroutine(enumerator_bigbody);
        }

        item.GetComponent<SpriteRenderer>().sprite = null;
        item.tag = "Untagged";

        enumerator_bigbody = SmallBodyPotion();
        StartCoroutine(enumerator_bigbody);
    }

    IEnumerator SpeedPotion(GameObject obj, GameObject item, int speed)
    {
        if (obj.tag =="Player")
        {
            obj.GetComponent<Player>().maxSpeed = speed;
            yield return new WaitForSeconds(3f);
            obj.GetComponent<Player>().maxSpeed = 5;
        }
        
        item.SetActive(false);    
    }

    IEnumerator JumpPotion(GameObject item, int jump)
    {
        Player.instance.jumpMaxCnt = jump;
        yield return new WaitForSeconds(4f);
        Player.instance.jumpMaxCnt = 3;
        item.SetActive(false);
    }

    IEnumerator GravityPotion(GameObject item, float gravity)
    {
        Player.instance.GetComponent<Rigidbody2D>().gravityScale = gravity;
        yield return new WaitForSeconds(4f);
        Player.instance.GetComponent<Rigidbody2D>().gravityScale = 1;
        item.SetActive(false);
    }

    IEnumerator SmallBodyPotion()
    {
        yield return new WaitForSeconds(4f);
        Player.instance.GetComponent<Transform>().localScale = new Vector3(1,1,1);
    }
}
