using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Effect Config", menuName = "Combat/New Effect Config")]
public class CombatEffectConfig : ScriptableObject
{
    [Header("UI Configuration")]
    public string displayName;
    public Sprite displayIcon;

    [Header("Combat Configuration")]
    public bool doesExpire;        
    [Tooltip("Controls how many combat rounds before effect is auto-removed (if doesExpire is true)")] public int roundDuration;
    public bool doesStack;
    [Tooltip("Max amount of effect instances on an entity at once (if doesStack is true)")]public int maxStackCount;
    [Tooltip("Varies from effect to effect (I.e determines base damage for fever effect)")] public int baseEffectiveness;

    [Header("Programmer Configuration")]
    [Tooltip("Unique string used to track stack counts")] public string effectId;
    public string effectHandlerClassName;
}
