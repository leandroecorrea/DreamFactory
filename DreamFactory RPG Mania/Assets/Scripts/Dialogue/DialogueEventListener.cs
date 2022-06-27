using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueEventListener : MonoBehaviour
{
    [NonReorderable] public List<DialogueEventSubscription> dialogueEventSubscriptions;

    private void OnDestroy()
    {
        DialogueEventSystem.Unsubscribe(HandleDialogueEventFired);
    }

    private void Awake()
    {
        DialogueEventSystem.Subscribe(HandleDialogueEventFired);
    }

    public void HandleDialogueEventFired(object sender, DialogueEventFiredArgs eventFiredArgs)
    {
        foreach(DialogueEventSubscription eventSubscription in dialogueEventSubscriptions)
        {
            if (eventSubscription.dialogueEvent == eventFiredArgs.dialogueEvent && eventSubscription.onDialogueEventFire != null)
            {
                eventSubscription.onDialogueEventFire.Invoke();
            }
        }
    }
}

[System.Serializable]
public class DialogueEventSubscription
{
    public DialogueEvent dialogueEvent;
    public UnityEvent onDialogueEventFire;
}
