using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject charactersView;
    [SerializeField] private GameObject mainOptions;
    [SerializeField] private GameObject itemOptions;
    [SerializeField] private GameObject equipmentOptions;
    [SerializeField] private Button backToMain;
    [SerializeField] private EventSystem eventSystem;
    private GameObject _defaultFirst;

    void OnEnable()
    {
        ShowCharacters();
        ShowMainOptions();
    }
    private void Awake()
    {
        _defaultFirst = eventSystem.firstSelectedGameObject;        
    }

    public void ShowMainOptions()
    {
        mainOptions.gameObject.SetActive(true);
        itemOptions.gameObject.SetActive(false);
        equipmentOptions.gameObject.SetActive(false);
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
        mainOptions.gameObject.SetActive(false);
        itemOptions.gameObject.SetActive(true);   
        ShowBackButton();
    }

    public void ShowEquipment()
    {
        mainOptions.gameObject.SetActive(false);
        charactersView.gameObject.SetActive(false);
        equipmentOptions.gameObject.SetActive(true);
        ShowBackButton();
    }

    private void ShowCharacters()
    {
        Character c = new Character();
        c.Name = "Zidane Tribal";
        c.Level = 10;
        c.MaxHP = 100;
        c.CurrentHP = 90;
        c.MaxMP = 25;
        c.CurrentMP = 25;
        charactersView.GetComponent<CharactersView>().InitializeCardFor(c);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
