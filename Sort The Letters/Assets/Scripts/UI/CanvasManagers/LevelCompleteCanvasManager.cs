using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Managers;

namespace UI.CanvasManagers
{
    public class LevelCompleteCanvasManager : MonoBehaviour
    {
        // level complete olduðunda görseller animasyon ile gelicek

        Ease easeType = Ease.OutBack;
        [SerializeField] private RectTransform nextLevel, levelCompleteText;
        [SerializeField] private ParticleSystem conffety;

        Sequence levelComplete;

        private void Start()
        {
            StartUI();
        }

        private void StartUI()
        {
            nextLevel.DOScale(0, 0);
            levelCompleteText.DOScale(0, 0);
        }

        private void FadeOut()
        {
            //conffety.Play();
            //levelComplete = DOTween.Sequence();

            //levelComplete.Append(levelCompleteText.DOScale(1, 0.5f).SetEase(easeType))
            //.Append(nextLevel.DOScale(1, 0.5f).SetEase(easeType));
        }

        private void OnGameStateChange(GameStates state)
        {
            if (state == GameStates.LEVELCOMPLETE)
                FadeOut();
            if (state == GameStates.MENU)
                StartUI();
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
