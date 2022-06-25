using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Dialogue/New Conversation")]
public class Conversation : ScriptableObject
{
    [NonReorderable] public List<ConversationPoint> conversationPoints;
}

[System.Serializable]
public class ConversationPoint
{
    public bool isPlayerSpeaking;
    public NPCConfig conversationPointSpeaker;

    public string conversationPointText;
    public float typingDelay;
}