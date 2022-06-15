using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Item> items;

    public Inventory()
    {
        items = new List<Item>();
    }

    public void Add(Item item)
    {
        var existentItem = Get(item);
        if (existentItem == null)
            items.Add(item);
        else
            existentItem.Add();
    }
    public void Consume(Item item)
    {
        var existentItem = Get(item);
        if (existentItem != null)
            existentItem.Substract();
    }
    private Item Get(Item item)
    {
        foreach (Item existentItem in items)
        {
            if(existentItem.data.itemName == item.data.itemName)
                return existentItem;
        }
        return null;
    }


}
