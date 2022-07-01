using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPartyManager
{
    private static List<PlayerPartyMemberConfig> _unlockedPartyMembers;
    private static List<string> _unlockedPartyMemberIds;

    public static List<PlayerPartyMemberConfig> UnlockedPartyMembers
    {
        get
        {
            if (_unlockedPartyMembers == null)
            {
                RetrievePartyMembersFromSave();
            }

            return _unlockedPartyMembers;
        }
    }

    public static List<string> UnlockedPartyMemberIds
    {
        get
        {
            if (_unlockedPartyMemberIds == null)
            {
                RetrievePartyMembersFromSave();
            }

            return _unlockedPartyMemberIds;
        }
    }

    public static PlayerPartyMemberConfig[] LoadAllAvailablePartyMemberConfigs()
    {
        return Resources.LoadAll<PlayerPartyMemberConfig>("Party Member Configs");
    }

    private static void RetrievePartyMembersFromSave()
    {
        PlayerPartyMemberConfig[] allPartyMemberConfigs = LoadAllAvailablePartyMemberConfigs();

        _unlockedPartyMemberIds = PlayerProgression.GetPlayerData<List<string>>(SaveKeys.UNLOCKED_PARTY_MEMBERS);
        Debug.Log($"Unlocked Party Member Id Count: {_unlockedPartyMemberIds.Count}");

        _unlockedPartyMembers = new List<PlayerPartyMemberConfig>();

        foreach (PlayerPartyMemberConfig partyMemberConfig in allPartyMemberConfigs)
        {
            if (_unlockedPartyMemberIds.Contains(partyMemberConfig.partyMemberId))
            {
                _unlockedPartyMembers.Add(partyMemberConfig);
            }
        }
    }

    public static void UnlockPartyMemberById(PlayerPartyMemberConfig newPartyMemberConfig)
    {
        Debug.Log($"Attempting to unlock {newPartyMemberConfig.name}");

        if (UnlockedPartyMemberIds.Contains(newPartyMemberConfig.partyMemberId))
        {
            Debug.LogError($"Trying to add {newPartyMemberConfig.name} to party when they're already added");
            return;
        }

        UnlockedPartyMemberIds.Add(newPartyMemberConfig.partyMemberId);
        UnlockedPartyMembers.Add(newPartyMemberConfig);

        PlayerProgression.UpdatePlayerData(SaveKeys.UNLOCKED_PARTY_MEMBERS, UnlockedPartyMemberIds);
    }

    public static List<CombatEntityConfig> GetAllUnlockedCombatConfigs()
    {
        List<CombatEntityConfig> combatEntityConfigs = new List<CombatEntityConfig>();

        foreach(PlayerPartyMemberConfig partyMemberConfig in UnlockedPartyMembers)
        {
            //combatEntityConfigs.Add(partyMemberConfig.characterCombatConfig.combatEntityConfig);
        }

        return combatEntityConfigs;
    }
}
