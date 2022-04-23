using UnityEngine;
using System;
using Players;
using SceneLoaders;
using Players.Stores;

namespace Managers
{
    public enum GameStates
    {
        GAME,
        MENU,
        LEVELCOMPLETE,
        GAMEOVER
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public static event Action<GameStates> OnGameStateChange;

        [HideInInspector] public Player player;
        public LevelManager LevelManager;
        public UIManager uiManager;
        public Joystick joystick;
        public SettingManager settingManager;


        [Header("Set State")]
        public GameStates GameStateToSet;

        private GameStates _gameState;
        public GameStates GameState
        {
            get
            {
                return _gameState;
            }
            set
            {
                _gameState = value;
                OnGameStateChange?.Invoke(value);
            }
        }


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            // Loading ekranını aç
            uiManager.SetLoading();
            LevelManager.LoadScene();
        }

        public void SetState(GameStates state)
        {
            switch (state)
            {
                case GameStates.GAME:
                    SetGame();
                    break;
                case GameStates.MENU:
                    SetMenu();
                    break;
                case GameStates.LEVELCOMPLETE:
                    SetLevelComplete();
                    break;
                case GameStates.GAMEOVER:
                    SetGameOver();
                    break;
                default:
                    break;
            }
        }

        // Set Game States
        public void SetGame()
        {
            GameState = GameStates.GAME;
            uiManager.SetGame();
            Debug.Log(GameState);
        }

        public void SetMenu()
        {
            GameState = GameStates.MENU;
            uiManager.SetMenu();
            Debug.Log(GameState);
        }

        public void SetLevelComplete()
        {
            if (GameState == GameStates.LEVELCOMPLETE) return;
            GameState = GameStates.LEVELCOMPLETE;
            uiManager.SetLevelComolete();
            Debug.Log(GameState);
        }

        public void SetGameOver()
        {
            if (GameState == GameStates.GAMEOVER) return;
            GameState = GameStates.GAMEOVER;
            uiManager.SetGameOver();
            Debug.Log(GameState);
        }

        public void LoadNextLevel()
        {
            PlayerLevelStore.SetNextLevel();
            LevelManager.LoadScene();
        }
        

    }
}
