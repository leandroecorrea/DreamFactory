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
    private int _currentExperience;
    private int _futureExperience;
    private int _currentToNextLevel;
    private int _futureToNextLevel;
    private LevelingStats _stats;

    public void SetCharacterRewardView(PlayerPartyMemberConfig characterConfig)
    {
        //_reward = CombatManager.currentStartRequest.experienceReward;
        _reward = 2000;
        _stats = characterConfig.Stats;
        characterName.text = characterConfig.CharacterDetails.characterName;
        level.text = "Level: " + characterConfig.Stats.level.ToString();
        _currentExperience = characterConfig.Stats.experience;
        _futureExperience = _currentExperience + _reward;
        _currentToNextLevel = characterConfig.Stats.experienceUntilNextLevel;
        _futureToNextLevel = _currentToNextLevel - _reward;
        //experienceUntilNextLevel.text = characterConfig.Stats.experienceUntilNextLevel.ToString();
        characterAsset.sprite = characterConfig.CharacterDetails.characterPortrait;
        StartCoroutine(UpdateExperience());
        StartCoroutine(UpdateToNextTurn());
    }

    private IEnumerator UpdateExperience()
    {        
        while(_currentExperience < _futureExperience)
        {
            totalExperience.text = Convert.ToInt32(_currentExperience).ToString();
            _currentExperience+=2;
            
            yield return null;
        }
        _stats.experience = _currentExperience;
    }
    private IEnumerator UpdateToNextTurn()
    {

        while(_currentToNextLevel > _futureToNextLevel)
        {
            experienceUntilNextLevel.text = Convert.ToInt32(_currentToNextLevel).ToString();
            _currentToNextLevel-=2;
            if(_currentToNextLevel < 0)
            {
                LevelingManager.LevelUp(ref _stats);
                level.text = "Level: "+ _stats.level.ToString();
                _currentToNextLevel = _stats.experienceUntilNextLevel;
                _futureToNextLevel = _currentToNextLevel + _futureToNextLevel;
            }
            yield return null;
        }
    }   
}
