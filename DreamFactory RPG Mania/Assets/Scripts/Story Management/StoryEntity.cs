using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoryEntity : MonoBehaviour
{
    [Header("Story Configuration")]
    [SerializeField] private List<StoryPointKeys.StoryKeys> availableStoryPoints;
    [SerializeField, NonReorderable] private List<StoryEntityListener> storyPointEvents;

    private void OnEnable()
    {
        if (!availableStoryPoints.Contains(StoryManager.GetCurrentStoryKey()))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        StoryManager.onStoryUpdate -= HandleStoryUpdateEvent;
    }

    public void Awake()
    {
        StoryManager.onStoryUpdate += HandleStoryUpdateEvent;
    }

    private void HandleStoryUpdateEvent(object sender, StoryUpdateEventArgs e)
    {
        foreach (StoryEntityListener storyListener in storyPointEvents)
        {
            if (e.oldStoryKey == storyListener.listenerStoryKey)
            {
                storyListener.onStoryExit?.Invoke();
            } 
            else  if (e.newStoryKey == storyListener.listenerStoryKey)
            {
                storyListener.onStoryEnter?.Invoke();
            }
        }
    }

    public void UpdateStoryPoint(string targetStoryKeyStr)
    {
        try
        {
            StoryPointKeys.StoryKeys targetStoryKey = (StoryPointKeys.StoryKeys)Enum.Parse(typeof(StoryPointKeys.StoryKeys), targetStoryKeyStr);
            StoryManager.UpdateCurrentStoryKey(targetStoryKey);
        }
        catch(Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }
}

[System.Serializable]
public class StoryEntityListener
{
    public StoryPointKeys.StoryKeys listenerStoryKey;

    public UnityEvent onStoryEnter;
    public UnityEvent onStoryExit;
}
