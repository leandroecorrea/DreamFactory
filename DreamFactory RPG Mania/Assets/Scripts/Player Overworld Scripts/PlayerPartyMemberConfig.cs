using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Party Member", menuName = "Player Party/New Party Member")]
public class PlayerPartyMemberConfig : ScriptableObject
{    
    public string partyMemberId;
    public PartyMemberCombatConfig PartyMemberCombatConfig;
    public CharacterDetails CharacterDetails;
    public LevelingStats Stats;    
}