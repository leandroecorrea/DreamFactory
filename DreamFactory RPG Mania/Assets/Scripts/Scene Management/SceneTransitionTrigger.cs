using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionTrigger : MonoBehaviour
{
    public enum LoadingOperation
    { 
        Waiting, // Waiting is only used by the trigger handler
        Load, 
        Unload 
    };

    [Header("Transition Settings")]
    [SerializeField] private SceneTransitionTriggerHandler triggerHandler;
    [SerializeField] private LoadingOperation loadingOperation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerHandler.NotifyTriggerEnter(loadingOperation);
            return;
        }
    }
}
