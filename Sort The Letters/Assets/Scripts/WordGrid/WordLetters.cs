using System.Collections;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;
using Managers;

public class WordLetters : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] letterTests;
    [SerializeField] private LetterListener[] letterListeners;

    private bool _isPressed;
    private bool _canPress = true;
    private int _firstIndex;
    private int _secondIndex;
    private int _selectCount;
    private string _correctWord;
    private string _wrongWord;



    public void SetLetters(string wrongWord, string correctWord)
    {
        _correctWord = correctWord;
        _wrongWord = wrongWord;
        WhriteText(wrongWord);
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

        // Secimi kontrol etme
        var wordLetters = _wrongWord.ToCharArray();
        var firstLetter = wordLetters[_firstIndex];
        wordLetters[_firstIndex] = wordLetters[_secondIndex];
        wordLetters[_secondIndex] = firstLetter;
        var newWord = wordLetters.ArrayToString();

        StartCoroutine(PlayLetterChangeSesion(newWord, 0.2f));
    }

    // Harflerin yerlerinin degismesi ve dogruluk kontrolu
    private IEnumerator PlayLetterChangeSesion(string newWord, float time)
    {
        letterListeners[_firstIndex].transform.DOScale(0, time);
        letterListeners[_secondIndex].transform.DOScale(0, time);
        yield return new WaitForSeconds(time);
        WhriteText(newWord);
        letterListeners[_firstIndex].transform.DOScale(1, time);
        letterListeners[_secondIndex].transform.DOScale(1, time);
        yield return new WaitForSeconds(time + 0.2f);
        if (newWord == _correctWord)
            PlayCorrectChoseSesion();
        else
            PlayWrongChoseSesion();
        GameManager.Instance.player.TotalChoceCount++;
    }

    private void PlayCorrectChoseSesion()
    {
        StartCoroutine(PlayOpenningAnimation(true, 0.1f));
        GameManager.Instance.player.Skore++;
    }
    private void PlayWrongChoseSesion()
    {
        StartCoroutine(PlayOpenningAnimation(false, 0.1f));
    }

    // Harflerin acilmasi
    private IEnumerator PlayOpenningAnimation(bool correct, float time)
    {
        for (int i = 0; i < letterListeners.Length; i++)
        {
            if (correct)
                letterListeners[i].CorrectChose();
            else
                letterListeners[i].WrongChose();
            yield return new WaitForSeconds(time);
        }
    }

    private void WhriteText(string text)
    {
        var letters = text.ToCharArray();
        for (int i = 0; i < letters.Length; i++)
        {
            var letter = letters[i].ToString();
            if (letter == "i")
                letter = "ı";

            letterTests[i].text = letter;
        }
    }

    public void Pressed(int index)
    {
        if (!_canPress) return;
        _isPressed = true;

        if (_selectCount == 0)
            _firstIndex = index;
        else if (_selectCount == 1)
            _secondIndex = index;
    }

}