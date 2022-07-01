using UnityEngine;

[CreateAssetMenu(fileName = "New Leveling stats", menuName = "Player Party/Stats/New Leveling Stats")]
public class LevelingStats : ScriptableObject
{
    public int Level;
    public int experience;
    public int experienceUntilNextLevel;
}