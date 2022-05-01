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
        [SerializeField] private WordSelected wordSelected;
        [SerializeField] private Image levelProgress;
        [Header("Level")]
        [SerializeField] private TextMeshProUGUI level;
        [Space(5)]
        [Header("Words")]
        [SerializeField] private WordLetters[] words;


        private List<string> _randomCurrectWords = new List<string>();
        private List<string> _wrongWords = new List<string>();



        public void MouseDown()
        {
            for (int i = 0; i < words.Length; i++)
            {
                words[i].CheckMouseDown();
            }
        }

        public void SetLevelProgress(int skore)
        {
            var ratio = (float)skore / 7f;
            levelProgress.DOKill();
            levelProgress.DOFillAmount(ratio, 0.3f);
        }

        #region SetWords
        private void SetWords()
        {
            _randomCurrectWords = wordSelected.GetRandomWords();
            _wrongWords = wordSelected.GetWrongWords(_randomCurrectWords);
        }
        private void SetLetters()
        {
            for (int i = 0; i < words.Length; i++)
            {
                words[i].SetLetters(_wrongWords[i],_randomCurrectWords[i]);
            }
        }
        #endregion

        private void StartLevel()
        {
            var level = GameManager.Instance.LevelManager.GetGameLevel();
            this.level.text = "LEVEL " + level;
            // Set Words
            SetWords();
            SetLetters();
            levelProgress.fillAmount = 0;
        }


        private void OnGameStateChange(GameStates state)
        {
            if (state == GameStates.GAME)
                StartLevel();
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
