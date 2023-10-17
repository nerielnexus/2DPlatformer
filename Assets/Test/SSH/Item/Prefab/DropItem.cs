using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public ItemData itemData;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SetData();
    }

    void SetData()
    {
        if(itemData != null)
        spriteRenderer.sprite = itemData.itemSprite;
    }



}
