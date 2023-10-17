using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJTest_Upgrade_ResourceItem : Item
{
    /* class Item
     * int address          : 아이템의 주소 값
     * string itemName      : 아이템의 이름
     * int cap              : 아이템의 소지 한도
     * ItemData itemData    : 외부에서 받아올 아이템 데이터
     */

    /* itemType
     *      재료 아이템(resource item) 의 종류를 정함
     *      그냥 잡템(none), 일반 강화용 재료(normalupgrade), 속성 강화용 재료(specialupgrade)
     * specialType
     *      어떤 속성이 붙었는지
     *      일반 강화 상태(none) 얼음속성(add_ice) 불속성(add_fire) 독속성(add_poison)
     */
    public GJJTEST_RESOURCEITEMTYPE itemType;
    public GJJTEST_SPECIALPROPERTY specialType;
}
