using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PostCombatUI : MonoBehaviour
{
    [SerializeField] private GameObject[] characterReward;
    [SerializeField] private TMP_Text informationText;
    

    private void OnEnable()
    {   
        var players = PlayerPartyManager.LoadAllAvailablePartyMemberConfigs();
        informationText.text = "Combat finished!";
        for (int i = 0; i < players.Length; i++)
        {
            characterReward[i].gameObject.SetActive(true);
            var component = characterReward[i].GetComponentInChildren<CombatRewardView>();
            component.SetCharacterRewardView(players[i]);
        }
    }
    public void InitializePreviousSceneTransition()
    {
        PostCombatTransitionManager.instance.InitializeBackToPreviousScene();
    }
}
