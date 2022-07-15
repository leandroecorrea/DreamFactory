using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Config", menuName = "Combat/Audio/Base Config")]
public class BaseAudioConfig : ScriptableObject
{
    [SerializeField] public AudioClip hit, spell, die;
}


