using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Managers;

namespace UI.CanvasManagers
{
    public class GameOverCanvasManager : MonoBehaviour
    {
        Ease easeType = Ease.OutBack;
        [SerializeField] private RectTransform tryAgain, gameOverText;


        private void Start()
        {
            tryAgain.DOScale(0, 0);
            tryAgain.DOScale(0, 0);
        }
        private void FadeOut()
        {
            tryAgain.DOScale(1, 0.5f).SetEase(easeType);
            gameOverText.DOScale(1, 0.5f).SetEase(easeType);
        }

        private void OnGameStateChange(GameStates state)
        {
            if (state == GameStates.GAMEOVER)
                FadeOut();
        }

        private void OnEnable()
        {
            GameManager.OnGameStateChange += OnGameStateChange;
        }
        private void OnDisable()
        {
            GameManager.OnGameStateChange -= OnGameStateChange;
        }
    }
}
