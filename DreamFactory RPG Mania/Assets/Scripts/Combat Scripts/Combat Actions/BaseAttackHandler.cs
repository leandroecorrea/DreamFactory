using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttackHandler
{
    public void ApplyEffects(List<CombatEffectConfig> effectsToApply, params CombatEntity[] targets)
    {
        foreach(CombatEntity targetEntity in targets)
        {
            List<IEffectHandler> effectHandlerInstances = new List<IEffectHandler>();

            foreach(CombatEffectConfig targetEffect in effectsToApply)
            {
                Type combatEffectType = Type.GetType(targetEffect.effectHandlerClassName);
                IEffectHandler effectHandlerInterfaceInstance = (IEffectHandler)Activator.CreateInstance(combatEffectType);

                effectHandlerInterfaceInstance.combatEffectConfig = targetEffect;
                effectHandlerInstances.Add(effectHandlerInterfaceInstance);
            }

            targetEntity.ApplyEffects(effectHandlerInstances);
        }
    }
}
