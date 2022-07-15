using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTheme : MonoBehaviour
{
    [SerializeField] AudioSource introToTheme;
    [SerializeField] AudioSource themeLoop;

    
    void Awake()
    {
        introToTheme.Play();
        StartCoroutine(IntroToThemeLoop());
    }
    private void Start()
    {
        CombatEventSystem.instance.onCombatFinished += StopAllSounds;        
    }

    private IEnumerator IntroToThemeLoop()
    {
        while(introToTheme.isPlaying)
        {
            yield return null;
        }
        Debug.Log("trigger loop");
        themeLoop.Play();
    }

    public void StopAllSounds(CombatResult result)
    {
        CombatEventSystem.instance.onCombatFinished -= StopAllSounds;
        StopAllCoroutines();
        if(introToTheme.isPlaying)
            introToTheme.Stop();
        else if(themeLoop.isPlaying)
            themeLoop.Stop();
    }
}
