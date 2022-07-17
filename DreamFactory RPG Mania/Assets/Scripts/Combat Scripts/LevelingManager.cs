using System.Collections.Generic;

public static class LevelingManager
{
    private static readonly Dictionary<int, int> experiencePerLevel = new Dictionary<int, int>
    {
        { 1, 100 },
        { 2, 200 },
        { 3, 345 },
        { 4, 500 },
        { 5, 600 },
        { 6, 850 },
        { 7, 1000 },
        { 8, 1500 },
        { 9, 2000 },
        { 10, 3000 },
        { 11, 4000 },
        { 12, 4500 },
        { 13, 5000 },
        { 14, 5000 },
        { 15, 5000 },
        { 16, 5000 },
        { 17, 5000 },
        { 18, 5000 },
        { 19, 5000 },
        { 20, 5000 },
        { 21, 5000 },
        { 22, 5000 }
    };

    public static void LevelUp(ref LevelingStats stats)
    {
        stats.level++;
        stats.experienceUntilNextLevel = experiencePerLevel[stats.level];
    }
    public static void UpdateStats(ref LevelingStats stats, int experienceReward)
    {
        while (experienceReward > 0)
        {
            stats.experience += experienceReward;
            stats.experienceUntilNextLevel -= experienceReward;
            experienceReward -= experienceReward;
            if (stats.experienceUntilNextLevel < 0)
            {
                experienceReward = stats.experienceUntilNextLevel * -1;
                LevelUp(ref stats);
            }
        }
    }
}