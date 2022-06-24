using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionManager : MonoBehaviour
{
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
                currentRangeInteractionManager.InitializeInteractionUI();
                gameObject.GetComponent<PlayerInput>().DeactivateInput();
            }
        }
    }
}
