using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventoryManager
{
    private static List<Item> items = ItemsProvider.Provide();
    public static List<Item> GetAll() 
        => items;   

    public static void Add(Item item)
    {
        var existentItem = Get(item);
        if (existentItem == null)
            items.Add(item);
        else
            existentItem.Add();
    }
    public static void Consume(Item item)
    {
        var existentItem = Get(item);
        if (existentItem != null)
        {
            existentItem.Substract();
            if(existentItem.amount == 0)
                items.Remove(existentItem);
        }
    }
    private static Item Get(Item item)
    {
        foreach (Item existentItem in items)
        {
            if (existentItem.data.itemName == item.data.itemName)
                return existentItem;
        }
        return null;
    }
}
