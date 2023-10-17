using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    //퀵슬롯의 현재 이미지와 수량 정보
    public Image QuickSlotimage;
    public Text QuickSlotText;

    //받아올 슬롯의 이미지와 수량 정보
    public Image itemImage;
    public Text itemText;
    
    void Update()
    {
        QuickSlotimage.sprite = itemImage.sprite;
        QuickSlotText = itemText;
    }
    
}
