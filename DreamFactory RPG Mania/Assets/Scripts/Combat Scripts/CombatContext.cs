using System.Collections.Generic;

public class CombatContext
{
    public List<CombatEntity> playerParty;
    public List<CombatEntity> enemyParty;

    public CombatEntity currentTurnEntity;
}
