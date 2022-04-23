#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace HyperCasualTemplateHelpers
{

    public class TemplateMenuItems : MonoBehaviour
    {
        [MenuItem("Hyper-Casual Template/Play Current Scene", priority = 1)]
        private static void PlayCurrentScene()
        {
            var currentScene = EditorSceneManager.GetActiveScene();
            EditorSceneManager.SaveScene(currentScene);
            EditorConfigManager.SetTestScene(currentScene.name, currentScene.path);
            EditorSceneManager.OpenScene(GlobalConsts.MainScenePath);
            EditorApplication.EnterPlaymode();
        }

        [MenuItem("Hyper-Casual Template/Play Current Scene", isValidateFunction: true, priority = 2)]
        private static bool PlayCurrentSceneValidation()
        {
            if (Application.isPlaying)
            {
                return false;
            }

            var currentScene = EditorSceneManager.GetActiveScene();
            return CanLoadScene(currentScene.name);
        }

        [MenuItem("Hyper-Casual Template/Go to Main Scene", priority = 12)]
        private static void OpenMainScene()
        {
            EditorSceneManager.OpenScene(GlobalConsts.MainScenePath);
        }

        [MenuItem("Hyper-Casual Template/Go to Main Scene", isValidateFunction: true, priority = 13)]
        private static bool OpenMainSceneValidation()
        {
            if (Application.isPlaying)
            {
                return false;
            }

            var currentScene = EditorSceneManager.GetActiveScene();
            return currentScene.name != GlobalConsts.MainSceneName;
        }

        private static bool CanLoadScene(string sceneName)
        {
            return sceneName != GlobalConsts.MainSceneName && sceneName != GlobalConsts.LoadSceneName;
        }
    }

}
#endif