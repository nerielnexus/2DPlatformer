using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//강화 UI 클래스
public class EnforceBtn : MonoBehaviour
{
    [Header("강화할 장비 슬롯")]
    public Slot equipSlot;

    [Header("강화 재료 슬롯")]
    public Slot etcSlot;

    [Header("결과 슬롯 (강화한 장비)")]
    public Slot resultSlot;

    /// <summary>
    /// 강화 버튼용 메소드
    /// </summary>
    public void Enforce()
    {
        EquipData equip;

        //강화 조건
        //장비 슬롯, 재료 슬롯 아이템 존재, 결과슬롯 아이템 X
        if (equipSlot.itemOn && etcSlot.itemOn && !resultSlot.itemOn)
        {
            equip = equipSlot.item as EquipData;

            //강화가 5이상일시 강화 불가
            if (equip.enforceValue < 5)
            {
                if (etcSlot.itemOn) // 재료 슬롯에 아이템 존재 여부 확인
                {
                    int address = etcSlot.item.address;

                    //재료 슬롯의 아이템주소에 따라 강화 방식을 바꾼다.

                    //내구도수리
                    //재료아이템의 밸류에 따른 내구도 증가
                    if (200 <= address && address <= 205)
                    {
                        resultSlot.AddItemData(EnforceManager.enforceManager.RepairItem(equip, etcSlot.item));
                    }
                    //기본강화
                    //재료아이템의 밸류에 따른 장비의 밸류(공격력, 방어력) 증가
                    if (206 <= address && address <= 208)
                    {
                        resultSlot.AddItemData(EnforceManager.enforceManager.NormalEnforce(equip, etcSlot.item));
                    }
                    //속성강화
                    //재료아이템의 밸류에 따른 속성치 증가
                    if (209 <= address && address <= 216)
                    {
                        resultSlot.AddItemData(EnforceManager.enforceManager.ElementalEnforce(equip, etcSlot.item));
                    }
                }
            }

            SlotRemove();
        }

           
    }

    //강화 후 각 슬롯의 아이템제거
    public void SlotRemove()
    {
        equipSlot.RemoveItem();
        etcSlot.RemoveItem();
    }
}
