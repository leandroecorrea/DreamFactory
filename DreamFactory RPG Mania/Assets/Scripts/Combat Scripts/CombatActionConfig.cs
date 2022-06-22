using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Combat Action", menuName = "Combat/New Combat Action")]
public class CombatActionConfig : ScriptableObject
{
    [Header("UI Facing Configuration")]
    public string actionName;
    public string description;

    [Header("Routing Configuration")]
    [Tooltip("Enable this for attacks that require the Combat Entity to approach the target (I.e Physical Attack)")] public bool requireRouting = true;

    [Header("Combat Stat Configurations")]
    [Tooltip("Varies from action to action (I.e determines base damage for physical attacks)")] public int baseEffectiveness;
    public List<CombatEffectConfig> effectsToApply;

    [Header("Programmer Configuration")]
    [Tooltip("Set this to the name of a Action Handler class name")]
    public string actionHandlerClassName;    
    public CombatActionType combatActionType;
    public TargetType targetType;
}

public enum CombatActionType
{
    ATTACK, SPELL, ITEM, RUN
}
public enum TargetType
{
    ALLIES, ENEMIES, ALL
}
