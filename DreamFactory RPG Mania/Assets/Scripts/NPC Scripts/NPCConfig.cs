using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC Config", menuName = "NPC/New NPC Config")]
public class NPCConfig : ScriptableObject
{
    [Header("Character Config")]
    public string characterName;
    public List<AudioClip> characterDialogueSounds;

    public AudioClip GetRandomAudioClip()
    {
        if (characterDialogueSounds == null || characterDialogueSounds.Count == 0)
        {
            return null;
        }

        int targetIndex = Random.Range(0, characterDialogueSounds.Count);
        return characterDialogueSounds[targetIndex];
    }
}
