using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class CartDetails : MonoBehaviour
{
    [SerializeField] private GameObject cartRowPrefab;
    [SerializeField] private GameObject cartView;
    [SerializeField] private Cart cart;
    [SerializeField] private TMP_Text cartAmount;
    private List<IDisposable> subscriptions;


    private void Awake()
    {
        subscriptions = new List<IDisposable>();
    }

    private void OnDisable()
    {
        cart.RemoveCart();
        int firstPrefabIndex = 1;
        for (int i = firstPrefabIndex; i < cartView.transform.childCount; i++)
        {            
            Destroy(cartView.transform.GetChild(i).gameObject);
        }
        subscriptions.ForEach(x =>
        {
            x?.Dispose();
        });
    }

    private void OnEnable()
    {
        UpdateTotal();
        var onAddSubscription = cart.items.ObserveAdd()
                  .Subscribe(onAddItem =>
                  {
                      var prefab = Instantiate(cartRowPrefab);
                      prefab.name = onAddItem.Value.itemId;
                      prefab.transform.SetParent(cartView.transform, false);
                      var component = prefab.GetComponent<CartRow>();
                      component.addButton.onClick.AddListener(() => onAddItem.Value.Add());
                      component.removeButton.onClick.AddListener(() => onAddItem.Value.Substract());
                      component.InitializeRow(onAddItem.Value.data.itemName, onAddItem.Value.data.price, onAddItem.Value.amount);
                      var onItemAmountChanged = onAddItem.Value
                             .ObserveEveryValueChanged(item => item.amount)
                             .Subscribe(amount =>
                             {
                                 UpdateTotal();
                                 if (amount == 0)
                                 {
                                     cart.items.Remove(onAddItem.Value);
                                 }
                                 else
                                 {
                                     component.UpdateValue(onAddItem.Value);
                                 }
                             }).AddTo(this);
                      subscriptions.Add(onItemAmountChanged);
                  }).AddTo(this);
        var onRemoveSubscription = cart.items.ObserveRemove()
            .Subscribe(onRemove =>
            {
                RemovePrefab(onRemove.Value.itemId);
            }).AddTo(this);
        subscriptions.Add(onAddSubscription);
        subscriptions.Add(onRemoveSubscription);
    }

    private void RemovePrefab(string itemId)
    {
        for (int i = 0; i < cartView.transform.childCount; i++)
        {
            if (cartView.transform.GetChild(i).gameObject.name == itemId)
            {
                Destroy(cartView.transform.GetChild(i).gameObject);
            }
        }
    }



    void UpdateTotal()
    {
        cartAmount.text = $"Total: {cart.Total}";
    }
}
