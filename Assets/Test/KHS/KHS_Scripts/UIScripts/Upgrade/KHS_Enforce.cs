using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KHS_Enforce : MonoBehaviour
{
    public Slot equip;
    public Slot etcitem;
    public Slot result;
    public Text percentText;

    EquipData equipitem;
    int percentTwo, percent;
    public static bool onlyOne = false;

    void Update()
    {
        if(equip.itemOn && etcitem.itemOn)
        {
            equipitem = equip.item as EquipData;

            if (!onlyOne)
            {
                percent = 100;

                // 장비강화수치에 따른 확률 계산
                // ■■ 0 : 100% ■■ 1 : 50% ■■ 2 : 25% ■■ 3 : 12% ■■ 4 : 6% ■■ 5 : 강화불가 ■■
                for (int i = 0; i < equipitem.enforceValue; i++)
                {
                    percentTwo = percent / 2;
                    percent = percentTwo;
                }

                onlyOne = true;
            }
        }
        else
        {
            onlyOne = false;
            percent = 0;
        }

        // 퍼센트 텍스트 변경
        percentText.text = percent + "%";
    }

    public void Enforce()
    { 
        int success = Random.Range(1, 101); // 1~100(포함)

        // 장비와 재료 슬롯에 아이템이 올라와져 있는지 확인
        if (equip.itemOn && etcitem.itemOn && !result.itemOn)
        {
            // 5단계 미만만 강화가능
            if (equipitem.enforceValue < 5)
            {
                // 강화 성공시
                if (success <= percent)
                {
                    equipitem.enforceValue++;

                    // 아이템 주소에 따른 강화목록
                    if (200 <= etcitem.item.address && etcitem.item.address <= 205)
                    {
                        // 내구도 강화
                        result.AddItemData(UpDurability(equipitem, etcitem.item as EtcItemData));
                    }
                    else if (206 <= etcitem.item.address && etcitem.item.address <= 208)
                    {
                        // 일반 강화
                        result.AddItemData(UpEnforce(equipitem, etcitem.item as EtcItemData));
                    }
                    else if (209 <= etcitem.item.address && etcitem.item.address <= 216)
                    {
                        // 속성능력 미세 강화
                        result.AddItemData(UpProperty(equipitem, etcitem.item as EtcItemData));
                    }
                }
            }

            // 장비, 아이템 제거
            equip.RemoveItem();
            etcitem.RemoveItem();
            percent = 0;
            onlyOne = false;
        }
    }

    ItemData UpDurability(EquipData equip, EtcItemData item)
    {
        equip.durability += item.value;

        return equip;
    }

    ItemData UpEnforce(EquipData equip, EtcItemData item)
    {
        equip.value += item.value;

        return equip;
    }

    ItemData UpProperty(EquipData equip, EtcItemData item)
    {
        // 전기속성 강화
        if (item.address == 209 || item.address == 210)
        {
            equip.light += item.value;
        }

        // 불속성 강화
        if (item.address == 211 || item.address == 212)
        {
            equip.fire += item.value;
        }

        // 얼음속성 강화
        if (item.address == 213 || item.address == 214)
        {
            equip.water += item.value;
        }

        // 독속성 강화
        if (item.address == 215 || item.address == 216)
        {
            equip.dark += item.value;
        }

        return equip;
    }
}
