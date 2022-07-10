using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNPCInteraction : MonoBehaviour
{
    [Header("Prefab Refs")]
    [SerializeField] protected GameObject interactionPrefab;

    [Header("Interaction UI Refs")]
    [SerializeField] protected Transform interactionUIMenuParent;

    protected GameObject interactionMenuInstance;
    protected INPCInteractionMenuHandler interactionMenuHandlerInstance;

    protected void CreateInteractionUIInstance()
    {
        interactionMenuInstance = GameObject.Instantiate(interactionPrefab, interactionUIMenuParent);
        interactionMenuHandlerInstance = interactionMenuInstance.GetComponent<INPCInteractionMenuHandler>();
    }

    protected void CleanUI()
    {
        GameObject.Destroy(interactionMenuInstance);
    }
}
