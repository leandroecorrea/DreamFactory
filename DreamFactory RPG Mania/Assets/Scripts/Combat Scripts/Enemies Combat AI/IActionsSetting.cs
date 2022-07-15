using System.Collections.Generic;

public interface IActionsSetting
{
    IContextSetting SetActions(List<CombatActionConfig> actions);
}
