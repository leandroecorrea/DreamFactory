using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueInteraction : BaseNPCInteraction, INPCInteraction
{
    [Header("Dialogue Settings")]
    public Conversation defaultConversation;
    [NonReorderable] public List<DialogueInteractionSettings> allDialogueInterations;

    public event EventHandler<InteractionCompletedArgs> onInteractionComplete;
    public Conversation targetConversation
    {
        get
        {
            StoryPointKeys.StoryKeys currentStoryKey = StoryManager.GetCurrentStoryKey();
            foreach(DialogueInteractionSettings dialogueInteraction in allDialogueInterations)
            {
                if (dialogueInteraction.availableStoryPoints.Contains(currentStoryKey))
                {
                    return dialogueInteraction.conversation;
            }
                }

            return defaultConversation;
        }
    }

    public bool CanExecuteInteraction()
    {
        return (targetConversation != null);
    }

    public NPCInteractionUIDisplay GetInteractionDisplay()
    {
        return new NPCInteractionUIDisplay() { displayText = "Talk", interactionInterface = this };
    }

    public void StartInteraction()
    {
        CreateInteractionUIInstance();
        interactionMenuHandlerInstance.InitializeMenuHandler(this);
    }

    public void CompleteInteraction()
    {
        CleanUI();
        onInteractionComplete?.Invoke(this, new InteractionCompletedArgs());
    }
}

[System.Serializable]
public class DialogueInteractionSettings
{
    public Conversation conversation;
    public List<StoryPointKeys.StoryKeys> availableStoryPoints;
}