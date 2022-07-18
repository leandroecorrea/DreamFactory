using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            GameObject.DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();

            return;
        }

        GameObject.Destroy(gameObject);
        return;
    }

    public void StartNewMusicTrack(AudioClip targetMusicClip)
    {
        if (audioSource.isPlaying)
        {
            if (audioSource.clip == targetMusicClip)
            {
                return;
            }

            audioSource.Stop();
        }

        audioSource.clip = targetMusicClip;
        audioSource.Play();

        return;
    }

    public void StopCurrentTrack()
    {
        if (!audioSource.isPlaying)
        {
            return;
        }

        audioSource.Stop();
        audioSource.clip = null;
    }
}
