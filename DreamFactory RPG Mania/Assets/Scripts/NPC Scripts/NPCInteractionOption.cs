using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCInteractionOption : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pointer;
    [SerializeField] private TextMeshProUGUI interactionOptionText;

    private INPCInteraction interactionInterface;
    public event EventHandler<NPCInteractionSelectedArgs> onInteractionSelect;

    private void OnDestroy()
    {
        foreach (Delegate handler in onInteractionSelect.GetInvocationList())
        {
            onInteractionSelect -= (EventHandler<NPCInteractionSelectedArgs>)handler;
        }
    }

    public void HandlePointerEnter() { pointer.SetActive(true); }
    public void HandlePointerExit() { pointer.SetActive(false); }

    public void InitializeOptionDisplay(NPCInteractionUIDisplay uiDisplayConfig)
    {
        interactionInterface = uiDisplayConfig.interactionInterface;
        interactionOptionText.text = uiDisplayConfig.displayText;
    }

    public void HandleSelect()
    {
        onInteractionSelect?.Invoke(this, new NPCInteractionSelectedArgs { selectedInteraction = interactionInterface });
    }
}

public class NPCInteractionSelectedArgs : EventArgs
{
    public INPCInteraction selectedInteraction;
} 