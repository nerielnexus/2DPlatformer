using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//강화용 클래스
public class EnforceManager : MonoBehaviour
{
    public static EnforceManager enforceManager;

    EquipData equipItem;
    EtcItemData etcItemData;

    //싱글톤
    void Awake()
    {
        if(enforceManager == null)
        {
            enforceManager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 내구도 수리
    /// </summary>
    /// <param name="itemData"></param>
    /// <returns></returns>
    public ItemData RepairItem(EquipData equipData, ItemData etcData)
    {
        equipItem = equipData as EquipData;

        etcItemData = etcData as EtcItemData;

        equipItem.durability = equipData.durability + etcItemData.value;

        return equipItem;
    }


    /// <summary>
    /// 아이템 속성강화
    /// </summary>
    /// <param name="itemData"></param>
    /// <returns></returns>
    public ItemData ElementalEnforce(EquipData equipData, ItemData etcData)
    {
        equipItem = equipData as EquipData;

        etcItemData = etcData as EtcItemData;

        // 속성 강화
        // 주소 이름              밸류값
        // 209  번개보석의 파편 : value 5
        // 210  번개보석        : value 10
        // 211  용의 피         : value 5
        // 212  불의 눈         : value 10
        // 213  얼음결정        : value 5
        // 214  얼음핵          : value 10
        // 215  부식            : value 5
        // 216  끈적액체        : value 10

        if (etcItemData.address == 209 || etcItemData.address == 210)
        {
            equipItem.light += etcItemData.value;
        }

        if (etcItemData.address == 211 || etcItemData.address == 212)
        {
            equipItem.fire += etcItemData.value;
        }

        if (etcItemData.address == 213 || etcItemData.address == 214)
        {
            equipItem.water += etcItemData.value;
        }

        if (etcItemData.address == 215 || etcItemData.address == 216)
        {
            equipItem.dark += etcItemData.value;
        }


        //강화수치 +1
        equipItem.enforceValue++;


        return equipItem;
    }


    /// <summary>
    /// 일반강화
    /// </summary>
    /// 재료아이템이 필요
    public ItemData NormalEnforce(EquipData equipData, ItemData etcData)
    {
        equipItem = equipData as EquipData;

        etcItemData = etcData as EtcItemData;

        //강화수치 +1
        equipItem.enforceValue++;
        //아이템밸류 증가
        equipItem.value += etcItemData.value;

        return equipItem;
    }

}