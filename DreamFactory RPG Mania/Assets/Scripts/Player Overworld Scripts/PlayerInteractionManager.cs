using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionManager : MonoBehaviour
{
    [Header("Component Refs")]
    [SerializeField] private PlayerOverworldAnimatorCtrl animControl;
    [SerializeField] private PlayerMovement movementControl;
 
    [Header("Interaction Settings")]
    [SerializeField] private LayerMask interactionLayermask;

    private NPCInteractionManager currentRangeInteractionManager;

  
    private void OnTriggerEnter(Collider other)
    {
        if (LayerMaskUtils.IsGameObjectInLayerMask(other.gameObject, interactionLayermask))
        {
            NPCInteractionManager otherNPCInteractionManager = other.GetComponent<NPCInteractionManager>();
            if (otherNPCInteractionManager != null)
            {
                currentRangeInteractionManager = otherNPCInteractionManager;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (
            LayerMaskUtils.IsGameObjectInLayerMask(other.gameObject, interactionLayermask) && 
            other.gameObject == currentRangeInteractionManager.gameObject
        ) {
            currentRangeInteractionManager = null;
        }
    }

    public void HandleNPCTalk(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            if (currentRangeInteractionManager != null)
            {
                currentRangeInteractionManager.InitailizeInteraction();
                currentRangeInteractionManager.onInteractionMenuDismiss += HandleInteractionUIDismiss;

                movementControl.StopMoving();
                animControl.NotifyMovementUpdate(false);
                animControl.StartTalking();

                gameObject.GetComponent<PlayerInput>().DeactivateInput();
            }
        }
    }

    private void HandleInteractionUIDismiss(object sender, System.EventArgs e)
    {
        currentRangeInteractionManager.onInteractionMenuDismiss -= HandleInteractionUIDismiss;

        animControl.StopTalking();
        gameObject.GetComponent<PlayerInput>().ActivateInput();
    }

}
