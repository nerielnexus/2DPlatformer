using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJTest_EnhancePlayerTemp : MonoBehaviour
{
    /*
    [Header("O: use // P: swap")]
    [SerializeField] private GJJTest_Upgrade_EquipItemSpecial gjj_Sword;
    // [SerializeField] private GJJTest_Upgrade_EquipItemSpecial gjj_Bow;
    [SerializeField] private GJJTest_Upgrade_EquipItemSpecial gjj_CurrentEquip = null;
    
    public GJJTest_Upgrade_EquipItemSpecial GJJTest_CurrentEquip
    {
        get => gjj_CurrentEquip;
        set => gjj_CurrentEquip = value;
    }

    void GJJTest_PlayerUseWeapon()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gjj_CurrentEquip == null)
                Debug.LogError("player is bare hand");

            if (gjj_CurrentEquip == gjj_Sword *//*|| gjj_CurrentEquip == gjj_Bow*//*)
            {
                Debug.LogWarning("[Sword] Normal(" + gjj_CurrentEquip.enforceValue + "/ " + gjj_CurrentEquip.value + " DMG), Special("
                    + gjj_CurrentEquip.upgradeSpecialName + "/ " + gjj_CurrentEquip.upgradeLevelSpecial + "/ " + gjj_CurrentEquip.valueSpecial + " DMG)");
            } 
        }
    }

    void GJJTest_PlayerSwapWeapon()
    {
        if (Input.GetKeyDown(KeyCode.O))
            if (GJJTest_CurrentEquip == null) GJJTest_CurrentEquip = gjj_Sword;
            else
            {
                gjj_Sword = GJJTest_CurrentEquip;
                GJJTest_CurrentEquip = null;
            }
    }

    private void Update()
    {
        GJJTest_PlayerSwapWeapon();
        GJJTest_PlayerUseWeapon();
    }
    */
}
