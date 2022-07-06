using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneTrigger : MonoBehaviour
{
    [Header("Transition UI Refs")]
    [SerializeField] private GameObject transitionCanvasParent;
    [SerializeField] private Animator transitionAnimator;

    [Header("Cutscene Refs")]
    [SerializeField] private string cutsceneSceneName;

    [Header("Story Settings")]
    [SerializeField] private List<StoryPointKeys.StoryKeys> availableStoryPoints;

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
        SceneManager.LoadScene(cutsceneSceneName);
        return;
    }
}
