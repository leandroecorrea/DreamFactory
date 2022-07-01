using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionTriggerHandler : MonoBehaviour
{
    [Header("Handler Settings")]
    [SerializeField] private string sceneName;

    private SceneTransitionTrigger.LoadingOperation currentLoadingOperation;

    private void Awake()
    {
        currentLoadingOperation = SceneTransitionTrigger.LoadingOperation.Waiting;
    }

    public void NotifyTriggerEnter(SceneTransitionTrigger.LoadingOperation targetLoadingOperation)
    {
        // If the passed in operation is the same as the store operation
        // That means the player exited the first trigger, go back to waiting state
        if (targetLoadingOperation == currentLoadingOperation)
        {
            currentLoadingOperation = SceneTransitionTrigger.LoadingOperation.Waiting;
            return;
        }

        // Stored Loading Operation hasn't been set by a trigger (I.e this is the first notify trigger)
        if (currentLoadingOperation == SceneTransitionTrigger.LoadingOperation.Waiting)
        {
            currentLoadingOperation = targetLoadingOperation;
            return;
        }

        ExecuteLoadingOperation();
    }

    private void ExecuteLoadingOperation()
    {
        switch(currentLoadingOperation)
        {
            case SceneTransitionTrigger.LoadingOperation.Load:
                SceneTransitionManager.RequestAdditiveLoadSceneOperation(sceneName);
                break;
            case SceneTransitionTrigger.LoadingOperation.Unload:
                SceneTransitionManager.RequestAdditiveUnloadSceneOperation(sceneName);
                break;
            default:
                Debug.LogWarning($"Trying to Execute Loading Operation {currentLoadingOperation} from gameObject: {gameObject.name}");
                break;
        }

        currentLoadingOperation = SceneTransitionTrigger.LoadingOperation.Waiting;
    }
}
