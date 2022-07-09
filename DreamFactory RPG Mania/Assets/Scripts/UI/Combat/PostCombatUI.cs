using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PostCombatUI : MonoBehaviour
{
    [SerializeField] private GameObject[] characterReward;
    [SerializeField] private TMP_Text informationText;
    private List<PlayerPartyMemberConfig> players;

    private void OnEnable()
    {   
        players = PlayerPartyManager.UnlockedPartyMembers;
        informationText.text = "Combat finished!";
        for (int i = 0; i < players.Count; i++)
        {
            characterReward[i].gameObject.SetActive(true);
            var component = characterReward[i].GetComponentInChildren<CombatRewardView>();
            component.SetCharacterRewardView(players[i]);
        }
    }
    public void InitializePreviousSceneTransition()
    {
        // Save player state?
        PostCombatTransitionManager.instance.InitializeReturnToPreviousScene();
    }
}

