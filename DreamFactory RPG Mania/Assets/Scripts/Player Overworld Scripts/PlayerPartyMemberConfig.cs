using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Party Member", menuName = "Player Party/New Party Member")]
public class PlayerPartyMemberConfig : ScriptableObject
{
    public string partyMemberId;
    public CombatEntityConfig combatEntityConfig;

    public int baseLevel;
    public Sprite partyMemberPortrait;
}
