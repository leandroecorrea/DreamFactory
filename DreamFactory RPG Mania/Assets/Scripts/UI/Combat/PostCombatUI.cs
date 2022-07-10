using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PostCombatUI : MonoBehaviour
{
    [SerializeField] private GameObject[] characterReward;
    [SerializeField] private TMP_Text informationText;
    private List<PlayerPartyMemberConfig> players;
    private Dictionary<string, LevelingStats> previousStats;

    private void OnEnable()
    {
        informationText.text = "Combat finished!";
        players = PlayerPartyManager.UnlockedPartyMembers;
        StorePreviousStats();
        for (int i = 0; i < players.Count; i++)
        {
            characterReward[i].gameObject.SetActive(true);
            var component = characterReward[i].GetComponentInChildren<CombatRewardView>();
            component.SetCharacterRewardView(players[i]);
        }
    }

    private void StorePreviousStats()
    {
        previousStats = new Dictionary<string, LevelingStats>();
        players.ForEach(x => previousStats.Add(x.partyMemberId,
        new LevelingStats
        {
            experience = x.Stats.experience,
            experienceUntilNextLevel = x.Stats.experienceUntilNextLevel,
            level = x.Stats.level
        }));
    }

    public void InitializePreviousSceneTransition()
    {
        UpdateStats();
        PostCombatTransitionManager.instance.InitializeReturnToPreviousScene();
    }

    private void UpdateStats()
    {
        players.ForEach(x =>
        {
            var stats = previousStats[x.partyMemberId];
            LevelingManager.UpdateStats(ref stats, CombatManager.currentStartRequest.experienceReward);
            x.Stats = stats;
        });
    }
}

