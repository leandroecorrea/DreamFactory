using System.Collections.Generic;

[System.Serializable]
public class CombatStartRequest
{
    public List<CombatEntityConfig> enemies;
    public List<CombatEntityConfig> allies;
    public string originScene;


    public CombatStartRequest(List<CombatEntityConfig> enemies, List<CombatEntityConfig> players, string originScene)
    {
        this.enemies = enemies;
        this.allies = players;
        this.originScene = originScene;
    }
}
