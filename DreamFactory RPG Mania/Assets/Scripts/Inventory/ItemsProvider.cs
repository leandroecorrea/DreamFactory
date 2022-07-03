using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ItemsProvider
{
    public static List<Item> Provide()
    {
        var item = ScriptableObject.CreateInstance<ItemConfig>();
        item.itemName = "Mint Potion";
        item.id = "POTION";
        item.description = "Restores HP for an allie";
        item.actionConfig = ScriptableObject.CreateInstance<CombatActionConfig>();
        item.actionConfig.baseEffectiveness = 30;
        item.actionConfig.actionName = "Mint Potion";
        item.actionConfig.actionHandlerClassName = "MintPotionItemHandler";
        item.actionConfig.combatActionType = CombatActionType.SPELL;                
        return Enumerable.Range(1, 5).Select(i =>
        {
            var itemObject = new Item(item);
            itemObject.amount = i;
            return itemObject;
        }).ToList();
    }
}
