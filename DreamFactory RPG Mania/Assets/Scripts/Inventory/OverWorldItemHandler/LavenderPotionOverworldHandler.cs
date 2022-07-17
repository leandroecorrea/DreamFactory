using UnityEngine;

public class LavenderPotionOverworldHandler : ItemOverworldHandler
{
    public LavenderPotionOverworldHandler(Item item) : base(item)
    {
    }

    public override void Handle(PlayerPartyMemberConfig member)
    {
        var currentMP = member.PartyMemberCombatConfig.currentMP;
        var maxMP = member.PartyMemberCombatConfig.CombatEntityConfig.baseMP;
        if (currentMP > 0 && _item.amount > 0)
        {
            member.PartyMemberCombatConfig.currentHP = Mathf.Min(currentMP + _item.data.actionConfig.baseEffectiveness, maxMP);
            InventoryManager.Consume(_item);
        }
    }
}
