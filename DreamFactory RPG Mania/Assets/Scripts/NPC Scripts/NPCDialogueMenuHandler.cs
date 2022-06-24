using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCDialogueMenuHandler : MonoBehaviour, INPCInteractionMenuHandler
{
    public INPCInteraction interactionHandler { get; set; }

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private NPCDialogueInteraction dialogueInteraction;

    public void InitializeMenuHandler(INPCInteraction interactionHandler)
    {
        this.interactionHandler = interactionHandler;
        this.dialogueInteraction = (NPCDialogueInteraction)interactionHandler;

        ConversationPoint conversationStartingPoint = dialogueInteraction.targetConversation.conversationPoints[0];
        speakerText.text = conversationStartingPoint.conversationPointSpeaker;
        dialogueText.text = conversationStartingPoint.conversationPointText;
    }
}
