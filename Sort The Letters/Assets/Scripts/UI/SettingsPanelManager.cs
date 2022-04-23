using Players.Stores;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsPanelManager : MonoBehaviour
    {
        [Header("Sound")] [SerializeField] private Button soundBtn;
        [SerializeField] private Image soundImage;
        [SerializeField] private Sprite soundEnabledIcon;
        [SerializeField] private Sprite soundDisabledIcon;

        [Header("Vibration")] [SerializeField] private Button vibrationBtn;
        [SerializeField] private Image vibrationImage;
        [SerializeField] private Sprite vibrationEnabledIcon;
        [SerializeField] private Sprite vibrationDisabledIcon;

        private void Start()
        {
            soundBtn.onClick.AddListener(ToggleSound);
            vibrationBtn.onClick.AddListener(ToggleVibration);
        }

        public void ToggleSound()
        {
            PlayerSettingStore.SetSoundEnabled(!PlayerSettingStore.IsSoundEnabled());
            SetIcons();
        }

        public void ToggleVibration()
        {
            PlayerSettingStore.SetVibrationEnabled(!PlayerSettingStore.IsVibrationEnabled());
            SetIcons();
        }

        private void SetIcons()
        {
            if (PlayerSettingStore.IsSoundEnabled())
            {
                soundImage.sprite = soundEnabledIcon;
            }
            else
            {
                soundImage.sprite = soundDisabledIcon;
            }

            if (PlayerSettingStore.IsVibrationEnabled())
            {
                vibrationImage.sprite = vibrationEnabledIcon;
            }
            else
            {
                vibrationImage.sprite = vibrationDisabledIcon;
            }
        }

        private void OnEnable()
        {
            SetIcons();
        }
    }
}