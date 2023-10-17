using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJTest_Upgrade_EquipItemSpecial : Equip
{
    /* class Equip
     * int durability       : 아이템의 내구도
     * int value            : 아이템의 공격력, 방어력에 대응하는 변수
     * int enforceValue     : 아이템의 강화 단계
     * SpriteRenderer sr    : 아이템 이미지
     * EquipData equipData  : 외부에서 받아올 장비 아이템 데이터
     */

    /* enforceValue
     *      일반 강화 단계
     */

    /* upgradeLevelSpecial
     *      속성 강화 단계
     * valueSpecial
     *      속성 공격력/방어력 값
     *      속성 강화 단계에 따라 증가한다
     *      속성 강화에 따라 방어력이 변할지는 아직 생각은 안해봄
     */
    public int upgradeLevelSpecial;
    public int valueSpecial;
    // public string upgradeSpecialName;
    public GJJTEST_SPECIALPROPERTY upgradeSpecialName = GJJTEST_SPECIALPROPERTY.NONE;
}
