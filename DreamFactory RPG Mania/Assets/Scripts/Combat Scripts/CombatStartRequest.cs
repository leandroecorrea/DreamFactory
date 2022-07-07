using System.Collections.Generic;

[System.Serializable]
public class CombatStartRequest
{
    public List<CombatEntityConfig> enemies;
    public List<CombatEntityConfig> allies;
    public string originScene;
    public int experienceReward;
    public EncounterHistory.Encounters encounter;


    public CombatStartRequest(List<CombatEntityConfig> enemies, List<CombatEntityConfig> players, string originScene)
    {
        this.enemies = enemies;
        this.allies = players;
        this.originScene = originScene;
    }

    public CombatStartRequest(List<CombatEntityConfig> enemies, List<CombatEntityConfig> allies, string originScene, int experienceReward, EncounterHistory.Encounters encounter)
    {
        this.experienceReward = experienceReward;
        this.encounter = encounter;
        this.enemies = enemies;
        this.allies = allies;
        this.originScene = originScene;
    }
}
