using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*  special upgrade 방식
 * 
 *  1. 강화 방식
 *      - 일반 강화 (normal upgrade)
 *          무기의 일반 대미지가 증가한다
 *          물리/속성 대미지가 있는 게임에서, 물리 대미지만 증가하는 강화와 같은 맥락
 *      - 특수 강화 (special upgrade)
 *          무기에 속성을 부여한다
 *          무기의 속성 대미지가 증가하고, 그 만큼의 기본 대미지가 감소한다
 *          즉 플레이어는 일반 강화를 충분히 한 후, 특수 강화를 통해 더 높은 대미지를 노릴 수 있다
 *  
 *  2. 실제 강화 메커니즘 (임시)
 *      - 일반 강화
 *          일반 강화 재료를 투입해서 일반 강화를 진행한다
 *          일반 강화를 1번 하면, 일반 강화 단계가 1점 증가한다
 *          이후 일반 강화 단계에 비례해 일반 대미지가 증가한다
 *          ex.
 *              일반+0 -> 일반+1 : 일반 대미지 +1
 *              일반+1 -> 일반+2 : 일반 대미지 +2
 *              일반+2 -> 일반+3 : 일반 대미지 +3
 *              ...
 *              
 *      - 특수 강화
 *          강화하고 싶은 속성을 선택해서, 맞는 속성 재료를 투입하고 특수 강화를 진행한다
 *          특수 강화도 일반 강화처럼 특수 강화 단계가 1점 증가하고, 특수 강화 단계에 비례해 특수 대미지가 증가한다.
 *          ex.
 *              특수+0 -> 특수+1 : 특수 대미지 +1
 *              ...
 *          특수 강화는 특수 강화 단계를 1점 증가시키면서, 동시에 일반 강화 단계를 1점 감소시킨다.
 *          일반 강화 단계가 1점 감소하는 만큼 일정량의 일반 대미지가 감소하는데, 일반 대미지가 감소한 만큼 추가 특수 대미지가 증가한다.
 *          ex.
 *              일반+100 특수+10 무기를 특수 강화할 때,
 *              특수+10 -> 특수+11 : 특수 대미지 +11
 *              특수 강화 단계 1점 증가 >> 일반+100 -> 일반+99 : 일반 대미지 -99
 *              일반 대미지 -99 >> 추가 특수 대미지 +99
 *              
 *      - 일반, 특수 강화를 같이 진행하기
 *              특수 강화를 할 때 일반 강화 단계가 감소하는 반면, 특수 강화 단계는 일반 강화를 해도 감소하지 않는다.
 *              즉 플레이어는 "일반 강화를 많이 하기" -> "특수 강화를 많이 하기" -> "낮아진 일반 강화 단계를 복구하기"
 *              루틴을 통해 일반 대미지와 특수 대미지 둘 다 높은 강한 무기를 만들 수 있다
 *              
 *      - 특수 강화의 패널티
 *              속성별 특수 대미지 변수가 존재하지 않기에, 속성을 원하는대로 바꾸며 특수 대미지를 무한히 올릴 수 있는 문제가 있다
 *              그렇기에 속성을 바꾸는 특수 강화를 진행하면 그 동안 쌓아온 특수 대미지의 50% 를 잃게된다
 *              일종의 강화 싱크 홀 역할을 담당하는 것
 *              
 *  3. 방어구
 *      당장은 무기만 생각했음
 *      하지만 방어구도 이동속도, 추가 목숨 조각 등 강화를 통해 올릴 수 있는 수단이 있을거라고 생각함
 */



