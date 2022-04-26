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
        [Header("Level")]
        [SerializeField] private TextMeshProUGUI level;
        [Space(5)]
        [Header("Words")]
        [SerializeField] private WordLetters[] words;


        private List<string> _randomCurrectWords = new List<string>();
        private List<string> _wrongWords = new List<string>();

        private void Start()
        {

        }

        public void MouseDown()
        {
            for (int i = 0; i < words.Length; i++)
            {
                words[i].CheckMouseDown();
            }
        }

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

        private void StartLevel()
        {
            var level = GameManager.Instance.LevelManager.GetGameLevel();
            this.level.text = "LEVEL " + level;
            // Set Words
            SetWords();
            SetLetters();
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
