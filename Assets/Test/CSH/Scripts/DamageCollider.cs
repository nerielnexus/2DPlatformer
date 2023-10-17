using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    //플레이어에게 가하는 데미지값
    public int dmgValue = 1;

    [Header("공격하는 몬스터")]
    public Monster attackerMonster;

    [HideInInspector]
    public int attackerAtk; //공격자 공격력
    [HideInInspector]
    public int attackerFire; //공격자 불속성치
    [HideInInspector]
    public int attackerWater; //공격자 물속성치
    [HideInInspector]
    public int attackerLight; //공격자 빛속성치
    [HideInInspector]
    public int attackerDark; //공격자 어둠속성치

    [HideInInspector]
    public int defenceDef; //방어자 방어력
    [HideInInspector]
    public int defenceFire; //방어자 불내성치
    [HideInInspector]
    public int defenceWater; //방어자 물내성치
    [HideInInspector]
    public int defenceLight; //방어자 빛내성치
    [HideInInspector]
    public int defenceDark; //방어자 어둠내성치

    Transform fireEffect;
    Transform waterEffect;
    Transform lightEffect;
    Transform darkEffect;

    private void Awake()
    {
        fireEffect = GameManager.instance.fireEffect;
        waterEffect = GameManager.instance.waterEffect;
        lightEffect = GameManager.instance.lightEffect;
        darkEffect = GameManager.instance.darkEffect;
    }

    void DmgValueCalcul(Collider2D collision) //데미지 계산식
    {
        int normalValue = attackerAtk - defenceDef;
        int fireValue = attackerFire - defenceFire;
        int waterValue = attackerWater - defenceWater;
        int lightValue = attackerLight - defenceLight;
        int darkValue = attackerDark - defenceDark;

        //해당 속성치가 해당 내성치를 넘지 못하면 해당 속성의 데미지는 데미지 총량에 가산되지 않는다
        if (normalValue <= 0) normalValue = 0;
        if (fireValue <= 0) fireValue = 0;
        else Instantiate(fireEffect, collision.transform.position, Quaternion.identity);
        if (waterValue <= 0) waterValue = 0;
        else Instantiate(waterEffect, collision.transform.position, Quaternion.identity);
        if (lightValue <= 0) lightValue = 0;
        else Instantiate(lightEffect, collision.transform.position, Quaternion.identity);
        if (darkValue <= 0) darkValue = 0;
        else Instantiate(darkEffect, collision.transform.position, Quaternion.identity);

        dmgValue = normalValue + fireValue + waterValue + lightValue + darkValue;
    }

    //'플레이어'가 적이나 함정으로부터 데미지를 입음
    void PlayerDamage(Collider2D collision)
    {
        if (collision.tag == "Player" && (this.transform.tag == "Enemy" || this.transform.tag == "Trap") ) //플레이어가 적에게 데미지를 입으면
        {
            if(transform.tag == "Enemy")
            {
                ValueMatchEnToPl(collision);
            }

            if (collision.GetComponent<Player>().playerShield >= dmgValue) //실드값이 데미지보다 많으면
            {
                collision.GetComponent<Player>().playerShield -= dmgValue; //실드만 깎는다
                collision.GetComponent<Player>().armor.durability -= dmgValue; //실드만 깎는다
            }
            else if (collision.GetComponent<Player>().playerShield < dmgValue) //실드값이 데미지보다 적으면
            {
                int leftValue = Mathf.Abs(collision.GetComponent<Player>().playerShield - dmgValue); //실드를 깎고 남은 데미지값
                collision.GetComponent<Player>().playerShield = 0; //데미지값이 실드값을 넘으므로 실드값을 0으로
                collision.GetComponent<Player>().armor.durability -= leftValue;
                collision.GetComponent<Player>().playerHp = collision.GetComponent<Player>().playerHp - leftValue; //체력에서 남는 데미지값을 깎는다
            }

            //Transform efc = Instantiate(nowEffect);
            //efc.position = collision.transform.position;

            //collision.transform.position
        }
    }

    //'플레이어'가 적으로부터 데미지를 입을 때의 수치 적용
    void ValueMatchEnToPl(Collider2D collision)
    {
        if(attackerMonster == true)
        {
            Monster _monster = attackerMonster; //공격을 입힐 몬스터의 Monster 클래스
            attackerAtk = _monster.attackValue;
            attackerFire = _monster.fireAtk;
            attackerWater = _monster.waterAtk;
            attackerLight = _monster.lightAtk;
            attackerDark = _monster.darkAtk;
        }

        defenceDef = collision.GetComponent<Player>().playerDef; //플레이어의 무기는 피격대상에서 제외될 필요가 있음
        defenceFire = collision.GetComponent<Player>().fireProof;
        defenceWater = collision.GetComponent<Player>().waterProof;
        defenceLight = collision.GetComponent<Player>().lightProof;
        defenceDark = collision.GetComponent<Player>().darkProof;

        DmgValueCalcul(collision);
    }


    //'적이' 플레이어(레이어)로부터 데미지를 입음
    void EnemyDamage(Collider2D collision)
    {
        if (collision.tag == "Enemy" && this.transform.tag == "Item")
        {
            if (collision.GetComponent<Projectile>()) //투사체는 파괴
            {
                Destroy(collision.gameObject);
            }
            else if (collision.GetComponent<Monster>()) //적에게는 데미지
            {
                if(transform.gameObject.layer == LayerMask.GetMask("Player"))
                {
                    ValueMatchPlToEn(collision);
                }
                else if(transform.gameObject.layer == LayerMask.GetMask("Item"))
                {

                }                

                collision.SendMessage("HitAndDamaged", dmgValue);
            }
        }
    }
    //'적이' 플레이어로부터 데미지를 입을 때의 수치 적용
    void ValueMatchPlToEn(Collider2D collision)
    {
        EquipData equipData = Player.instance.weapon;
        attackerAtk = equipData.value;
        attackerFire = equipData.fire;
        attackerWater = equipData.water;
        attackerLight = equipData.light;
        attackerDark = equipData.dark;

        defenceDef = collision.GetComponent<Monster>().defenceValue;
        defenceFire = collision.GetComponent<Monster>().fireProof;
        defenceWater = collision.GetComponent<Monster>().waterProof;
        defenceLight = collision.GetComponent<Monster>().lightProof;
        defenceDark = collision.GetComponent<Monster>().darkProof;

        DmgValueCalcul(collision);
    }

    //중립이 플레이어나 함정으로부터 데미지를 입음
    void NeutralDamage(Collider2D collision)
    {
        if (collision.tag == "Neutral" && (this.transform.tag == "Trap" || this.transform.tag == "Player"))
        {
            collision.SendMessage("HitAndDamaged", dmgValue);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerDamage(collision);

        EnemyDamage(collision);

        NeutralDamage(collision);
    }
}
