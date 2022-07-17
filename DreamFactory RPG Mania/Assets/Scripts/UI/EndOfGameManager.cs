using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGameManager : MonoBehaviour
{
    [Header("Navigation Settings")]
    [SerializeField] private string mainMenuSceneName;

    private void OnEnable()
    {
        // Removing Singleton Instances
        if (PlayerOverworldPersistance.persistance != null)
        {
            GameObject.Destroy(PlayerOverworldPersistance.persistance);
            PlayerOverworldPersistance.persistance = null;
        }

        if (CombatTransitionManager.instance != null)
        {
            GameObject.Destroy(CombatTransitionManager.instance);
            CombatTransitionManager.instance = null;
        }
    }

    public void HandleQuit()
    {
        Application.Quit();
    }

    public void HandleMainMenuNav()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
