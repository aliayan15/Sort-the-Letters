using System;
using UnityEngine;
using Utilities.Data;

namespace HyperCasualTemplateHelpers
{
    [Serializable]
    public class EditorConfigData
    {
        public string TestSceneName;
        public string TestScenePath;
    }

    public class EditorConfigManager
    {
        public static EditorConfigData GetConfig()
        {
            return JsonDataStore.GetData<EditorConfigData>(GlobalConsts.EditorConfigFileName);
        }

        public static void SetConfig(EditorConfigData data)
        {
            JsonDataStore.StoreData(GlobalConsts.EditorConfigFileName, data);
        }

        public static void SetTestScene(string sceneName, string scenePath)
        {
            Debug.Log("Setting test scene to " + sceneName);

            var config = GetConfig();
            if (config == null)
            {
                config = new EditorConfigData();
            }

            config.TestSceneName = sceneName;
            config.TestScenePath = scenePath;
            SetConfig(config);
        }

        public static (string sceneName, string scenePath) GetTestScene()
        {
            var config = GetConfig();
            if (config == null)
            {
                return (null, null);
            }
            
            return (config.TestSceneName, config.TestScenePath);
        }

        public static void ClearTestScene()
        {
            var config = GetConfig();
            if (config == null)
            {
                config = new EditorConfigData();
            }

            config.TestSceneName = null;
            config.TestScenePath = null;
            SetConfig(config);
        }
    }
}