using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StoryManager
{
    private static StoryPointKeys.StoryKeys _currentStoryKey = StoryPointKeys.StoryKeys.Unset;

    public static event EventHandler<StoryUpdateEventArgs> onStoryUpdate;

    private static void LoadCurrentStoryKeyFromSave()
    {
        _currentStoryKey = PlayerProgression.GetPlayerData<StoryPointKeys.StoryKeys>(SaveKeys.CURRENT_STORY_POINT);
    }

    public static StoryPointKeys.StoryKeys GetCurrentStoryKey()
    {
        if (_currentStoryKey == StoryPointKeys.StoryKeys.Unset)
        {
            LoadCurrentStoryKeyFromSave();
        }

        return _currentStoryKey;
    }

    public static void UpdateCurrentStoryKey(StoryPointKeys.StoryKeys newTargetKey)
    {
        if (newTargetKey < _currentStoryKey)
        {
            Debug.LogError($"Trying to Update Story from {_currentStoryKey} to {newTargetKey} which would regress the story");
            return;
        }

        StoryPointKeys.StoryKeys oldStoryKey = _currentStoryKey;
        _currentStoryKey = newTargetKey;

        onStoryUpdate?.Invoke(null, new StoryUpdateEventArgs(oldStoryKey, _currentStoryKey));
    }
}

public class StoryUpdateEventArgs : EventArgs
{
    public StoryPointKeys.StoryKeys oldStoryKey;
    public StoryPointKeys.StoryKeys newStoryKey;

    public StoryUpdateEventArgs(StoryPointKeys.StoryKeys oldStoryKey, StoryPointKeys.StoryKeys newStoryKey)
    {
        this.oldStoryKey = oldStoryKey;
        this.newStoryKey = newStoryKey;
    }
}