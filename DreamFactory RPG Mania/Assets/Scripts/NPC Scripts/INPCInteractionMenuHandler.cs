using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INPCInteractionMenuHandler
{
    INPCInteraction interactionHandler { get; set; }

    void InitializeMenuHandler(INPCInteraction interactionHandler);
}
