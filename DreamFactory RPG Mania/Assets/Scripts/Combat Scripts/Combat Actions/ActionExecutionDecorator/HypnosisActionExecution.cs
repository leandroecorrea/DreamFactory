using UnityEngine;

public class HypnosisActionExecution : ActionExecution
{

    private readonly BaseActionExecution baseActionExecution;

    public HypnosisActionExecution(BaseActionExecution baseActionExecution)
    {
        this.baseActionExecution = baseActionExecution;
    }

    public override void Execute(CombatActionRequest request)
    {
        var hypnosisStrategy = ScriptableObject.CreateInstance<AlliesTargetStrategyConfig>();
        var entityType = typeof(CombatEntity);
        var contextType = entityType.GetField("currentTurnCtx", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var combatContext = (CombatContext)contextType.GetValue(request.CurrentEntity);
        var targets = hypnosisStrategy.GetTargets(combatContext);
        request.Targets = new CombatEntity[] { targets[0] };
        baseActionExecution.Execute(request);
    }
}