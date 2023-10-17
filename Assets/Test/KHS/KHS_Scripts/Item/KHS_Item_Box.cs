using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 설명 : 소비형 아이템, 랜덤 아이템 생성

public class KHS_Item_Box : UseItem
{
    public GameObject[] itemlist;
    public Sprite openBox;
    InvenUI inven;
    SpriteRenderer box;

    void Start()
    {
        inven = InvenUI.invenUI;

        //Debug.Log(GameObject.Find("InvenUI").GetComponentInChildren<Inventory>().name);
        box = GetComponent<SpriteRenderer>();

        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            for(int i=0; i<inven.slotList.Length; i++)
            {
                if(inven.slotList[i].item != null)
                {
                    if (inven.slotList[i].item.address == 107)
                    {
                        box.sprite = openBox;
                        inven.slotList[i].RemoveItem();
                        StartCoroutine(UpItem());
                    }
                }
            }
        }
    }

    IEnumerator UpItem()
    {
        int r = Random.Range(0, 6);
        yield return new WaitForSeconds(1f);
        
        GameObject appear = Instantiate(itemlist[r]);
        appear.transform.position = transform.position;
    }
}
