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
    private Coroutine currentWritingCoroutine;
    private int currentConversationPointIndex;

    private ConversationPoint currentConversationPoint
    {
        get
        {
            return dialogueInteraction?.targetConversation.conversationPoints[currentConversationPointIndex];
        }
    }

    public void InitializeMenuHandler(INPCInteraction interactionHandler)
    {
        this.interactionHandler = interactionHandler;

        dialogueInteraction = (NPCDialogueInteraction)interactionHandler;
        currentConversationPointIndex = 0;

        InitializeConversationPointState(currentConversationPoint);
    }

    private void InitializeConversationPointState(ConversationPoint targetConversationPoint)
    {
        // The hard coded "Player" may be replaced with a player selected name at some point
        string speakerDisplayText = (targetConversationPoint.isPlayerSpeaking) ? "Player" : targetConversationPoint.conversationPointSpeaker.characterName;

        speakerText.text = speakerDisplayText;
        dialogueText.text = "";

        currentWritingCoroutine = StartCoroutine(WriteCurrentDialogueText());
    }

    private IEnumerator WriteCurrentDialogueText()
    {
        string runningDialogueString = "";
        int currentDialogueTextCharacterIndex = 0;

        while(currentDialogueTextCharacterIndex < currentConversationPoint.conversationPointText.Length)
        {
            runningDialogueString += currentConversationPoint.conversationPointText[currentDialogueTextCharacterIndex];
            dialogueText.text = runningDialogueString;

            currentDialogueTextCharacterIndex++;
            yield return new WaitForSeconds(currentConversationPoint.typingDelay);
        }

        currentWritingCoroutine = null;
    }

    public void AdvanceDialogue()
    {
        if (currentWritingCoroutine != null)
        {
            StopCoroutine(currentWritingCoroutine);

            dialogueText.text = currentConversationPoint.conversationPointText;
            currentWritingCoroutine = null;

            return;
        }

        currentConversationPointIndex += 1;
        if (currentConversationPointIndex == dialogueInteraction.targetConversation.conversationPoints.Count)
        {
            dialogueInteraction.CompleteInteraction();
            return;
        }

        InitializeConversationPointState(currentConversationPoint);
    }
}
