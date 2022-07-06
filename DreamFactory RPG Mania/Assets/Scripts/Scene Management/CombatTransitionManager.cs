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

        List<CombatEntityConfig> targetPlayerConfigs = PlayerPartyManager.GetAllUnlockedCombatConfigs();
        CombatManager.currentStartRequest = new CombatStartRequest(encounterConfig.enemies, targetPlayerConfigs, SceneManager.GetActiveScene().name);

        transitionUIParentCanvas.SetActive(true);
        anim.SetTrigger("Display");
    }

    public void InitializeCombatTransition(CombatStartRequest startRequest, string targetCombatScene)
    {
        PlayerOverworldPersistance.persistance.StorePosition();
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
        transitionUIParentCanvas.SetActive(false);
    }

    public void CloseCombatTransition()
    {
        transitionUIParentCanvas.SetActive(false);
    }
}
