using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC Config", menuName = "NPC/New NPC Config")]
public class NPCConfig : ScriptableObject
{
    [Header("Character Config")]
    public string characterName;
}
