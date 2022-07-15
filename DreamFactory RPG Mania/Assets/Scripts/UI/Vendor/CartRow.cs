using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CartRow : MonoBehaviour
{
    [SerializeField] private TMP_Text itemName;
    [SerializeField] public TMP_Text price;
    [SerializeField] private TMP_Text quantity;
    [SerializeField] public Button addButton;
    [SerializeField] public Button removeButton;

    public void InitializeRow(string itemName, int price, int quantity)
    {
        this.itemName.text = itemName;
        var cartPrice = price * quantity;
        this.price.text = cartPrice.ToString();
        this.quantity.text = quantity.ToString();        
    }



    public void UpdateValue(Item item)
    {                    
            this.quantity.text = item.amount.ToString();
            this.price.text = (item.amount * item.data.price).ToString();        
    }   

}
