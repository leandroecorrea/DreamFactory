using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCard : MonoBehaviour
{
    public Text characterName;
    public Text level;
    public Text HP;
    public Text MP;

    public void UpdateCard(Character c)
    {
        characterName.text = c.Name;
        level.text = $"Level {c.Level}";
        HP.text = $"{c.CurrentHP}/{c.MaxHP}";
        MP.text = $"{c.CurrentMP}/{c.MaxMP}";
    }
}
