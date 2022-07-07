using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject proceedButton;

    private Conversation currentConversation;
    private Coroutine currentWritingCoroutine;
    private int currentConversationPointIndex;

    public event EventHandler<ConversationCompletedArgs> onConversationComplete;

    private ConversationPoint currentConversationPoint
    {
        get
        {
            return currentConversation.conversationPoints[currentConversationPointIndex];
        }
    }

    public void InitializeConversation(Conversation targetConversation)
    {
        currentConversation = targetConversation;
        currentConversationPointIndex = 0;

        InitializeConversationPointState(currentConversationPoint);
    }

    private void InitializeConversationPointState(ConversationPoint targetConversationPoint)
    {
        if (targetConversationPoint.onStartEvent != null && targetConversationPoint.onStartEvent.Count > 0)
        {
            foreach (DialogueEvent diagEvent in targetConversationPoint.onStartEvent)
            {
                DialogueEventSystem.Publish(diagEvent);
            }
        }

        speakerText.text = targetConversationPoint.conversationPointSpeaker.characterName;
        dialogueText.text = "";

        EventSystem.current?.SetSelectedGameObject(proceedButton);

        currentWritingCoroutine = StartCoroutine(WriteCurrentDialogueText());
    }

    private IEnumerator WriteCurrentDialogueText()
    {
        string runningDialogueString = "";
        int currentDialogueTextCharacterIndex = 0;

        while (currentDialogueTextCharacterIndex < currentConversationPoint.conversationPointText.Length)
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

        if (currentConversationPoint.onCompleteEvent != null && currentConversationPoint.onCompleteEvent.Count > 0)
        {
            foreach(DialogueEvent diagEvent in currentConversationPoint.onCompleteEvent)
            {
                DialogueEventSystem.Publish(diagEvent);
            }
        }

        currentConversationPointIndex += 1;
        if (currentConversationPointIndex == currentConversation.conversationPoints.Count)
        {
            DialogueEvent conversationCompleteEvent = currentConversation.onConversationCompleteEvent;
            if (conversationCompleteEvent != null)
            {
                DialogueEventSystem.Publish(conversationCompleteEvent);
            }

            onConversationComplete?.Invoke(this, new ConversationCompletedArgs { conversation = currentConversation });
            return;
        }

        InitializeConversationPointState(currentConversationPoint);
    }
}

public class ConversationCompletedArgs : EventArgs
{
    public Conversation conversation;
}