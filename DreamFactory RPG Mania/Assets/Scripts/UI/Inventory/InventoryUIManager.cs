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
    [SerializeField] private GameObject statsView;
    [SerializeField] private GameObject abilitiesOptions;
    [SerializeField] private Button backToMain;
    [SerializeField] private EventSystem eventSystem;       
    private PlayerPartyMemberConfig _selectedEntity;
    private GameObject _defaultFirst;
    private List<PlayerPartyMemberConfig> _playerParty;

    void OnEnable()
    {
        _playerParty = PlayerPartyManager.UnlockedPartyMembers;
        HandlePlayerMenuRequest(_playerParty);
        _selectedEntity = _playerParty[0];
    }

    private void SetPartyMember(PlayerPartyMemberConfig playerPartyMemberConfig)
    {
        _selectedEntity = playerPartyMemberConfig;
        Debug.Log(_selectedEntity.partyMemberId);
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
        var charactersViewComponent = charactersView.GetComponent<CharactersView>();
        charactersViewComponent.DelegateBehaviour = SetPartyMember;
        playerParty.ForEach(x => charactersViewComponent.InitializeCardFor(x));        
    }

    public void ShowMainOptions()
    {
        mainOptions.gameObject.SetActive(true);        
        statsView.gameObject.SetActive(false);
        abilitiesOptions.gameObject.SetActive(false);
        charactersView.SetActive(true);
        eventSystem.SetSelectedGameObject(_defaultFirst);
        HideBackButton();        
    }

    public void ShowAbilitiesOptions()
    {
        mainOptions.gameObject.SetActive(false);        
        statsView.gameObject.SetActive(false);
        abilitiesOptions.gameObject.SetActive(true);
        charactersView.SetActive(true);
        ShowBackButton();
        eventSystem.SetSelectedGameObject(_defaultFirst);        
        Debug.Log(abilitiesOptions.GetComponent<DetailsView>());
        abilitiesOptions.GetComponent<DetailsView>().InitializeAbilities(_selectedEntity);       
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
        var items = InventoryManager.GetAll();
        mainOptions.gameObject.SetActive(false);        
        statsView.gameObject.SetActive(false);
        abilitiesOptions.gameObject.SetActive(true);
        charactersView.SetActive(true);
        ShowBackButton();
        eventSystem.SetSelectedGameObject(_defaultFirst);
        Debug.Log(abilitiesOptions.GetComponent<DetailsView>());
        abilitiesOptions.GetComponent<DetailsView>().InitializeItems(items);        
    }

    public void ShowStats()
    {
        statsView.GetComponent<StatusView>().SetStats(_selectedEntity);
        mainOptions.gameObject.SetActive(false);
        abilitiesOptions.gameObject.SetActive(false);
        charactersView.gameObject.SetActive(false);
        statsView.gameObject.SetActive(true);
        ShowBackButton();
    }      
}
