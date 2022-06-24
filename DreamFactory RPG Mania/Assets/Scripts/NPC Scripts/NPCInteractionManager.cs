using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NPCInteractionManager : MonoBehaviour
{
    [Header("Prefab References")]
    [SerializeField] private GameObject npcInteractionOptionPrefab;

    [Header("UI References")]
    [SerializeField] private GameObject interactionUIParent;
    [SerializeField] private Transform interactionMenuParent;
    [SerializeField] private Transform interactionOptionListParent;

    private List<INPCInteraction> interactions;

    private void Awake()
    {
        interactions = gameObject.GetComponents<INPCInteraction>().ToList();
    }

    public bool HasInteractions()
    {
        return (interactions != null && interactions.Count > 0);
    }

    public void InitializeInteractionUI()
    {
        // Clean any existing interaction options
        if (interactionOptionListParent.childCount > 0)
        {
            for (int i = interactionOptionListParent.childCount - 1; i >= 0; i--)
            {
                GameObject.Destroy(interactionOptionListParent.GetChild(i).gameObject);
            }
        }
        
        // Instantiate New interaction options
        foreach(INPCInteraction interaction in interactions)
        {
            if (interaction.CanExecuteInteraction())
            {
                GameObject newInteractionOption = GameObject.Instantiate(npcInteractionOptionPrefab, interactionOptionListParent);

                NPCInteractionOption interactionOptionHandler = newInteractionOption.GetComponent<NPCInteractionOption>();
                interactionOptionHandler.InitializeOptionDisplay(interaction.GetInteractionDisplay());
                interactionOptionHandler.onInteractionSelect += HandleInteractionSelect;
            }
        }

        // Enable the UI
        interactionMenuParent.gameObject.SetActive(false);
        interactionOptionListParent.gameObject.SetActive(true);
        interactionUIParent.SetActive(true);
    }

    private void HandleInteractionSelect(object sender, NPCInteractionSelectedArgs e)
    {
        INPCInteraction interactionInterface = e.selectedInteraction;
        if (interactionInterface != null)
        {
            interactionInterface.StartInteraction();
        }

        interactionMenuParent.gameObject.SetActive(true);
        interactionOptionListParent.gameObject.SetActive(false);
    }
}
