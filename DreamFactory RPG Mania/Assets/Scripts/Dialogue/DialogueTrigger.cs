using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : DialogueManager
{
    public GameObject dialogueTriggerUIParent;

    [Header("Dialogue Trigger Settings")]
    [SerializeField] private Conversation targetTriggerConversation;

    private PlayerInput playerInput;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInput = other.gameObject.GetComponent<PlayerInput>();
            if (playerInput != null)
            {
                playerInput.gameObject.GetComponent<PlayerMovement>().StopMoving();
                playerInput.DeactivateInput();

                onConversationComplete += HandleDialogueTriggerComplete;
            }

            InitializeConversation(targetTriggerConversation);
            dialogueTriggerUIParent.SetActive(true);
        }
    }

    private void HandleDialogueTriggerComplete(object sender, ConversationCompletedArgs args)
    {
        playerInput.ActivateInput();
        playerInput = null;

        dialogueTriggerUIParent.SetActive(false);
        onConversationComplete -= HandleDialogueTriggerComplete;
    }
}
