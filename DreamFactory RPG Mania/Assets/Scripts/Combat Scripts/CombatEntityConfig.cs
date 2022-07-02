using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Combat Entity", menuName = "Combat/New Combat Entity")]
public class CombatEntityConfig : ScriptableObject
{
    [Header("Description")]
    public string Id;
    public string Name;
    [Header("Base Stats")]
    public int baseHP;
    public int baseMP;
    public int baseAttack;
    public int baseSpeed;

    [Header("Combat Action References")]
    public GameObject combatEntityPrefab;    
    public List<CombatActionConfig> actions;
}
