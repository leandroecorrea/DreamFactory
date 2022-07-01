using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{    
    [SerializeField] private GameObject charactersView;
    [SerializeField] private GameObject mainOptions;
    [SerializeField] private GameObject itemOptions;
    [SerializeField] private GameObject equipmentOptions;
    [SerializeField] private GameObject abilitiesOptions;
    [SerializeField] private Button backToMain;
    [SerializeField] private EventSystem eventSystem;
    private GameObject _defaultFirst;

    void OnEnable()
    {
        var playerParty = PlayerPartyManager.UnlockedPartyMembers;
        HandlePlayerMenuRequest(playerParty);
    }
    private void Awake()
    {        
        _defaultFirst = eventSystem.firstSelectedGameObject;
    }


    public void HandlePlayerMenuRequest(List<PlayerPartyMemberConfig> playerParty)
    {
        ShowCharacters(playerParty);
        ShowMainOptions();
    }
    private void ShowCharacters(List<PlayerPartyMemberConfig> playerParty)
    {
        playerParty.ForEach(x => charactersView.GetComponent<CharactersView>().InitializeCardFor(x));
        
    }

    public void ShowMainOptions()
    {
        mainOptions.gameObject.SetActive(true);
        itemOptions.gameObject.SetActive(false);
        equipmentOptions.gameObject.SetActive(false);
        abilitiesOptions.gameObject.SetActive(false);
        charactersView.SetActive(true);
        eventSystem.SetSelectedGameObject(_defaultFirst);
        HideBackButton();        
    }

    public void ShowAbilitiesOptions()
    {
        mainOptions.gameObject.SetActive(false);
        itemOptions.gameObject.SetActive(false);
        equipmentOptions.gameObject.SetActive(false);
        abilitiesOptions.gameObject.SetActive(true);
        charactersView.SetActive(true);
        eventSystem.SetSelectedGameObject(_defaultFirst);
        HideBackButton();
    }

    private void ShowBackButton()
    {
        backToMain.gameObject.SetActive(true);
        eventSystem.SetSelectedGameObject(backToMain.gameObject);
    }
    private void HideBackButton()
    {
        backToMain.gameObject.SetActive(false);
    }


    public void ShowItems()
    {
        abilitiesOptions.gameObject.SetActive(false);
        mainOptions.gameObject.SetActive(false);
        itemOptions.gameObject.SetActive(true);   
        ShowBackButton();
    }

    public void ShowEquipment()
    {
        mainOptions.gameObject.SetActive(false);
        abilitiesOptions.gameObject.SetActive(false);
        charactersView.gameObject.SetActive(false);
        equipmentOptions.gameObject.SetActive(true);
        ShowBackButton();
    }

      
}
