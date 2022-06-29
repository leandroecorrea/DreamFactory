using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PlayerProgression
{
    private const string BASE_SAVE_FILE_NAME = "DreamFactory_Save";
    private const string BASE_SAVE_FILE_EXTENSION = ".save";

    private static Dictionary<string, object> loadedPlayerData;
    private static int currentSaveIndex = -1; // -1 indicates no saves have been loaded

    public static T GetPlayerData<T>(string dataKey)
    {
        // This indicates a save hasn't been loaded, load the default save
        if (currentSaveIndex == -1)
        {
            loadedPlayerData = new Dictionary<string, object>(NewSaveGenerator.newSaveDefaultInfo);
        }

        if (loadedPlayerData != null && loadedPlayerData.TryGetValue(dataKey, out object loadedPlayerDataValue))
        {
            return (T)loadedPlayerDataValue;
        }

        return default;
    }

    public static void UpdatePlayerData<T>(string dataKey, T dataValue)
    {
        // This indicates a save hasn't been loaded, load the default save
        if (currentSaveIndex == -1)
        {
            loadedPlayerData = new Dictionary<string, object>(NewSaveGenerator.newSaveDefaultInfo);
        }

        if (loadedPlayerData == null)
        {
            loadedPlayerData = new Dictionary<string, object>();
            loadedPlayerData.Add(dataKey, dataValue);

            return;
        }

        loadedPlayerData[dataKey] = dataValue;
        return;
    }

    public static void RemovePlayerData(string dataKey)
    {
        // This indicates a save hasn't been loaded, load the default save
        if (currentSaveIndex == -1)
        {
            loadedPlayerData = new Dictionary<string, object>(NewSaveGenerator.newSaveDefaultInfo);
        }

        if (loadedPlayerData != null && loadedPlayerData.ContainsKey(dataKey))
        {
            loadedPlayerData.Remove(dataKey);
        }
    }

    private static Dictionary<string, object> ReadPlayerSaveData(int saveIndex)
    {
        string saveFilePath = Application.persistentDataPath;
        string targetSaveFileName = $"{BASE_SAVE_FILE_NAME}{saveIndex}{BASE_SAVE_FILE_EXTENSION}";
        string totalTargetDataPath = $"{saveFilePath}/{targetSaveFileName}";

        Dictionary<string, object> gameData = null;
        if (File.Exists(totalTargetDataPath))
        {
            BinaryFormatter converter = new BinaryFormatter();
            FileStream inputStream = new FileStream(totalTargetDataPath, FileMode.Open);

            gameData = converter.Deserialize(inputStream) as Dictionary<string, object>;
            inputStream.Close();
        }

        return gameData;
    }

    public static PlayerSaveDisplay GetPlayerSaveDisplay(int saveIndex)
    {
        return new PlayerSaveDisplay(
            ReadPlayerSaveData(saveIndex), 
            saveIndex
        );
    }

    public static void SaveLoadedData()
    {
        if (loadedPlayerData == null)
        {
            Debug.LogError("Trying to save without loading data");
            return;
        }

        string saveFilePath = Application.persistentDataPath;
        string targetSaveFileName = $"{BASE_SAVE_FILE_NAME}{currentSaveIndex}{BASE_SAVE_FILE_EXTENSION}";
        string totalTargetDataPath = $"{saveFilePath}/{targetSaveFileName}";

        BinaryFormatter converter = new BinaryFormatter();
        FileStream inputStream = new FileStream(totalTargetDataPath, FileMode.Create);

        converter.Serialize(inputStream, loadedPlayerData);
        inputStream.Close();
    }

    public static void LoadPlayerSave(int saveIndex)
    {
        Dictionary<string, object> playerDataToLoad = ReadPlayerSaveData(saveIndex);
        if (playerDataToLoad == null)
        {
            playerDataToLoad = new Dictionary<string, object>(NewSaveGenerator.newSaveDefaultInfo);
        }

        currentSaveIndex = saveIndex;
        loadedPlayerData = playerDataToLoad;
    }
}

public class PlayerSaveDisplay
{
    private static List<PlayerPartyMemberConfig> allPartyMemberConfigs;

    public string fileName;
    public bool isNewSave;

    public List<PlayerPartyMemberConfig> unlockedPartyMembers;

    public PlayerSaveDisplay(Dictionary<string, object> rawPlayerSaveData, int saveIndex)
    {
        fileName = $"Save {saveIndex + 1}";
        isNewSave = (rawPlayerSaveData == null);

        if (!isNewSave)
        {
            if (rawPlayerSaveData.TryGetValue(SaveKeys.UNLOCKED_PARTY_MEMBERS, out object partyMemberIds))
            {
                List<string> partyMemberIdList = partyMemberIds as List<string>;
                if (partyMemberIdList != null)
                {
                    LocateUnlockedPartyMemberConfigs(partyMemberIdList);
                }
            }
            
        }
    }

    private void LocateUnlockedPartyMemberConfigs(List<string> unlockedPartyMemberIds)
    {
        if (allPartyMemberConfigs == null)
        {
            allPartyMemberConfigs = PlayerPartyManager.LoadAllAvailablePartyMemberConfigs().ToList();
        }

        unlockedPartyMembers = new List<PlayerPartyMemberConfig>();
        foreach (PlayerPartyMemberConfig availableConfig in allPartyMemberConfigs)
        {
            if (unlockedPartyMemberIds.Contains(availableConfig.partyMemberId))
            {
                unlockedPartyMembers.Add(availableConfig);
            }
        }
    }
}