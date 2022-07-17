using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MintPotionOverworldHandler : ItemOverworldHandler
{
    public MintPotionOverworldHandler(Item item) : base(item)
    {
    }

    public override void Handle(PlayerPartyMemberConfig member)
    {
        var currentHP = member.PartyMemberCombatConfig.currentHP;
        var maxHP = member.PartyMemberCombatConfig.CombatEntityConfig.baseHP;
        if (currentHP > 0 && _item.amount > 0)
        {
            member.PartyMemberCombatConfig.currentHP = Mathf.Min(currentHP + _item.data.actionConfig.baseEffectiveness,maxHP);
            InventoryManager.Consume(_item);
        }
    }
}
