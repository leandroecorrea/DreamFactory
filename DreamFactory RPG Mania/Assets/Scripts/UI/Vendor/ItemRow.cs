using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemRow : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemPrice;
    [SerializeField] public Button button;

    public void SetRow(ItemConfig itemConfig)
    {
        itemSprite.sprite = itemConfig.icon;
        itemName.text = itemConfig.itemName;
        itemPrice.text = itemConfig.price.ToString();        
    }
}
