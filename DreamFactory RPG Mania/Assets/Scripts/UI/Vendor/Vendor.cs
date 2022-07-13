using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using TMPro;

public class Vendor : MonoBehaviour
{
    [SerializeField] private GameObject itemRow;
    [SerializeField] private GameObject content;
    [SerializeField] private List<ItemConfig> items;
    [SerializeField] private TMP_Text currencyAvailable;
    [SerializeField] private Cart cart;
    public int currentCurrency;

    private void OnEnable()
    {        
        items.ForEach(item =>
        {
            var itemPrefab = Instantiate(itemRow);
            var itemRowComponent = itemPrefab.GetComponent<ItemRow>();
            itemRowComponent.SetRow(item);
            itemRowComponent.button.onClick.AddListener(delegate
            {
                cart.AddToCart(item);
            });
            itemPrefab.transform.SetParent(content.transform, false);
        });
        //currentCurrency = PlayerProgression.GetPlayerData<int>(SaveKeys.CURRENT_CURRENCY);        
        currentCurrency = 100;
        currencyAvailable.text = $"You have: ${currentCurrency}";
    }

   

    public void Buy()
    {
        if (!cart.sold.Value && currentCurrency >= cart.Total)
        {            
            cart.Buy();
        }
    }

}