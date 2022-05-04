using UnityEngine;

namespace Players.Stores
{
    public class PlayerLevelStore
    {
        private const string CurrentLevelKey = "CurrentLevelInt";
        private const string TotarialKey = "TotarialKey";

        public static int GetCurrentLevel()
        {
            return PlayerPrefs.GetInt(CurrentLevelKey, 1);
        }

        public static void SetNextLevel()
        {
            var currentLevel = GetCurrentLevel();
            PlayerPrefs.SetInt(CurrentLevelKey, currentLevel + 1);
        }
        
        public static bool IsTotarialDone()
        {
            if (PlayerPrefs.HasKey(TotarialKey))
            {
                return true;
            }
            else
            {
                PlayerPrefs.SetInt(TotarialKey, 5);
                return false;
            }
        }
    }
}