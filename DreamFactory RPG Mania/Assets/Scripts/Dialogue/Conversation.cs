using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Dialogue/New Conversation")]
public class Conversation : ScriptableObject
{
    public List<ConversationPoint> conversationPoints;
}

[System.Serializable]
public class ConversationPoint
{
    public string conversationPointSpeaker;
    public string conversationPointText;
}