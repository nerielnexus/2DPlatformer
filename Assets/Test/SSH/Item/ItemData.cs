using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    //아이템 이름
    public string itemName;

    //아이템 주소
    public int address;

    /*
     * 무기 0
     * 갑옷 10
     * 방어구 20
     * 소모성 100
     * 재료 200
     * 젬 300
     */

    //아이템 타입
    //아이템(소비)    0
    //장비            1
    //무기            2
    //방어구          3
    //재료            4
    //젬              5
    public int itemType;

    //아이템 이미지
    public Sprite itemSprite;

    
    [Multiline (3)]
    //아이템설명
    public string itemInfo;


    //새로운 아이템데이터 객체 생성용 메소드
    public virtual ItemData CreateItemData()
    {
        var itemData = CreateInstance<ItemData>();

        itemData.itemName = this.itemName;
        itemData.address = this.address;
        itemData.itemType = this.itemType;
        itemData.itemSprite = this.itemSprite;
        itemData.itemInfo = this.itemInfo;

        return itemData;
    }

    //아이템 사용용
    public virtual void UseItem()
    {
        //사용 아이템 = 사용 아이템 생성 메소드로
        //장착 아이템 = 장착 메소드로
        //기타 아이템 = X
    }
}
