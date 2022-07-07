using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatRewardView : MonoBehaviour
{
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text level;
    [SerializeField] private TMP_Text totalExperience;
    [SerializeField] private TMP_Text experienceUntilNextLevel;
    [SerializeField] private Image characterAsset;
    private int _reward;

    public void SetCharacterRewardView(PlayerPartyMemberConfig characterConfig)
    {
        //_reward = CombatManager.currentStartRequest.experienceReward;
        _reward = 100;
        characterName.text = characterConfig.CharacterDetails.characterName;
        level.text = "Level: " + characterConfig.Stats.level.ToString();
        TweenExperience(characterConfig);

        experienceUntilNextLevel.text = characterConfig.Stats.experienceUntilNextLevel.ToString();
        characterAsset.sprite = characterConfig.CharacterDetails.characterPortrait;
    }

    private void TweenExperience(PlayerPartyMemberConfig characterConfig)
    {        
        var currentExperience = characterConfig.Stats.experience;
        var futureExperience = currentExperience + _reward;
        var currentToNextLevel = characterConfig.Stats.experienceUntilNextLevel;
        var futureToNextLevel = characterConfig.Stats.experienceUntilNextLevel - _reward;
        LeanTween.value(gameObject, currentExperience, futureExperience, 5f)            
            .setOnUpdate(SetExperienceText);
        LeanTween.value(gameObject, currentToNextLevel, futureToNextLevel, 5f)
            .setOnUpdate(SetExperienceToNextLevel);
    }

    private void SetExperienceToNextLevel(float value)
    {
        experienceUntilNextLevel.text = "XP to next level: "+ Convert.ToInt32(value).ToString();
    }

    private void SetExperienceText(float value)
    {
        totalExperience.text = "Current XP: "+ Convert.ToInt32(value).ToString();
    }
}
