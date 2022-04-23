using UnityEngine;

namespace Players.Stores
{
    public class PlayerLevelStore
    {
        private const string CurrentLevelKey = "CurrentLevelInt";

        public static int GetCurrentLevel()
        {
            return PlayerPrefs.GetInt(CurrentLevelKey, 1);
        }

        public static void SetNextLevel()
        {
            var currentLevel = GetCurrentLevel();
            PlayerPrefs.SetInt(CurrentLevelKey, currentLevel + 1);
        } 
    }
}