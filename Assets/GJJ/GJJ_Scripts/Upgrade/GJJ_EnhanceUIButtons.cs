using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class GJJ_EnhanceUI : MonoBehaviour
{
    public void OnButtonClick_EnhanceNormal()
    {
        statusValue = ENHANCEUISTATUS.ENHANCE_NORMAL;
        SetEnhanceUIActiveStatus(enhanceUI_Normal);

        foreach (Slot iter in list_EnhanceSlots)
            iter.tag = "Slot(Equip)";
    }

    public void OnButtonClick_EnhanceElemental()
    {
        statusValue = ENHANCEUISTATUS.ENHANCE_ELEMENTAL;
        SetEnhanceUIActiveStatus(enhanceUI_Elemental);
        foreach (Slot iter in list_EnhanceSlots)
            iter.tag = "Slot(Gem)";
    }

    public void OnButtonClick_CraftGem()
    {
        statusValue = ENHANCEUISTATUS.CRAFT_GEM;
        SetEnhanceUIActiveStatus(enhanceUI_Craft);
    }

    public void OnButtonClick_DoIt()
    {
        if (statusValue != ENHANCEUISTATUS.NONE)
            Debug.LogWarning("Do it");

        if (statusValue == ENHANCEUISTATUS.ENHANCE_ELEMENTAL)
            ProceedEnhance_Gem();

        // SetEnhanceUIActiveStatus(enhanceUI_None);
    }

    public void OnButtonClick_Exit()
    {
        enhanceUI_Composite.SetActive(!enhanceUI_Composite.activeSelf);
    }
}
