using MoreMountains.NiceVibrations;
using Players.Stores;
using UnityEngine;

namespace Managers
{
    public class SettingManager : MonoBehaviour
    {
        private bool _isVibrationEnabled = true;


        private void Start()
        {
            _isVibrationEnabled = PlayerSettingStore.IsVibrationEnabled();
            SetSoundSetting();
        }

        public void VibrateLight(HapticTypes hapticTypes = HapticTypes.LightImpact)
        {
            if (!_isVibrationEnabled)
            {
                return;
            }

            MMVibrationManager.Haptic(hapticTypes);
        }

        public void VibrateTransient()
        {
            if (!_isVibrationEnabled)
            {
                return;
            }

            MMVibrationManager.TransientHaptic(1f, 1f);
        }

        private void OnEnable()
        {
            PlayerSettingStore.OnSettingChange += OnSettingChanged;
        }

        private void OnDisable()
        {
            PlayerSettingStore.OnSettingChange -= OnSettingChanged;
        }

        private void OnSettingChanged(SettingType settingType)
        {
            switch (settingType)
            {
                case SettingType.IsVibrationEnabled:
                    _isVibrationEnabled = PlayerSettingStore.IsVibrationEnabled();
                    break;
                case SettingType.IsSoundEnabled:
                    SetSoundSetting();
                    break;
                default:
                    break;
            }
        }

        private static void SetSoundSetting()
        {
            var isSoundEnabled = PlayerSettingStore.IsSoundEnabled();
            AudioListener.pause = !isSoundEnabled;
        }
    }
}
