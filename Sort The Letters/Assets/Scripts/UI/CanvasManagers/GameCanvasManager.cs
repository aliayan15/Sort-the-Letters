using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using DG.Tweening;
using TMPro;

namespace UI.CanvasManagers
{
    public class GameCanvasManager : MonoBehaviour
    {
        // Animasyon
        //Ease easeType = Ease.OutBack;
        [Header("Level")]
        [SerializeField] private TextMeshProUGUI level;
        [SerializeField] private RectTransform levelRec;

        private void Start()
        {
            
        }
        private void ShowLevel()
        {
            var level = GameManager.Instance.LevelManager.GetGameLevel();
            this.level.text = "LEVEL " + level;
        }

        private void OnGameStateChange(GameStates state)
        {
            if (state == GameStates.GAME)
                ShowLevel();
        }

        private void OnEnable()
        {
            GameManager.OnGameStateChange+= OnGameStateChange;
        }
        private void OnDisable()
        {
            GameManager.OnGameStateChange -= OnGameStateChange;
        }

    }
}
