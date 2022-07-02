using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ItemsProvider
{
    public static List<Item> Provide()
    {
        var item = ScriptableObject.CreateInstance<ItemConfig>();
        item.itemName = "Potion";
        item.id = "POTION";
        item.description = "Restores HP for an allie";
        return Enumerable.Range(1, 5).Select(i =>
        {
            var itemObject = new Item(item);
            itemObject.amount = i;
            return itemObject;
        }).ToList();
    }
}
