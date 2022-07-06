using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatTransitionManager : MonoBehaviour
{
    public static CombatTransitionManager instance;

    [Header("UI References")]
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject transitionUIParentCanvas;

    string targetCombatScene;

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

    public void InitializeCombatTransition(CombatEncounterConfig encounterConfig)
    {
        this.targetCombatScene = encounterConfig.targetCombatSceneName;
        CombatManager.currentStartRequest = new CombatStartRequest(PlayerPartyManager.GetAllUnlockedCombatConfigs(), encounterConfig.enemies, SceneManager.GetActiveScene().name);

        transitionUIParentCanvas.SetActive(true);
        anim.SetTrigger("Display");
    }

    public void InitializeCombatTransition(CombatStartRequest startRequest, string targetCombatScene)
    {
        CombatManager.currentStartRequest = startRequest;
        this.targetCombatScene = targetCombatScene;

        transitionUIParentCanvas.SetActive(true);
        anim.SetTrigger("Display");
    }

    public void HandleCombatTransitionComplete()
    {
        SceneManager.LoadScene(targetCombatScene);
        targetCombatScene = "";

        anim.SetTrigger("Hide");
    }

    public void CloseCombatTransition()
    {
        transitionUIParentCanvas.SetActive(false);
    }
}
