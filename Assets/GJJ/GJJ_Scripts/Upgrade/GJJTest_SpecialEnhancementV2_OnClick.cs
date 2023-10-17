using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GJJTest_SpecialEnhancementV2 : MonoBehaviour
{
    /*
    // onclick
    public void OnClick_EnlistEquipment()
    {
        if(gjj_TempPlayer.GJJTest_CurrentEquip == null)
        {
            Debug.LogError("player is bare hand (OnClick_EnlistEquipment)");
            return;
        }

        gjj_EquipItem = gjj_TempPlayer.GJJTest_CurrentEquip;
        gjj_TempPlayer.GJJTest_CurrentEquip = null;
    }

    public void OnClick_TakeEquipment()
    {
        if (gjj_EquipItem == null)
        {
            Debug.LogError("equip item is null (OnClick_TakeEquipment)");
            return;
        }

        gjj_TempPlayer.GJJTest_CurrentEquip = gjj_EquipItem;
        gjj_EquipItem = null;
    }

    public void OnClick_ResourceIce()
    {
        if (gjj_Resource != gjj_TempResourceIce)
            gjj_Resource = gjj_TempResourceIce;
        else
            gjj_Resource = null;
    }

    public void OnClick_ResourceFire()
    {
        if (gjj_Resource != gjj_TempResourceFire)
            gjj_Resource = gjj_TempResourceFire;
        else
            gjj_Resource = null;
    }

    public void OnClick_ResourcePoison()
    {
        if (gjj_Resource != gjj_TempResourcePoison)
            gjj_Resource = gjj_TempResourcePoison;
        else
            gjj_Resource = null;
    }

    public void OnClick_ResourceNormalOneStep()
    {
        if (gjj_Resource != gjj_TempResourceNormalOne)
            gjj_Resource = gjj_TempResourceNormalOne;
        else
            gjj_Resource = null;
    }

    public void OnClick_ResourceNormalTenStep()
    {
        if (gjj_Resource != gjj_TempResourceNormalTen)
            gjj_Resource = gjj_TempResourceNormalTen;
        else
            gjj_Resource = null;
    }

    public void OnClick_DoEnhance()
    {
        if(gjj_Resource == gjj_TempResourceNormalOne)
        {
            gjj_EquipItem.enforceValue++;
            gjj_EquipItem.value += gjj_EquipItem.enforceValue;

            return;
        }

        if (gjj_Resource == gjj_TempResourceNormalTen)
        {
            for(int i=0; i<10; i++)
            {
                gjj_EquipItem.enforceValue++;
                gjj_EquipItem.value += gjj_EquipItem.enforceValue;
            }

            return;
        }

        if (!GJJTest_AreYouTryingToSpecialEnhance(gjj_Resource))
            return;

        if (GJJTest_CheckSpecialEnhanceChange(gjj_Resource))
            gjj_EquipItem.valueSpecial /= 2;

        gjj_EquipItem.upgradeSpecialName = gjj_Resource.specialType;
        gjj_EquipItem.upgradeLevelSpecial++;
        gjj_EquipItem.valueSpecial += gjj_EquipItem.upgradeLevelSpecial;

        int tmpdmg = gjj_EquipItem.enforceValue;
        gjj_EquipItem.valueSpecial += tmpdmg;

        if(gjj_EquipItem.enforceValue > 0)
        {
            gjj_EquipItem.enforceValue--;
            gjj_EquipItem.value -= tmpdmg;
        }
            

    }
    */
}
