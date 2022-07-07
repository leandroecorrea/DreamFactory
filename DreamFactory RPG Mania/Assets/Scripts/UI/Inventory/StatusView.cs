using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusView : MonoBehaviour
{
    [SerializeField] private TMP_Text attack, defence, speed, level, total_xp, xpToNextLevel;

    public void SetStats(PlayerPartyMemberConfig partyMember)
    {
        var combatConfig = partyMember.PartyMemberCombatConfig.CombatEntityConfig;
        var levelingStats = partyMember.Stats;
        attack.text = $"Attack: {combatConfig.baseAttack}";
        defence.text = $"Defence: Not implemented";
        speed.text = $"Speed: {combatConfig.baseSpeed}";
        level.text = $"Level: {levelingStats.level}";
        total_xp.text = $"Total XP: {levelingStats.experience}";
        xpToNextLevel.text = $"Next level: {levelingStats.experienceUntilNextLevel}";
    }
}
