using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GJJTEST_RESOURCEITEMTYPE
{
    NONE = 0,
    NORMALUPGRADE,
    SPECIALUPGRADE
};

public enum GJJTEST_SPECIALPROPERTY
{
    NONE = 0,
    ICE,
    FIRE,
    POISON
};

public partial class GJJTest_SpecialEnhancementV2 : MonoBehaviour
{
    /*
    // public 

    // private
    [SerializeField] private GJJTest_Upgrade_ResourceItem gjj_Resource = null;
    [SerializeField] private GJJTest_Upgrade_EquipItemSpecial gjj_EquipItem = null;
    [SerializeField] private GJJTest_EnhancePlayerTemp gjj_TempPlayer;

    [SerializeField] private Text gjjtext_EquipmentToEnhance;
    [SerializeField] private Text gjjtext_ResourceToUse;
    [SerializeField] private Text gjjtext_ResultItemName;
    [SerializeField] private Text gjjtext_ResultNormalEnhance;
    [SerializeField] private Text gjjtext_ResultSpecialEnhance;

    [SerializeField] private GJJTest_Upgrade_ResourceItem gjj_TempResourceIce;
    [SerializeField] private GJJTest_Upgrade_ResourceItem gjj_TempResourceFire;
    [SerializeField] private GJJTest_Upgrade_ResourceItem gjj_TempResourcePoison;
    [SerializeField] private GJJTest_Upgrade_ResourceItem gjj_TempResourceNormalOne;
    [SerializeField] private GJJTest_Upgrade_ResourceItem gjj_TempResourceNormalTen;

    // method
    void SetTexts(GJJTest_Upgrade_EquipItemSpecial _equip, GJJTest_Upgrade_ResourceItem _resource)
    {
        gjjtext_EquipmentToEnhance.text = _equip != null ? _equip.itemName : "equip n/a";
        gjjtext_ResourceToUse.text = _resource != null ? _resource.itemName : "resource n/a";
        gjjtext_ResultItemName.text = _equip != null ? _equip.itemName : "result n/a";

        gjjtext_ResultNormalEnhance.text
            = "Normal +"
            + (_equip != null ? _equip.enforceValue.ToString() : "n/a")
            + " (" + (_equip != null ? _equip.value.ToString() : "n/a") + " DMG)";
        gjjtext_ResultSpecialEnhance.text
            = (_equip != null ? _equip.upgradeSpecialName.ToString() : "special n/a")+ " +"
            + (_equip != null ? _equip.upgradeLevelSpecial.ToString() : "n/a")
            + " (" + (_equip != null ? _equip.valueSpecial.ToString() : "n/a") + " DMG)";
    }

    bool GJJTest_AreYouTryingToSpecialEnhance(GJJTest_Upgrade_ResourceItem _rsc)
    {
        if (_rsc == gjj_TempResourceIce) return true;
        if (_rsc == gjj_TempResourceFire) return true;
        if (_rsc == gjj_TempResourcePoison) return true;

        return false;
    }

    bool GJJTest_CheckSpecialEnhanceChange(GJJTest_Upgrade_ResourceItem _rsc)
    {
        if (gjj_EquipItem.upgradeSpecialName == GJJTEST_SPECIALPROPERTY.NONE)
            return false;

        if (gjj_EquipItem.upgradeSpecialName == _rsc.specialType)
            return false;

        return true;
    }

    // unity
    private void Awake()
    {
        gjj_TempPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<GJJTest_EnhancePlayerTemp>();
        SetTexts(null, null);
    }

    private void Update()
    {
        SetTexts(gjj_EquipItem, gjj_Resource);
    }
    */
}
