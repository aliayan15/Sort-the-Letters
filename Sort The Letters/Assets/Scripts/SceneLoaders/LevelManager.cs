using UnityEngine.SceneManagement;
using UnityEngine;
using HyperCasualTemplateHelpers;
using Players.Stores;

namespace SceneLoaders
{
    public class LevelManager : MonoBehaviour
    {
        public static string CurrentLoadingSceneName;
        public static bool CanLoad = true;

        [SerializeField] private string[] allScenes;
        [HideInInspector] [SerializeField] private int _testSceneIndex;

        private string _lastRandomScene;

        private void Start()
        {
            if (_testSceneIndex != -1 && _testSceneIndex >= allScenes.Length)
            {
                _testSceneIndex = -1;
            }
        }

        public void LoadScene()
        {
#if UNITY_EDITOR
            if (TryLoadTestScene())
            {
                return;
            }
#endif

            var currentLevel = PlayerLevelStore.GetCurrentLevel();
            var sceneName = allScenes.Length < currentLevel
                ? GetRandomScene()
                : allScenes[currentLevel - 1];

            _lastRandomScene = sceneName;
            LoadLoaderFor(sceneName);
        }

        private bool TryLoadTestScene()
        {
            // tepmlate de scene al
            var (sceneName, _) = EditorConfigManager.GetTestScene();
            if (!string.IsNullOrWhiteSpace(sceneName))
            {
                LoadLoaderFor(sceneName);
                return true;
            }
            if (_testSceneIndex > -1)
            {
                LoadLoaderFor(allScenes[_testSceneIndex]);
                return true;
            }


            return false;
        }

        // LevelScene yükle, ordan CurrentLoadingSceneName yükle
        public void LoadLoaderFor(string requestedScene)
        {
            if (!CanLoad)
            {
                return;
            }

            CanLoad = false;
            CurrentLoadingSceneName = requestedScene;
            SceneManager.LoadScene(GlobalConsts.LoadSceneName);
        }

        public void LoadCurrentLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }

        private string GetRandomScene()
        {
            var scene = RandomScene();
            if (scene == _lastRandomScene)
            {
                scene = RandomScene();
            }
              
            Debug.Log("Load Random Scene");
            return scene;
        }

        private string RandomScene()
        {
            return allScenes[Random.Range(0, allScenes.Length)];
        }


        public float GetGameLevel()
        {
            return PlayerLevelStore.GetCurrentLevel();
        }

        public void SetTestScene(int? index)
        {
            if (!index.HasValue)
            {
                _testSceneIndex = -1;
            }
            else
            {
                if (index.Value >= allScenes.Length)
                {
                    _testSceneIndex = -1;
                }
                else
                {
                    _testSceneIndex = index.Value;
                }
            }
        }


        public void SetAllScenes(string[] scenes)
        {
            allScenes = scenes;
        }

    }
}
