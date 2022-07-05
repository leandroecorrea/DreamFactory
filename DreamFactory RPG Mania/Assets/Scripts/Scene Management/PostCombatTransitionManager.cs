using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostCombatTransitionManager : MonoBehaviour
{
    public static PostCombatTransitionManager instance;
    [Header("UI References")]
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject transitionUIParentCanvas;

    string _targetCombatScene;
    private string _originScene;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);

            return;
        }
        else
        {
            GameObject.Destroy(gameObject);
            return;
        }
    }
    public void InitializePostCombatTransition(CombatResult result, int experienceReward, string previousCombatScene)
    {
        _originScene = previousCombatScene;
        if (result == CombatResult.WIN)
            _targetCombatScene = "Win combat scene";
        transitionUIParentCanvas.SetActive(true);
        anim.SetTrigger("Display");
    }

    public void HandleCombatTransitionComplete()
    {
        SceneManager.LoadScene(_targetCombatScene);
        if(_targetCombatScene == _originScene)
        {
            PlayerOverworldPersistance.persistance.ResetPosition();
        }
        anim.SetTrigger("Hide");
        _targetCombatScene = "";
        transitionUIParentCanvas.gameObject.SetActive(false);
    }

    public void CloseCombatTransition()
    {
        transitionUIParentCanvas.SetActive(false);
    }

    public void InitializeBackToPreviousScene()
    {
        _targetCombatScene = _originScene;                
        transitionUIParentCanvas.SetActive(true);
        anim.SetTrigger("Display");
    }

}
