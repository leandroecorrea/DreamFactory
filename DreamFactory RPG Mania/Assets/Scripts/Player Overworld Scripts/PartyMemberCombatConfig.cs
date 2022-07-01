using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Party Combat Config", menuName = "Player Party/Combat Config/New Party Member Combat Config")]
public class PartyMemberCombatConfig : ScriptableObject
{
    public int currentHP;
    public int currentMP;
    public List<CombatEffectConfig> effects;
    public CombatEntityConfig combatEntityConfig;
}