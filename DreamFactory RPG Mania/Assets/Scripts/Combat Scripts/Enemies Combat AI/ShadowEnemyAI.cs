using System.Collections.Generic;

public class ShadowEnemyAI : BaseEnemyAI
{
    protected override string[] ActionKeys => new[] { "Freeze", "Fever", "Physical Attack", "Panic"};    
    protected override CombatActionConfig ChooseAnAction()
    {
        var random = UnityEngine.Random.Range(0, 100);
        if (!IsAbleToPerformAllAbilities())
            return RandomAction();
        if (random < 45)
            return actions["Physical Attack"];
        else if (random < 75)
            return actions["Panic"];
        else if (random < 95)
            return actions["Fever"];
        else
            return actions["Freeze"];
    }
}


