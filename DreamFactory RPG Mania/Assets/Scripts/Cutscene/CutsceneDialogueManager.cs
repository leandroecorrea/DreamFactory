using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDialogueManager : DialogueManager
{
    public GameObject cutsceneUIParent;

    [Header("Conversation Settings")]
    [SerializeField] private Conversation cutsceneConversation;

    public void StartCutsceneDialogue()
    {
        InitializeConversation(cutsceneConversation);
        ToggleCutsceneDialogueUI(true);
    }

    public void ToggleCutsceneDialogueUI(bool display)
    {
        cutsceneUIParent.SetActive(display);
    }
}
