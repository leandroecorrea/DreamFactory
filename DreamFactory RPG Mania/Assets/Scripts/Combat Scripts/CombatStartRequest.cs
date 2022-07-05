using System.Collections.Generic;

public class CombatStartRequest
{
    public List<CombatEntityConfig> enemies;
    public List<CombatEntityConfig> allies;
    public string originScene;
    public int experienceReward;

    public CombatStartRequest(List<CombatEntityConfig> enemies, List<CombatEntityConfig> players, string originScene, int experienceReward)
    {
        this.enemies = enemies;
        this.allies = players;
        this.originScene = originScene;
        this.experienceReward = experienceReward;
    }
}
