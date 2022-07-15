using System;
using UnityEngine;

[Serializable]
public class Item
{
    public string itemId;
    public ItemConfig data;
    public int amount { get; set; }
    private readonly int maxAmount = 99;
    private readonly int minAmount = 0;

    public Item(ItemConfig data)
    {
        this.data = data;
        this.itemId = data.id;
        amount = 1;
    }
    
    
    public void Add() =>
        amount = Mathf.Min(amount + 1, maxAmount);
    
    public void Substract() =>    
        amount = Mathf.Max(amount - 1, minAmount);

    public override bool Equals(object obj)
    {
        if(obj is Item)
            return (obj as Item).itemId == itemId;
        return base.Equals(obj);
    }
}
