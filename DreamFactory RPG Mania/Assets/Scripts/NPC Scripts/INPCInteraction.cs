using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INPCInteraction
{
    bool CanExecuteInteraction();
    NPCInteractionUIDisplay GetInteractionDisplay();

    void StartInteraction();
    void CompleteInteraction();

    event EventHandler<InteractionCompletedArgs> onInteractionComplete;
}

public class NPCInteractionUIDisplay
{
    public INPCInteraction interactionInterface;
    public string displayText;
}

public class InteractionCompletedArgs : EventArgs
{

}