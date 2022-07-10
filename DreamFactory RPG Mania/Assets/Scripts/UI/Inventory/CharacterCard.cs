using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCard : MonoBehaviour
{
    public TMP_Text characterName;
    public TMP_Text level;
    public TMP_Text HP;
    public TMP_Text MP;
    public Image characterSprite;


    public void UpdateCard(PlayerPartyMemberConfig c)
    {
        characterName.text = c.CharacterDetails.characterName;
        level.text = $"Level {c.Stats.level}";
        HP.text = $"HP {c.PartyMemberCombatConfig.currentHP}/{c.PartyMemberCombatConfig.CombatEntityConfig.baseHP}";
        MP.text = $"MP {c.PartyMemberCombatConfig.currentMP}/{c.PartyMemberCombatConfig.CombatEntityConfig.baseMP}";
        characterSprite.sprite = c.CharacterDetails.characterPortrait;
    }
}
