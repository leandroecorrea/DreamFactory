using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    [Header("Trigger Settings")]
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private bool triggerOnAwake;
    [SerializeField] private bool triggerOnEnter;

    private void Awake()
    {
        Debug.Log($"{triggerOnAwake} VS {MusicManager.instance != null}");
        if (triggerOnAwake && MusicManager.instance != null)
        {
            MusicManager.instance.StartNewMusicTrack(musicClip);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{triggerOnEnter} VS {MusicManager.instance != null}");
        if (triggerOnEnter && MusicManager.instance != null)
        {
            MusicManager.instance.StartNewMusicTrack(musicClip);
        }
    }
}
