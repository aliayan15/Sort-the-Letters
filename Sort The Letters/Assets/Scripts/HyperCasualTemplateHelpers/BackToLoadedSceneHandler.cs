#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

namespace HyperCasualTemplateHelpers
{

    [InitializeOnLoad]
    public static class BackToLoadedSceneHandler
    {
        static BackToLoadedSceneHandler()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredEditMode) TryBackToLoadedScene();
        }

        private static void TryBackToLoadedScene()
        {
            var (_, testScenePath) = EditorConfigManager.GetTestScene();
            if (!string.IsNullOrWhiteSpace(testScenePath))
            {
                EditorConfigManager.ClearTestScene();
                EditorSceneManager.OpenScene(testScenePath);
            }
        }
    }

}
#endif