public class GJJTest_SpecialEnhancement : MonoBehaviour
{
    /*
    [SerializeField] private GJJTest_Upgrade_EquipItemSpecial _equipSword;
    [SerializeField] private GJJTest_Upgrade_EquipItemSpecial _equipBow;
    [SerializeField] private GJJTest_Upgrade_ResourceItem _resourceIce;
    [SerializeField] private GJJTest_Upgrade_ResourceItem _resourceFire;
    [SerializeField] private GJJTest_Upgrade_ResourceItem _resourcePoison;
    [SerializeField] private GJJTest_Upgrade_ResourceItem _resourceNormalOneStep;
    [SerializeField] private GJJTest_Upgrade_ResourceItem _resourceNormalTenStep;

    [SerializeField] private GJJTest_Upgrade_EquipItemSpecial _selectedEquip = null;
    [SerializeField] private GJJTest_Upgrade_ResourceItem _selectedResource = null;

    public Text _textResource;
    public Text _textEquip;
    public Text _textResult;
    public Text _textResultNormalUpgrade;
    public Text _textResultSpecialUpgrade;

    public void OnClick_EquipSword()
    {
        _selectedEquip = _equipSword;
        _textEquip.text = _equipSword.itemName;
    }

    public void OnClick_EquipBow()
    {
        _selectedEquip = _equipBow;
        _textEquip.text = _equipBow.itemName;
    }

    public void OnClick_ResourceIce()
    {
        _selectedResource = _resourceIce;
        _textResource.text = _resourceIce.itemName;
    }

    public void OnClick_ResourceFire()
    {
        _selectedResource = _resourceFire;
        _textResource.text = _resourceFire.itemName;
    }

    public void OnClick_ResourcePoison()
    {
        _selectedResource = _resourcePoison;
        _textResource.text = _resourcePoison.itemName;
    }

    public void OnClick_ResourceNormalOneStep()
    {
        _selectedResource = _resourceNormalOneStep;
        _textResource.text = _resourceNormalOneStep.itemName;
    }

    public void OnClick_ResourceNormalTenStep()
    {
        _selectedResource = _resourceNormalTenStep;
        _textResource.text = _resourceNormalTenStep.itemName;
    }

    public void OnClick_DoUpgrade()
    {
        if(_selectedEquip == null)
        {
            Debug.LogError("equip item is null");
            return;
        }

        if (_selectedResource == null)
        {
            Debug.LogError("resource item is null");
            return;
        }

        // do upgrade
        if(GJJTest_IsResourceForSpecial(_selectedResource))
        {
            int tmpDmg = _selectedEquip.enforceValue;
            _selectedEquip.upgradeLevelSpecial++;
            _selectedEquip.valueSpecial += _selectedEquip.upgradeLevelSpecial;
            _selectedEquip.valueSpecial += tmpDmg;

            if (_selectedEquip.enforceValue > 0)
            {
                _selectedEquip.enforceValue--;
                _selectedEquip.value -= tmpDmg;
            }

            GJJTest_SetSpecialUpgradeName(_selectedResource);
        }

        if (_selectedResource == _resourceNormalOneStep)
        {
            // _selectedEquip.upgradeSpecialName = " (Normal)";

            _selectedEquip.enforceValue ++;
            _selectedEquip.value += _selectedEquip.enforceValue;
        }

        if (_selectedResource == _resourceNormalTenStep)
        {
            // _selectedEquip.upgradeSpecialName = " (Normal)";

            for (int i=1; i<=10; i++)
            {
                _selectedEquip.enforceValue ++;
                _selectedEquip.value += _selectedEquip.enforceValue;
            }
        }
    }

    bool GJJTest_IsResourceForSpecial(GJJTest_Upgrade_ResourceItem _rsc)
    {
        if (_rsc == _resourceIce) return true;

        if (_rsc == _resourceFire) return true;

        if (_rsc == _resourcePoison) return true;

        return false;
    }

    void GJJTest_SetSpecialUpgradeName(GJJTest_Upgrade_ResourceItem _rsc)
    {
        if (_rsc == _resourceIce)
        {
            _selectedEquip.upgradeSpecialName = GJJTEST_SPECIALPROPERTY.ICE;
            return;
        }

        if (_rsc == _resourceFire)
        {
            _selectedEquip.upgradeSpecialName = GJJTEST_SPECIALPROPERTY.FIRE;
            return;
        }

        if (_rsc == _resourcePoison)
        {
            _selectedEquip.upgradeSpecialName = GJJTEST_SPECIALPROPERTY.POISON;
            return;
        }
    }

    void GJJTest_SetSpecialValue(GJJTest_Upgrade_EquipItemSpecial _eq, GJJTest_Upgrade_ResourceItem _rsc)
    {
        int tmpDmg = _eq.enforceValue;
        _eq.upgradeLevelSpecial++;
        _eq.valueSpecial += _eq.upgradeLevelSpecial;
        _eq.valueSpecial += tmpDmg;

        if(true)
        {

        }

        if (_eq.enforceValue > 0)
        {
            _eq.enforceValue--;
            _eq.value -= tmpDmg;
        }
    }

    void SetResultText()
    {
        _textResult.text = _selectedEquip.itemName + _selectedEquip.upgradeSpecialName;
        _textResultNormalUpgrade.text = "Normal +" + _selectedEquip.enforceValue + " (" + _selectedEquip.value + " DMG)";
        _textResultSpecialUpgrade.text = "Special +" + _selectedEquip.upgradeLevelSpecial + " (" + _selectedEquip.valueSpecial + " " + _selectedEquip.upgradeSpecialName + " DMG)";
    }

    private void Awake()
    {
        _textEquip.text = "n/a";
        _textResource.text = "n/a";
        _textResult.text = "n/a";
    }

    private void Update()
    {
        SetResultText();
    }
    */
}
