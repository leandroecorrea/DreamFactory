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
    [SerializeField] private TMP_Text remainingCurrencyText;
    [SerializeField] private Cart cart;
    public IntReactiveProperty remainingCurrency = new IntReactiveProperty();

    private void OnEnable()
    {
        remainingCurrency.Subscribe(c => UpdateRemainingCurrency(c));
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
        var currentCurrency = PlayerProgression.GetPlayerData<int>(SaveKeys.CURRENT_CURRENCY);
        remainingCurrency.Value = currentCurrency;
    }

    private void UpdateRemainingCurrency(int c)
    {
        remainingCurrencyText.text = $"You have: ${c}";
    }

    public void Buy()
    {
        if (remainingCurrency.Value > 0 && remainingCurrency.Value >= cart.Total)
        {
            remainingCurrency.Value -= cart.Total;
            cart.Buy();
        }
    }

}