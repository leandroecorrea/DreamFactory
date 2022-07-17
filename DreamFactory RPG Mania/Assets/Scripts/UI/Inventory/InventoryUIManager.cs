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
    [SerializeField] private GameObject abilitiesOptions;
    [SerializeField] private Button backToMain;    
    [SerializeField] private EventSystem eventSystem;
    private CharacterCard _currentCard;
    private PlayerPartyMemberConfig _selectedEntity;
    private GameObject _defaultFirst;
    private List<PlayerPartyMemberConfig> _playerParty;
    

    void OnEnable()
    {
        _playerParty = PlayerPartyManager.UnlockedPartyMembers;
        HandlePlayerMenuRequest(_playerParty);
        _selectedEntity = _playerParty[0];
    }

    private void SetPartyMember(PlayerPartyMemberConfig playerPartyMemberConfig, CharacterCard currentCard)
    {
        _currentCard = currentCard;
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
        charactersViewComponent.CharacterUpdateDelegate = SetPartyMember;
        playerParty.ForEach(x => charactersViewComponent.InitializeCardFor(x));        
    }

    public void ShowMainOptions()
    {
        mainOptions.gameObject.SetActive(true);                
        abilitiesOptions.gameObject.SetActive(false);
        charactersView.SetActive(true);
        eventSystem.SetSelectedGameObject(_defaultFirst);
        HideBackButton();        
    }

    public void ShowAbilitiesOptions()
    {
        mainOptions.gameObject.SetActive(false);                
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


    private void UseItem(Item item)
    {
        ItemOverworldHandler itemHandler = null;
        if (item.data.actionConfig != null)
        {
            string typeName = item.data.actionConfig.actionHandlerClassName.Replace("Item", "Overworld");
            Type handlerType = Type.GetType(typeName);
            itemHandler = (ItemOverworldHandler)Activator.CreateInstance(handlerType, item);
            itemHandler?.Handle(_selectedEntity);
            _currentCard?.UpdateCard(_selectedEntity);
        }
        else if(item.data.id == "SLEEPCAPSULE")
        {            
            Type handlerType = Type.GetType("SleepCapsuleOverworldHandler");
            itemHandler = (ItemOverworldHandler)Activator.CreateInstance(handlerType, item);
            itemHandler?.Handle(_selectedEntity);
            _currentCard?.UpdateCard(_selectedEntity);
        }        
    }

    public void ShowItems()
    {
        var items = InventoryManager.GetAll();
        mainOptions.gameObject.SetActive(false);                
        abilitiesOptions.gameObject.SetActive(true);
        charactersView.SetActive(true);
        ShowBackButton();
        eventSystem.SetSelectedGameObject(_defaultFirst);
        Debug.Log(abilitiesOptions.GetComponent<DetailsView>());
        abilitiesOptions.GetComponent<DetailsView>().InitializeItems(items, UseItem);        
    }   
}
