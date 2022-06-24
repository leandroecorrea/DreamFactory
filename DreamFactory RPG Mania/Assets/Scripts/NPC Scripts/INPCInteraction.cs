using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INPCInteraction
{
    bool CanExecuteInteraction();
    NPCInteractionUIDisplay GetInteractionDisplay();
    void StartInteraction();
}

public class NPCInteractionUIDisplay
{
    public INPCInteraction interactionInterface;
    public string displayText;
}