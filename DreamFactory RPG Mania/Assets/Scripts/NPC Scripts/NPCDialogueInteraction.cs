using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueInteraction : BaseNPCInteraction, INPCInteraction
{
    [Header("Dialogue Settings")]
    public Conversation targetConversation;

    public event EventHandler<InteractionCompletedArgs> onInteractionComplete;

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
