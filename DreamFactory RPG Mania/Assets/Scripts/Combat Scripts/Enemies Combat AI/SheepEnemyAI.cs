using System.Collections.Generic;

public class SheepEnemyAI : BaseEnemyAI
{

    protected override string[] ActionKeys { get=> new[] {"Physical Attack", "Lucid", "Amnesia", "Recall"}; }    

    protected override CombatActionConfig ChooseAnAction()
    {
        var random = UnityEngine.Random.Range(0, 100);
        if (!IsAbleToPerformAllAbilities())
            return RandomAction();
        if (random < 75)
            return actions["Physical Attack"];
        else if (random < 85)
            return actions["Lucid"];
        else if (random < 95)
            return actions["Amnesia"];
        else
            return actions["Recall"];
    }
}


