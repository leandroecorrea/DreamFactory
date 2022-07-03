using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Inventory Item")]
public class ItemConfig : ScriptableObject
{
    public string id;
    public string itemName;
    public string description;
    public Sprite icon;    
    public CombatActionConfig actionConfig;
    //public GameObject prefab;    
}