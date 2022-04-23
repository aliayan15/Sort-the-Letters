using System;
using UnityEngine;

namespace Players.Stores
{
    public enum SettingType
    {
        IsSoundEnabled,
        IsVibrationEnabled
    }

    public class PlayerSettingStore
    {
        public static event Action<SettingType> OnSettingChange;

        public static bool IsSoundEnabled()
        {
            return PlayerPrefs.GetInt(SettingType.IsSoundEnabled.ToString(), 1) == 1;
        }

        public static void SetSoundEnabled(bool enabled)
        {
            PlayerPrefs.SetInt(SettingType.IsSoundEnabled.ToString(), enabled ? 1 : 0);
            OnSettingChange?.Invoke(SettingType.IsSoundEnabled);
        }

        public static bool IsVibrationEnabled()
        {
            return PlayerPrefs.GetInt(SettingType.IsVibrationEnabled.ToString(), 1) == 1;
        }

        public static void SetVibrationEnabled(bool enabled)
        {
            PlayerPrefs.SetInt(SettingType.IsVibrationEnabled.ToString(), enabled ? 1 : 0);
            OnSettingChange?.Invoke(SettingType.IsVibrationEnabled);
        }
    }
}