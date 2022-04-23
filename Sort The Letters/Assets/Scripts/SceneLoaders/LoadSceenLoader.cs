using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

namespace SceneLoaders
{
    public class LoadSceenLoader : MonoBehaviour
    {
        [SerializeField] private Image slider;

        private void Start()
        {
            slider.fillAmount = 0;

            LevelManager.CanLoad = true;

            StartCoroutine(LoadAsync(LevelManager.CurrentLoadingSceneName));
        }

        IEnumerator LoadAsync(string sceneName)
        {
            GC.Collect();
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                SetProgress(progress);
                yield return null;
            }

            Resources.UnloadUnusedAssets();
            GC.Collect();
        }
        private void SetProgress(float progress)
        {
            slider.fillAmount = progress;
        }
    }
}
