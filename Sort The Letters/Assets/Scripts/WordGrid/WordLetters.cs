using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class WordLetters : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] letterTests;
    [SerializeField] private LetterListener[] letterListeners;

    private bool _isPressed;
    private bool _canPress = true;
    private string _firstLetter;
    private string _secondLetter;
    private int _selectCount;
    private string _correctWord;

    public void SetLetters(string wrongWord, string correctWord)
    {
        _correctWord = correctWord;
        var letters = wrongWord.ToCharArray();
        for (int i = 0; i < letters.Length; i++)
        {
            letterTests[i].text = letters[i].ToString();
        }
    }

    public void CheckMouseDown()
    {
        if (_isPressed)
        {
            _isPressed = false;
            _selectCount++;
            if (_selectCount >= 2)
            {
                // 2. harf secildi
                LettersSelected();
                _selectCount = 0;
            }
            return;
        }
        for (int i = 0; i < letterListeners.Length; i++)
        {
            letterListeners[i].Clear();
        }
    }

    private void LettersSelected()
    {
        _canPress = false;
        Debug.Log("first: " + _firstLetter + "  second: " + _secondLetter);
    }

    public void Pressed(string letter)
    {
        if (!_canPress) return;
        _isPressed = true;

        if (_firstLetter == null)
            _firstLetter = letter;
        else if (_secondLetter == null)
            _secondLetter = letter;
    }

}