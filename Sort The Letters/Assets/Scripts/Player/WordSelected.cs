using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSelected : MonoBehaviour
{
    [SerializeField] private WordDataScriptableObjects wordData;

    private const string _lastIndex = "lastIndex";

    public List<string> GetRandomWords()
    {
        List<string> words = new List<string>();
        // 7 kelime se�
        int lastIndex = 0;
        if (PlayerPrefs.HasKey(_lastIndex))
            lastIndex = PlayerPrefs.GetInt(_lastIndex);

        var count = lastIndex + 7;
        for (int i = lastIndex; i < count; i++)
        {
            words.Add(wordData.WordList[i]);
        }
        PlayerPrefs.SetInt(_lastIndex, count);
        
        return words;
    }

    public List<string> GetWrongWords(List<string> words)
    {
        List<string> wrongwords = new List<string>();
        // secilen kelimelerin 2 harfini yer degistir
        for (int i = 0; i < words.Count; i++)
        {
            var letters = words[i].ToCharArray();
            var randomIndex = Random.Range(0, letters.Length);
            var secontIndex = randomIndex + Random.Range(1, 5);
            if (secontIndex > letters.Length - 1)
                secontIndex -= letters.Length;

            var randomLetter = letters[randomIndex];
            letters[randomIndex] = letters[secontIndex];
            letters[secontIndex] = randomLetter;
            var wrongWord = "";
            for (int v = 0; v < letters.Length; v++)
            {
                wrongWord += letters[v].ToString();
            }
            wrongwords.Add(wrongWord);
        }

        return wrongwords;
    }
}
