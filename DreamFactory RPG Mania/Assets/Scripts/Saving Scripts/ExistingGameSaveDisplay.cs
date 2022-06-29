using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ExistingGameSaveDisplay : MonoBehaviour
{
    [Header("UI Component Refs")]
    [SerializeField] private GameObject noExistingDataUIParent;
    [SerializeField] private GameObject existingDataUIParent;
    [SerializeField] private TextMeshProUGUI gameSaveName;

    [Header("Scene Management Refs")]
    [SerializeField] private string newGameTargetScene;
    [SerializeField] private string existingGameTargetScene;

    private PlayerSaveDisplay playerSaveDisplay;

    private void Awake()
    {
        LoadGameSaveDisplay();
        UpdateGameSaveUI();
    }

    public void LoadGameSaveDisplay()
    {
        playerSaveDisplay = PlayerProgression.GetPlayerSaveDisplay(transform.GetSiblingIndex());
    }

    public void UpdateGameSaveUI()
    {
        if (playerSaveDisplay.isNewSave)
        {
            noExistingDataUIParent.SetActive(true);
            existingDataUIParent.SetActive(false);

            return;
        }

        existingDataUIParent.SetActive(true);
        noExistingDataUIParent.SetActive(false);

        gameSaveName.text = playerSaveDisplay.fileName;
    }

    public void HandleSelectGameSave()
    {
        PlayerProgression.LoadPlayerSave(transform.GetSiblingIndex());
        SceneManager.LoadScene((playerSaveDisplay.isNewSave) ? newGameTargetScene : existingGameTargetScene);
    }
}
