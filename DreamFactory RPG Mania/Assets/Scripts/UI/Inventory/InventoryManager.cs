using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject charactersView;
    [SerializeField] private GameObject mainOptions;
    [SerializeField] private GameObject itemOptions;
    [SerializeField] private GameObject equipmentOptions;

    void Start()
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
