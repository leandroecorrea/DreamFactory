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
        characterName.text = c.characterConfig.characterName;
        level.text = $"Level {c.levelingStats.Level}";
        HP.text = $"HP {c.characterCombatConfig.currentHP}/{c.characterCombatConfig.combatEntityConfig.baseHP}";
        MP.text = $"MP {c.characterCombatConfig.currentMP}/{c.characterCombatConfig.combatEntityConfig.baseMP}";
        characterSprite.sprite = c.characterConfig.portrait;
    }
}
