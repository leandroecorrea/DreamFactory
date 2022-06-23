using System.Collections.Generic;

public interface ITargetStrategy
{
    List<CombatEntity> GetTargets(CombatContext context);
}