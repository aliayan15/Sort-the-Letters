using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using DG.Tweening;
using TMPro;
using Players.Stores;

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
        [Header("Totarial")]
        [SerializeField] private WordLetters totarialWord;
        [SerializeField] private GameObject totarial;
        [SerializeField] private string totarialCorrectWord;
        [SerializeField] private string totarialWrongWord;
        [SerializeField] private GameObject[] hands;


        private List<string> _randomCurrectWords = new List<string>();
        private List<string> _wrongWords = new List<string>();



        public void MouseDown()
        {
            for (int i = 0; i < words.Length; i++)
            {
                words[i].CheckMouseDown();
            }
            if (totarial.activeInHierarchy)
                totarialWord.CheckMouseDown();
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
                words[i].SetLetters(_wrongWords[i], _randomCurrectWords[i]);
            }
            // totarial yapildi
            if (!PlayerLevelStore.IsTotarialDone())
            {
                SetTotarial();
            }
            else
            {
                totarial.SetActive(false);
                words[0].gameObject.SetActive(true);
            }
        }

        private void SetTotarial()
        {
            totarialWord.SetLetters(totarialWrongWord, totarialCorrectWord);
            totarial.SetActive(true);
            words[0].gameObject.SetActive(false);
        }
        #endregion

        public void SetTotarialEnd()
        {
            totarial.GetComponent<Image>().enabled = false;
            for (int i = 0; i < hands.Length; i++)
            {
                hands[i].SetActive(false);
            }
        }

        private void DisableWordAnimators()
        {
            for (int i = 0; i < words.Length; i++)
            {
                words[i].DisableAllAnimators();
            }
        }

        private void StartLevel()
        {
            var level = GameManager.Instance.LevelManager.GetGameLevel();
            this.level.text = "LEVEL " + level;
            // Set Words
            DisableWordAnimators();
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
