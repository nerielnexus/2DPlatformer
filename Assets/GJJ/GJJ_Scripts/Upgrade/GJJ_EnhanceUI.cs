using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class GJJ_EnhanceUI : MonoBehaviour
{
    private enum ENHANCEUISTATUS
    {
        NONE = 0,
        ENHANCE_NORMAL,
        ENHANCE_ELEMENTAL,
        CRAFT_GEM
    };

    private List<GameObject> list_EnhanceUI;

    [Header("UI Prefabs")]
    [SerializeField] private ENHANCEUISTATUS statusValue = ENHANCEUISTATUS.NONE;
    [SerializeField] private GameObject enhanceUI_None = null;
    [SerializeField] private GameObject enhanceUI_Normal = null;
    [SerializeField] private GameObject enhanceUI_Elemental = null;
    [SerializeField] private GameObject enhanceUI_Craft = null;
    [SerializeField] private GameObject enhanceUI_Composite = null;

    [Header("Change Slot Tag for Enhance Normal/Elemental")]
    [SerializeField] private List<Slot> list_EnhanceSlots = new List<Slot>();

    [Header("Text for show Information of Enhance Resource")]
    [SerializeField] private Text resource1_TextLevel = null;
    [SerializeField] private Text resource1_TextValue = null;
    [SerializeField] private Text resource2_TextLevel = null;
    [SerializeField] private Text resource2_TextValue = null;
    [SerializeField] private Text result_TextLevel = null;
    [SerializeField] private Text result_TextValue = null;


    private void SetEnhanceUIActiveStatus(GameObject toEnable)
    {
        foreach(GameObject tmp in list_EnhanceUI)
        {
            if (tmp.Equals(toEnable))
                tmp.SetActive(true);
            else
                tmp.SetActive(false);
        }
    }

    private void SetEnhanceUIActive()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            enhanceUI_Composite.SetActive(!enhanceUI_Composite.activeSelf);
    }

    private void GetItemInfo_Equip()
    {
        EquipData eq1 = (EquipData)(list_EnhanceSlots[0].GetComponent<Slot>().item) == null ?
            null : (EquipData)(list_EnhanceSlots[0].GetComponent<Slot>().item);

        EquipData eq2 = (EquipData)(list_EnhanceSlots[1].GetComponent<Slot>().item) == null ?
            null : (EquipData)(list_EnhanceSlots[1].GetComponent<Slot>().item);

        EquipData eq3 = (EquipData)(list_EnhanceSlots[2].GetComponent<Slot>().item) == null ?
            null : (EquipData)(list_EnhanceSlots[2].GetComponent<Slot>().item);

        if (eq1 == null)
        {
            Debug.LogWarning("eq1 was null");
            resource1_TextLevel.text = "n/a";
            resource1_TextValue.text = "n/a";
        }
        else
        {
            resource1_TextLevel.text = eq1.enforceValue.ToString();
            resource1_TextValue.text = eq1.value.ToString();
        }

        if (eq2 == null)
        {
            Debug.LogWarning("eq2 was null");
            resource2_TextLevel.text = "n/a";
            resource2_TextValue.text = "n/a";
        }
        else
        {
            resource2_TextLevel.text = eq2.enforceValue.ToString();
            resource2_TextValue.text = eq2.value.ToString();
        }

        if (eq3 == null)
        {
            Debug.LogWarning("eq3 was null");
            result_TextLevel.text = "n/a";
            result_TextValue.text = "n/a";
        }
        else
        {
            result_TextLevel.text = eq3.enforceValue.ToString();
            result_TextValue.text = eq3.value.ToString();
        }
    }

    private void GetItemInfo_Gem()
    {
        GJJ_Enhance_SpecialGem gem1 = list_EnhanceSlots[0].item as GJJ_Enhance_SpecialGem == null ?
            null : list_EnhanceSlots[0].item as GJJ_Enhance_SpecialGem;

        GJJ_Enhance_SpecialGem gem2 = list_EnhanceSlots[1].item as GJJ_Enhance_SpecialGem == null ?
            null : list_EnhanceSlots[1].item as GJJ_Enhance_SpecialGem;

        GJJ_Enhance_SpecialGem gem3 = list_EnhanceSlots[2].item as GJJ_Enhance_SpecialGem == null ?
            null : list_EnhanceSlots[2].item as GJJ_Enhance_SpecialGem;

        if (gem1 == null)
        {
            Debug.LogWarning("gem1 was null");
            resource1_TextLevel.text = "Please Add";
            resource1_TextValue.text = "Gem to enhance";
        }
        else
        {
            resource1_TextLevel.text = gem1.enhanceLevel.ToString();
            resource1_TextValue.text = gem1.value.ToString();
        }

        if (gem2 == null)
        {
            Debug.LogWarning("gem2 was null");
            resource2_TextLevel.text = "Please Add";
            resource2_TextValue.text = "Gem to enhance";
        }
        else
        {
            resource2_TextLevel.text = gem2.enhanceLevel.ToString();
            resource2_TextValue.text = gem2.value.ToString();
        }

        if (gem3 == null)
        {
            Debug.LogWarning("gem3 was null");
            result_TextLevel.text = "Please Add";
            result_TextValue.text = "Gem to enhance";
        }
        else
        {
            result_TextLevel.text = gem3.enhanceLevel.ToString();
            result_TextValue.text = gem3.value.ToString();
        }
    }

    private void ProceedEnhance_Gem()
    {
        if(list_EnhanceSlots[2].itemOn)
        {
            Debug.Log("ENHANCE - item is in result slot");
            return;
        }

        if (!list_EnhanceSlots[0].itemOn || !list_EnhanceSlots[1].itemOn)
        {
            Debug.Log("ENHANCE - item is not in resource slot");
            return;
        }

        GJJ_Enhance_SpecialGem rsc1 = list_EnhanceSlots[0].item as GJJ_Enhance_SpecialGem;
        GJJ_Enhance_SpecialGem rsc2 = list_EnhanceSlots[1].item as GJJ_Enhance_SpecialGem;

        if(rsc1.itemType != rsc2.itemType)
        {
            Debug.Log("ENHANCE - item type mismatch");
            return;
        }

        if(rsc1.enhanceLevel != rsc2.enhanceLevel)
        {
            Debug.Log("ENHANCAE - items enhanceLevel not equal");
            return;
        }

        GJJ_Enhance_SpecialGem ret = rsc1.Clone() as GJJ_Enhance_SpecialGem;
        ret.enhanceLevel = rsc1.enhanceLevel + 1;
        ret.value = rsc1.value  + (ret.enhanceLevel * 2);

        list_EnhanceSlots[0].RemoveItem();
        list_EnhanceSlots[1].RemoveItem();

        list_EnhanceSlots[2].AddItemData(ret);

        /*
        rsc1.enhanceLevel++;
        rsc1.value += (rsc1.enhanceLevel * 2);

        list_EnhanceSlots[0].RemoveItem();
        list_EnhanceSlots[1].RemoveItem();

        list_EnhanceSlots[2].AddItemData(rsc1);
        */
    }

    // unity
    private void Awake()
    {
        list_EnhanceUI = new List<GameObject>();

        list_EnhanceUI.Add(enhanceUI_None);
        list_EnhanceUI.Add(enhanceUI_Normal);
        list_EnhanceUI.Add(enhanceUI_Elemental);
        list_EnhanceUI.Add(enhanceUI_Craft);

        enhanceUI_Composite.SetActive(false);
    }

    private void Update()
    {
        SetEnhanceUIActive();

        if(statusValue == ENHANCEUISTATUS.ENHANCE_NORMAL)
            GetItemInfo_Equip();

        if (statusValue == ENHANCEUISTATUS.ENHANCE_ELEMENTAL)
            GetItemInfo_Gem();
    }
}
