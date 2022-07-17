using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShopInteraction : BaseNPCInteraction, INPCInteraction
{
    public event EventHandler<InteractionCompletedArgs> onInteractionComplete;

    public bool CanExecuteInteraction()
    {
        return true;
    }

    public void CompleteInteraction()
    {
        CleanUI();
        onInteractionComplete?.Invoke(this, new InteractionCompletedArgs());
    }

    public NPCInteractionUIDisplay GetInteractionDisplay()
    {
        return new NPCInteractionUIDisplay() { displayText = "Shop", interactionInterface = this };
    }

    public void StartInteraction()
    {
        CreateInteractionUIInstance();
        interactionMenuHandlerInstance.InitializeMenuHandler(this);
    }
}
