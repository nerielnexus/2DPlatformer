using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Storage : MonoBehaviour
{
    public static Storage storage;
    

    //슬롯 프리팹
    public GameObject slotPrefab;

    //슬롯 부모 오브젝트
    public Transform slotsPos;

    //최대 슬롯갯수
    public int maxSlots;

    //창고 슬롯리스트
    public List<Slot> slotList = new List<Slot>();



    //슬롯 동적생성
    void CreateSlot()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, new Vector2(slotsPos.position.x + i, slotsPos.position.y + i), Quaternion.identity);

            Slot slotData = slot.GetComponent<Slot>();

            slot.transform.SetParent(slotsPos);
            slot.name = "Slot" + i;

            slotList.Add(slotData);
        }
    }


    //그리드 끄기
    public void GridOff()
    {
        slotsPos.GetComponent<GridLayoutGroup>().enabled = false;
    }

    private void Awake()
    {
        CreateSlot();
    }

    private void Start()
    {
        storage = this;
        Invoke("GridOff", 0.5f);
    }

}
