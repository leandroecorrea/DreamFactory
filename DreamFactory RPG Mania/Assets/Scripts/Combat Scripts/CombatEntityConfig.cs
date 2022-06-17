using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Combat Entity", menuName = "Combat/New Combat Entity")]
public class CombatEntityConfig : ScriptableObject
{
    [Header("Base Stats")]
    public int baseHP;
    public int baseMP;
    public int baseAttack;
    public int baseSpeed;

    [Header("Combat Action References")]
    public List<CombatActionConfig> actions;
}
