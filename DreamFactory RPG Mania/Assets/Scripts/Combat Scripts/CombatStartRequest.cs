using System.Collections.Generic;

[System.Serializable]
public class CombatStartRequest
{
    public List<CombatEntityConfig> enemies;
    public List<CombatEntityConfig> allies;

    public string originScene;
    private string targetReloadScene; // Scene to Transition to after Combat Win

    public int experienceReward;
    public EncounterHistory.Encounters encounter;


    public CombatStartRequest(List<CombatEntityConfig> enemies, List<CombatEntityConfig> players, string originScene)
    {
        this.enemies = enemies;
        this.allies = players;

        this.originScene = originScene;
        targetReloadScene = originScene;
    }

    public CombatStartRequest(List<CombatEntityConfig> enemies, List<CombatEntityConfig> allies, string originScene, int experienceReward, EncounterHistory.Encounters encounter)
    {
        this.experienceReward = experienceReward;
        this.encounter = encounter;
        this.enemies = enemies;
        this.allies = allies;

        this.originScene = originScene;
        targetReloadScene = originScene;
    }

    public void OverrideReturnScene(string newTargetReloadScene)
    {
        targetReloadScene = newTargetReloadScene;
    }

    public string GetSceneNameToReload()
    {
        return targetReloadScene;
    }
}
