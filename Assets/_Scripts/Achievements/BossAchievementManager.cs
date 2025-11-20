public static class BossAchievementManager
{
    private static readonly string[] bosses = { "Boss1Defeated", "Boss2Defeated", "Boss3Defeated" };
    public static void UnlockBoss(int index)
    {
        AchievementManager.Instance.Unlock(bosses[index]);
    }

    public static void UnlockAll()
    {
        for (int i = 0; i < bosses.Length; i++)
        {
            AchievementManager.Instance.Unlock(bosses[i]);
        }
    }

}
