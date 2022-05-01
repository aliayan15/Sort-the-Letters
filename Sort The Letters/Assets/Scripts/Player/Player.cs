using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

namespace Players
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int winCount;
        private int _skore;
        private int _totalCount;
        public int Skore { get { return _skore; } set { _skore = value; UpdateSkore(); } }
        public int TotalChoceCount { get { return _totalCount; } set { _totalCount = value; CheckLevelEnd(); } }

        private void Start()
        {
            GameManager.Instance.player = this;
        }

        private void UpdateSkore()
        {
            GameManager.Instance.uiManager.gameCanvasManager.SetLevelProgress(_skore);
        }

        private void CheckLevelEnd()
        {
            if (_totalCount < 7) return;
            Invoke("SetLevelEnd", 1.5f);
        }
        private void SetLevelEnd()
        {
            if (_skore >= winCount)
                GameManager.Instance.SetLevelComplete();
            else
                GameManager.Instance.SetGameOver();
        }
    }
}
