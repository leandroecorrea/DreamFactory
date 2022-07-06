using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class NPCDialogueMenuHandler : MonoBehaviour, INPCInteractionMenuHandler
{
    public INPCInteraction interactionHandler { get; set; }
    private NPCDialogueInteraction dialogueInteraction;

    [Header("Component References")]
    [SerializeField] private DialogueManager dialogueManager;

    public void InitializeMenuHandler(INPCInteraction interactionHandler)
    {
        this.interactionHandler = interactionHandler;
        dialogueInteraction = (NPCDialogueInteraction)interactionHandler;

        dialogueManager.onConversationComplete += HandleConversationComplete;
        dialogueManager.InitializeConversation(dialogueInteraction.targetConversation);
    }

    private void HandleConversationComplete(object sender, ConversationCompletedArgs e)
    {
        dialogueManager.onConversationComplete -= HandleConversationComplete;
        dialogueInteraction.CompleteInteraction();
    }
}
