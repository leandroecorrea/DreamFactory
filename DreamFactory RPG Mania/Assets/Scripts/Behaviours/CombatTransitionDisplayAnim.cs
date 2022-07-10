using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTransitionDisplayAnim : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CombatTransitionManager.instance.HandleCombatTransitionComplete();
    }
}
