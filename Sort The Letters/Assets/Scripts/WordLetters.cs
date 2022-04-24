using System.Collections;
using UnityEngine;
using TMPro;

public class WordLetters : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] letterTests;

    public void SetLetters(string word)
    {
        var letters = word.ToCharArray();
        for (int i = 0; i < letters.Length; i++)
        {
            letterTests[i].text = letters[i].ToString();
        }
    }

}