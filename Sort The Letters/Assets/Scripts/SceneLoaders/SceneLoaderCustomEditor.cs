using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SceneLoaders
{
#if UNITY_EDITOR
    [CustomEditor(typeof(LevelManager))]
    public class SceneLoaderCustomEditor : Editor
    {
        private int _selected;

        public override void OnInspectorGUI()
        {
            var sceneLoader = (LevelManager)target;

            DrawDefaultInspector();

            EditorGUILayout.Space();
            if (GUILayout.Button("Load Scenes From Build Settings"))
            {
                SetSceneNamesFromBuildSettings(sceneLoader);
                SetSelection(sceneLoader, 0);

                EditorUtility.SetDirty(sceneLoader);
            }

            EditorGUILayout.Space();
            //HorizontalLine();
            //HandleTestSceneSelection(sceneLoader);
        }

        private void SetSelection(LevelManager sceneLoader, int selection)
        {
            if (selection == 0)
            {
                sceneLoader.SetTestScene(null);
            }
            else
            {
                sceneLoader.SetTestScene(selection - 1);
            }

            EditorUtility.SetDirty(sceneLoader);
            _selected = selection;
        }



        private void SetSceneNamesFromBuildSettings(LevelManager sceneLoader)
        {
            int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
            string[] scenes = new string[sceneCount];
            for (int i = 0; i < sceneCount; i++)
            {
                scenes[i] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility
                    .GetScenePathByBuildIndex(i));
            }
            // Do not take non-game scenes
            scenes = scenes.Where(s => s != GlobalConsts.MainSceneName
            && s != GlobalConsts.LoadSceneName
            && s != GlobalConsts.ElephantSceneName
            && s != GlobalConsts.EmptySceneName).ToArray();
            sceneLoader.SetAllScenes(scenes);
        }

        private void HorizontalLine(int height = 1)
        {
            Rect rect = EditorGUILayout.GetControlRect(false, height);
            rect.height = height;
            EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
        }
    }
#endif
}