public class CombatActionRequest
{
    public CombatEntity CurrentEntity { get; set; }
    public CombatActionConfig ActionChosen { get; set; }
    public CombatEntity[] Targets { get; set; }
}
