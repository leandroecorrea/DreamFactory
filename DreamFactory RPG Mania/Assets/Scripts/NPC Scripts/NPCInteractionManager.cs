using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class NPCInteractionManager : MonoBehaviour
{
    [Header("Prefab References")]
    [SerializeField] private GameObject npcInteractionOptionPrefab;
    [SerializeField] private GameObject dismissNPCInteractionUIPrefab;

    [Header("Character Config")]
    [SerializeField] private NPCConfig npcConfig;

    [Header("Component Refs")]
    [SerializeField] private Animator npcAnim;

    [Header("UI References")]
    [SerializeField] private GameObject selectInteractionUIParent;
    [SerializeField] private GameObject interactionUIParent;
    [SerializeField] private Transform interactionMenuParent;
    [SerializeField] private Transform interactionOptionListParent;
    [SerializeField] private TextMeshProUGUI npcNameText;

    public event EventHandler onInteractionMenuDismiss;

    private List<INPCInteraction> interactions;

    private void Awake()
    {
        interactions = gameObject.GetComponents<INPCInteraction>().ToList();
    }

    public bool HasInteractions()
    {
        return (interactions != null && interactions.Count > 0);
    }

    public void InitailizeInteraction()
    {
        npcAnim.SetBool("IsTalking", true);
        InitializeInteractionUI();
    }

    public void InitializeInteractionUI()
    {
        CleanSelectInteractionUI();
        BuildSelectInteractionUI();
        EnableSelectInteraction();
    }

    private void CleanSelectInteractionUI()
    {
        // Clean any existing interaction options
        if (interactionOptionListParent.childCount > 0)
        {
            for (int i = interactionOptionListParent.childCount - 1; i >= 0; i--)
            {
                GameObject.Destroy(interactionOptionListParent.GetChild(i).gameObject);
            }
        }
    }

    private void BuildSelectInteractionUI()
    {
        npcNameText.text = npcConfig.characterName;

        // Instantiate New interaction options
        foreach (INPCInteraction interaction in interactions)
        {
            GameObject newInteractionOption = GameObject.Instantiate(npcInteractionOptionPrefab, interactionOptionListParent);
            newInteractionOption.GetComponent<Button>().interactable = (interaction.CanExecuteInteraction());

            NPCInteractionOption interactionOptionHandler = newInteractionOption.GetComponent<NPCInteractionOption>();
            interactionOptionHandler.InitializeOptionDisplay(interaction.GetInteractionDisplay());
            interactionOptionHandler.onInteractionSelect += HandleInteractionSelect;
        }

        GameObject dismissInteractionUIInstance = GameObject.Instantiate(dismissNPCInteractionUIPrefab, interactionOptionListParent);
        dismissInteractionUIInstance.GetComponent<Button>().onClick.AddListener(DismissInteractionUI);
    }

    private void EnableSelectInteraction()
    {
        // Enable the UI
        interactionMenuParent.gameObject.SetActive(false);
        selectInteractionUIParent.SetActive(true);
        interactionUIParent.SetActive(true);

        EventSystem.current.SetSelectedGameObject(interactionOptionListParent.GetChild(0).gameObject);
    }

    private void DisableInteractionUI()
    {
        // Enable the UI
        interactionMenuParent.gameObject.SetActive(false);
        selectInteractionUIParent.SetActive(false);
        interactionUIParent.SetActive(false);
    }

    private void HandleInteractionSelect(object sender, NPCInteractionSelectedArgs e)
    {
        interactionMenuParent.gameObject.SetActive(true);
        selectInteractionUIParent.SetActive(false);

        INPCInteraction interactionInterface = e.selectedInteraction;
        if (interactionInterface != null)
        {
            interactionInterface.onInteractionComplete += HandleNPCInteractionComplete;
            interactionInterface.StartInteraction();
        }
    }

    private void HandleNPCInteractionComplete(object sender, InteractionCompletedArgs e)
    {
        EnableSelectInteraction();
    }

    public void DismissInteractionUI()
    {
        npcAnim.SetBool("IsTalking", false);

        CleanSelectInteractionUI();
        DisableInteractionUI();

        onInteractionMenuDismiss?.Invoke(this, EventArgs.Empty);
    }
}
