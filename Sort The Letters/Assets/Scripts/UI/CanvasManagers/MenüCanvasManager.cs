using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenüCanvasManager : MonoBehaviour
{
    [SerializeField] private Image settingsBackground;


    CanvasGroup canvasSettingsBack;

    private bool isSettingsOn = false;

    private void Awake()
    {
        canvasSettingsBack = settingsBackground.GetComponent<CanvasGroup>();
    }

    void Start()
    {
        canvasSettingsBack.DOFade(0, 0f);
    }

    public void OnOffSettings()
    {
        if (isSettingsOn)
        {
            canvasSettingsBack.DOFade(0, 0.5f);
        }

        if (!isSettingsOn)
        {
            canvasSettingsBack.DOFade(1, 0.7f);
        }

        isSettingsOn = !isSettingsOn;
    }
}
