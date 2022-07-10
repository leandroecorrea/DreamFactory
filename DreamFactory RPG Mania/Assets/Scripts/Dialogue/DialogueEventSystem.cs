using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogueEventSystem
{
    private static event EventHandler<DialogueEventFiredArgs> onDialogueEventFire;

    public static void Subscribe(EventHandler<DialogueEventFiredArgs> dialogueEventSubscription)
    {
        onDialogueEventFire += dialogueEventSubscription;
        return;
    }

    public static void Unsubscribe(EventHandler<DialogueEventFiredArgs> dialogueEventSubscription)
    {
        onDialogueEventFire -= dialogueEventSubscription;
        return;
    }

    public static void Publish(DialogueEvent dialogueEventToPublish)
    {
        onDialogueEventFire?.Invoke(null, new DialogueEventFiredArgs { dialogueEvent = dialogueEventToPublish });
    }
}

public class DialogueEventFiredArgs : EventArgs
{
    public DialogueEvent dialogueEvent;
}