using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Party Member", menuName = "Player Party/New Party Member")]
public class PlayerPartyMemberConfig : ScriptableObject
{
    [Header("Party member details")]
    public string partyMemberId;
    public CharacterDetails characterConfig;
    [Header("Party member combat stats")]
    public PartyMemberCombatConfig characterCombatConfig;            
    public LevelingStats levelingStats;
    public CombatActionConfig actions { get; set; }        
}