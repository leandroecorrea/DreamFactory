using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueInteraction : BaseNPCInteraction, INPCInteraction
{
    [Header("Dialogue Settings")]
    public Conversation targetConversation;

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
        GameObject dialogueMenu = GameObject.Instantiate(interactionPrefab, interactionUIMenuParent);
        dialogueMenu.GetComponent<NPCDialogueMenuHandler>().InitializeMenuHandler(this);
    }
}
