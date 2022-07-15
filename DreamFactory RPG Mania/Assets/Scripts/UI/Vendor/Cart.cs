using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;


public class Cart : MonoBehaviour
{
    public ReactiveCollection<Item> items = new ReactiveCollection<Item>();    
    public int Total { get => items.Aggregate(0, (prev, x) => prev + x.amount * x.data.price); }

    public event Action onBuy;


    public void Buy()
    {
        var itemsToRemove = new List<Item>();
        foreach (var item in items)
        {
            for (int i = 0; i < item.amount; i++)
            {
                InventoryManager.Add(item);
            }
            Debug.Log($"Bought!{item.data.itemName} has {InventoryManager.Get(item)?.amount} amount in inventory");
            itemsToRemove.Add(item);
        }
        itemsToRemove.ForEach(x=> items.Remove(x));       
        onBuy?.Invoke();
    }


    public void RemoveCart()
    {
        items.Clear();
    }

    public void AddToCart(ItemConfig item)
    {
        var addedItem = new Item(item);
        foreach (var existentItem in items)
        {
            if (existentItem.Equals(addedItem))
            {
                existentItem.Add();
                return;
            }
        }
        items.Add(addedItem);        
    }
    

    private void OnDisable()
    {
        items.Dispose();   
        onBuy = null;
    }

}
