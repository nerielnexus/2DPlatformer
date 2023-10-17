using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ItemData data;

    public void OnPointerEnter(PointerEventData eventData)
    {
        /*  SetItemInfo 메서드의 매개변수 변경으로 에러 발생해서 주석 처리해둠
         *  221221 10:38 GJJ
         */

        /*
         * 수정
         * 221221 SSH
         */

        ItemInfoUI.itemInfoUI.SetItemInfo(data, eventData.position);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemInfoUI.itemInfoUI.RemoveItemInfo();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
