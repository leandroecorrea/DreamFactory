using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EntitySoundConfig : MonoBehaviour
{    
    [SerializeField] BaseAudioConfig audioConfig;
    [SerializeField] AudioSource audioSource;

    public void PlayHitSound()
    {
        audioSource.clip = audioConfig.hit;
        audioSource.Play();
    }

    public void PlaySpellSound()
    {
        audioSource.clip = audioConfig.spell;
        audioSource.Play();
    }
    public void PlayDieSound()
    {
        audioSource.clip = audioConfig.die;
        audioSource.Play();
    }
}
