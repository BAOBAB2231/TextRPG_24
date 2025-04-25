namespace SpartaDungeon
{
    public static class LevelExpCalculator
    {
        public static int GetNextLevelExp(int currentLevel)
        {
            return 40 + (currentLevel - 1) * 40;
        }
    }
}
