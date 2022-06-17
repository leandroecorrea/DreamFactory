using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEntity : MonoBehaviour
{
    public CombatEntityConfig entityConfig;

    private List<IEffectHandler> effectsCaused;
    private int currentHP;
    private int currentMaxHP;
    private int currentMP;
    private int currentMaxMP;
    private int currentAttack;
    private int currentSpeed;

    public void Awake()
    {
        currentMaxHP = entityConfig.baseHP;
        currentHP = currentMaxHP;

        currentMaxMP = entityConfig.baseMP;
        currentMP = currentMaxMP;

        currentAttack = entityConfig.baseAttack;
        currentSpeed = entityConfig.baseSpeed;
    }

    public virtual void StartTurn()
    {
        Debug.LogError("StartTurn Not Implemented");
        return;
    }

    public virtual void TakeDamage(int damage)
    {
        Debug.LogError("TakeDamage Not Implemented");
        return;
    }

    public virtual void ApplyEffects(List<IEffectHandler> affectsToApply)
    {
        Debug.LogError("ApplyEffects Not Implemented");
        return;
    }
}
