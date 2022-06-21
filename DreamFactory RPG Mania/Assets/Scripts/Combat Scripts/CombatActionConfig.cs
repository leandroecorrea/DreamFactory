using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Combat Action", menuName = "Combat/New Combat Action")]
public class CombatActionConfig : ScriptableObject
{
    [Header("UI Facing Configuration")]
    public string actionName;
    public string description;

    [Header("Combat Stat Configurations")]
    [Tooltip("Varies from action to action (I.e determines base damage for physical attacks)")] public int baseEffectiveness;
    public List<CombatEffectConfig> effectsToApply;

    [Header("Programmer Configuration")]
    [Tooltip("Set this to the name of a Action Handler class name")]
    public string actionHandlerClassName;    
    public CombatActionType combatActionType;
}

public enum CombatActionType
{
    ATTACK, SPELL, ITEM, RUN
}
