using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ExistingGameSaveDisplay : MonoBehaviour
{
    [Header("Prefab Refs")]
    [SerializeField] private GameObject unlockedMemberPortraitPrefab;

    [Header("UI Component Refs")]
    [SerializeField] private GameObject noExistingDataUIParent;
    [SerializeField] private GameObject existingDataUIParent;
    [SerializeField] private TextMeshProUGUI gameSaveName;
    [SerializeField] private Transform unlockedMemberPortraitParent;

    [Header("Scene Management Refs")]
    [SerializeField] private string newGameTargetScene;
    [SerializeField] private string existingGameTargetScene;

    private PlayerSaveDisplay playerSaveDisplay;

    private void Awake()
    {
        LoadGameSaveDisplay();
        UpdateGameSaveUI();
    }

    private void LoadGameSaveDisplay()
    {
        playerSaveDisplay = PlayerProgression.GetPlayerSaveDisplay(transform.GetSiblingIndex());
    }

    private void UpdateGameSaveUI()
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
        CreateUnlockedCharacterDisplays();
    }

    private void CreateUnlockedCharacterDisplays()
    {
        foreach(PlayerPartyMemberConfig unlockedPartyMember in playerSaveDisplay.unlockedPartyMembers)
        {
            GameObject newUnlockedCharacterPortrait = Instantiate(unlockedMemberPortraitPrefab, unlockedMemberPortraitParent);
            newUnlockedCharacterPortrait.GetComponent<Image>().sprite = unlockedPartyMember.CharacterDetails.characterPortrait;
        }
    }

    public void HandleSelectGameSave()
    {
        PlayerProgression.LoadPlayerSave(transform.GetSiblingIndex());
        SceneManager.LoadScene((playerSaveDisplay.isNewSave) ? newGameTargetScene : existingGameTargetScene);
    }
}