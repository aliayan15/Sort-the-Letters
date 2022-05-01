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
    [SerializeField] private int index;
    [SerializeField] private Image image;
    [SerializeField] private WordLetters wordLetter;

    private Animator _anim;
    private int _redAnim = Animator.StringToHash("Red");
    private int _greenAnim = Animator.StringToHash("Green");

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.enabled = false;
    }

    public void Pressed()
    {
        if (image.color == dataStore.SelecteColor) return;
        image.color = dataStore.SelecteColor;
        wordLetter.Pressed(index);
        GameManager.Instance.uiManager.gameCanvasManager.MouseDown();
    }

    public void CorrectChose()
    {
        if (!_anim.enabled)
            _anim.enabled = true;
        _anim.SetTrigger(_greenAnim);
    }

    public void WrongChose()
    {
        if (!_anim.enabled)
            _anim.enabled = true;
        _anim.SetTrigger(_redAnim);
    }

    public void Clear()
    {
        image.color = dataStore.NormalColor;
    }
}
