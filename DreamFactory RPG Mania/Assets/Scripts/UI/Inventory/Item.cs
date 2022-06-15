using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemData data;
    public int amount { get; set; }
    private readonly int maxAmount = 99;
    private readonly int minAmount = 0;

    public Item(ItemData data)=>
        this.data = data;
    
    public void Add() =>
        amount = Mathf.Min(amount + 1, maxAmount);
    
    public void Substract() =>    
        amount = Mathf.Max(amount - 1, minAmount);        
}
