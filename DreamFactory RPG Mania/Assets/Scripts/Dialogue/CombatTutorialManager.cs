using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTutorialManager : DialogueManager
{
    [Header("Tutorial Settings")]
    [SerializeField] private Conversation combatTutorial;

    private void Awake()
    {
        InitializeConversation(combatTutorial);
    }
}
