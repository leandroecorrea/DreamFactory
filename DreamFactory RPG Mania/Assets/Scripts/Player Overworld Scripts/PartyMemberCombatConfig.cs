using System;
using System.Collections.Generic;

[Serializable]
public class PartyMemberCombatConfig
{
    public int currentHP;
    public int currentMP;
    public List<string> EffectsID;
    public List<CombatEffectConfig> combatEffectConfigs;
    public CombatEntityConfig CombatEntityConfig;
}