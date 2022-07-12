using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "CartItems", menuName = "Vendor/Cart Items")]
public class Cart : ScriptableObject
{
    public ReactiveCollection<Item> items = new ReactiveCollection<Item>();

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
}
