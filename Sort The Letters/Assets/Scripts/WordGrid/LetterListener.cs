using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Managers;

public class LetterListener : MonoBehaviour
{
    [SerializeField] private LetterDataStore dataStore;
    [Header("Compenents")]
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;
    [SerializeField] private WordLetters wordLetter;

    public void Pressed()
    {
        image.color = dataStore.SelecteColor;
        wordLetter.Pressed(text.text);
        GameManager.Instance.uiManager.gameCanvasManager.MouseDown();
    }

   public void Clear()
    {
        image.color = dataStore.NormalColor;
    }
}
