using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SSH_Inventory : MonoBehaviour
{
    /// <summary>
    /// 인벤 아이템 배열
    /// </summary>
    public GameObject[] invenItem = new GameObject[6];

    
    /// <summary>
    /// 인벤토리UI 스크립트
    /// </summary>
    public SSH_InvenUI invenUi;


    /// <summary>
    /// 아이템 인덱스 추가
    /// </summary>
    /// <param name="item"></param>
    public void AddItemData(GameObject item)
    {
        int index = FindNullSlot();

        if (index == -1)
        {
            Debug.Log("빈슬롯없음");
            return;
        }
        else
        {
            invenItem[index] = item;
            invenUi.SetItem(index, item);
        }
    }


    /// <summary>
    /// 빈 인덱스 찾기 i = 사전 키 값
    /// </summary>
    /// <returns></returns>
    int FindNullSlot()
    {
        for (int i = 0; i < invenItem.Length; i++)
        {
            if (invenItem[i] == null)
            {
                Debug.Log("빈슬롯 : " + i);
                return i;
            }
        }
        return -1;
    }


    
    //아이템 인덱스 교환
    public void SwapItemIndex(int first, int last)
    {
        //값 교환
        var tmpSprite = invenItem[first];
        invenItem[first] = invenItem[last];
        invenItem[last] = tmpSprite;
    }
    
    /// <summary>
    /// 아이템 인덱스 제거
    /// </summary>
    public void RemoveIndex(int index)
    {
        invenItem[index] = null;
    }





}