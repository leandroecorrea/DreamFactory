using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneTransitionManager
{
    // Track currently loaded scenes to prevent unloading unloaded scenes or loading loaded scenes
    private static HashSet<string> _currentlyLoadedScenes;

    public static HashSet<string> CurrentlyLoadedScenes {
        get
        {
            if (_currentlyLoadedScenes == null)
            {
                _currentlyLoadedScenes = new HashSet<string> { SceneManager.GetActiveScene().name };
            }

            return _currentlyLoadedScenes;
        }

        private set {
            _currentlyLoadedScenes = value;
        }
    }

    public static AsyncOperation RequestAdditiveLoadSceneOperation(string targetSceneName)
    {
        if (CurrentlyLoadedScenes.Contains(targetSceneName))
        {
            Debug.LogWarning($"Requested Scene ({targetSceneName}) to be loaded when its already loaded");
            return null;
        }

        CurrentlyLoadedScenes.Add(targetSceneName);
        return SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive);
    }

    public static AsyncOperation RequestAdditiveUnloadSceneOperation(string targetSceneName)
    {
        if (!CurrentlyLoadedScenes.Contains(targetSceneName))
        {
            Debug.LogWarning($"Requested Scene ({targetSceneName}) to be unloaded when it hasn't been loaded");
            return null;
        }

        CurrentlyLoadedScenes.Remove(targetSceneName);
        return SceneManager.UnloadSceneAsync(targetSceneName);
    }

    public static void LoadNewSingleScene(string targetSceneName)
    {
        SceneManager.LoadScene(targetSceneName, LoadSceneMode.Single);

        // Loading Scene with Single Mode will unload all other scenes
        // Clear the stored Hashset
        CurrentlyLoadedScenes.Clear();
    }
}
