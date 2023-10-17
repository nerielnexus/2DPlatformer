using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    //싱글톤클래스
    public static ItemDataBase itemDataBase;

    //모든 아이템 데이터
    [SerializeField]
    ItemData[] itemSO;

    //게임내에서 사용할 아이템 데이터 리스트
    public Dictionary<int, ItemData> itemList = new Dictionary<int, ItemData>();

    private void Awake()
    {
        if(itemDataBase == null)
        {
            itemDataBase = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    //아이템 리스트 만들기
    void CreateItemList()
    {
        int itemAddress;

        for (int i = 0; i < itemSO.Length; i++)
        {
            itemAddress = itemSO[i].address;

            itemList.Add(itemAddress, itemSO[i]);
        }
    }

    private void Start()
    {
        CreateItemList();
    }
}
