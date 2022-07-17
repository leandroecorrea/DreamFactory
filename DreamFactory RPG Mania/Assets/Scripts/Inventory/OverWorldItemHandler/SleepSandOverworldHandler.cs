using UnityEngine;

public class SleepSandOverworldHandler : ItemOverworldHandler
{
    public SleepSandOverworldHandler(Item item) : base(item)
    {
    }

    public override void Handle(PlayerPartyMemberConfig member)
    {
        var effectiveness = 15;
        var currentHP = member.PartyMemberCombatConfig.currentHP;
        var maxHP = member.PartyMemberCombatConfig.CombatEntityConfig.baseHP;
        if (currentHP > 0 && _item.amount > 0)
        {
            member.PartyMemberCombatConfig.currentHP = Mathf.Min(currentHP + effectiveness, maxHP);            
        }
        var currentMP = member.PartyMemberCombatConfig.currentMP;
        var maxMP = member.PartyMemberCombatConfig.CombatEntityConfig.baseMP;
        if (currentMP > 0 && _item.amount > 0)
        {
            member.PartyMemberCombatConfig.currentHP = Mathf.Min(currentMP + effectiveness, maxMP);
            InventoryManager.Consume(_item);
        }
    }
}
