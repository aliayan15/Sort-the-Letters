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
        
        [Header("Level")]
        [SerializeField] private TextMeshProUGUI level;

        private void Start()
        {
            
        }
        private void StartLevel()
        {
            var level = GameManager.Instance.LevelManager.GetGameLevel();
            this.level.text = "LEVEL " + level;
        }

        
        private void OnGameStateChange(GameStates state)
        {
            if (state == GameStates.GAME)
                StartLevel();
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
