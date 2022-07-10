using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Dialogue/New Conversation")]
public class Conversation : ScriptableObject
{
    [NonReorderable] public List<ConversationPoint> conversationPoints;
    public DialogueEvent onConversationCompleteEvent;
}

[System.Serializable]
public class ConversationPoint
{
    public NPCConfig conversationPointSpeaker;

    public string conversationPointText;
    public float typingDelay;

    public List<DialogueEvent> onStartEvent;
    public List<DialogueEvent> onCompleteEvent;
}