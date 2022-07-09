using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class CutsceneTrigger : MonoBehaviour
{
    [Header("Transition UI Refs")]
    [SerializeField] private GameObject transitionCanvasParent;
    [SerializeField] private Animator transitionAnimator;

    [Header("Cutscene Refs")]
    [SerializeField] private string cutsceneSceneName;

    [Header("Story Settings")]
    [SerializeField] private List<StoryPointKeys.StoryKeys> availableStoryPoints;

    [Header("Events")]
    [SerializeField] private UnityEvent beforeCutsceneStart;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && (availableStoryPoints?.Contains(StoryManager.GetCurrentStoryKey()) ?? false))
        {
            InitializeCutsceneTrigger();
        }
    }

    public void InitializeCutsceneTrigger()
    {
        transitionCanvasParent.SetActive(true);
        transitionAnimator.SetTrigger("Activate");
    }

    public void NotifyCutsceneTransitionFinished()
    {
        beforeCutsceneStart?.Invoke();
        PlayerOverworldPersistance.persistance.StorePosition();

        SceneManager.LoadScene(cutsceneSceneName);

        return;
    }

    public void UpdateStoryPoint(string targetStoryKeyStr)
    {
        try
        {
            StoryPointKeys.StoryKeys targetStoryKey = (StoryPointKeys.StoryKeys)Enum.Parse(typeof(StoryPointKeys.StoryKeys), targetStoryKeyStr);
            StoryManager.UpdateCurrentStoryKey(targetStoryKey);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }
}
