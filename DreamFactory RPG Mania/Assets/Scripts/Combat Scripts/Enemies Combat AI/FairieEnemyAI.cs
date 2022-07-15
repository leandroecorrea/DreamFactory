using System.Collections.Generic;

public class FairieEnemyAI : BaseEnemyAI
{
    protected override string[] ActionKeys => new[] {"Anxiety", "Delirium", "Physical Attack", "REM", "Soothe"};        

    protected override CombatActionConfig ChooseAnAction()
    {        
        var random = UnityEngine.Random.Range(0, 100);
        if (!IsAbleToPerformAllAbilities())
            return RandomAction();
        if (random < 55)
            return actions["Physical Attack"];
        else if (random < 75)
            return actions["Anxiety"];
        else if (random < 85)
            return actions["Delirium"];
        else if (random < 95)
            return actions["REM"];
        else
            return actions["Soothe"];
    }
}


