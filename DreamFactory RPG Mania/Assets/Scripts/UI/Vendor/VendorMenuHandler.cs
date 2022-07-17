using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorMenuHandler : MonoBehaviour, INPCInteractionMenuHandler
{
    public INPCInteraction interactionHandler { get; set; }

    // ADDED FOR INTERFACE
    public void InitializeMenuHandler(INPCInteraction interactionHandler)
    {
        this.interactionHandler = interactionHandler;
    }

    public void HandleSelectExit()
    {
        interactionHandler.CompleteInteraction();
    }

    public void HandleSelectBuy()
    {
        interactionHandler.CompleteInteraction();
    }
}
