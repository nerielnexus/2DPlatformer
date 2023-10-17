using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 장비 UI관리
/// </summary>
public class EquipInven : MonoBehaviour
{
    public static EquipInven equipInven;

    void Awake()
    {
        if(equipInven == null)
        {
            equipInven = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public WaeponSlot waeponSlot;
    public ArmorSlot armorSlot;

    /// <summary>
    /// 무기 장착
    /// </summary>
    public void EquipWaepon(ItemData item)
    {
        if(SceneManager.GetActiveScene().buildIndex > 1)
        {
            Player.instance.MatchWeaponValue();
        }
    }

    /// <summary>
    /// 무기 해제
    /// </summary>
    public void DisarmWaepon()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            Player.instance.MatchWeaponValue();
        }
    }


    /// <summary>
    /// 방어구 장착
    /// </summary>
    /// <param name="item"></param>
    public void EquipArmor(ItemData item)
    {
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            Player.instance.MatchArmorValue(item as EquipData);
        }

    }

    /// <summary>
    /// 방어구 해제
    /// </summary>
    public void DisarmArmor()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            Player.instance.MatchArmorValue(null);
        }
    }
}