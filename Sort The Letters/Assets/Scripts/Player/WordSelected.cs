using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordSelected : MonoBehaviour
{
    [SerializeField] private WordDataScriptableObjects wordData;

    private const string _lastIndex = "lastIndex";

    public List<string> GetRandomWords()
    {
        List<string> words = new List<string>();
        // 7 kelime seç
        int lastIndex = 0;
        if (PlayerPrefs.HasKey(_lastIndex))
            lastIndex = PlayerPrefs.GetInt(_lastIndex);
        
        var count = lastIndex + 7;

        if (count >= wordData.WordList.Count - 1)
        {
            lastIndex = 0;
            count = lastIndex + 7;
        } 
        for (int i = lastIndex; i < count; i++)
        {
            words.Add(wordData.WordList[i]);
        }
        PlayerPrefs.SetInt(_lastIndex, count);

        return words;
    }

    // secilen kelimelerin 2 harfini yer degistir
    public List<string> GetWrongWords(List<string> words)
    {
        List<string> wrongwords = new List<string>();
        // Yer Degistir
        for (int i = 0; i < words.Count; i++)
        {
            var letters = words[i].ToCharArray();
            var randomIndex = Random.Range(0, letters.Length);
            var secontIndex = randomIndex + Random.Range(1, 5);
            if (secontIndex > letters.Length - 1)
                secontIndex -= letters.Length;
            // Ayni harf olmadigini kontrol et
            if (letters[randomIndex] == letters[secontIndex])
            {
                secontIndex++;
                if (secontIndex > letters.Length - 1)
                    secontIndex -= letters.Length;
            }

            var randomLetter = letters[randomIndex];
            letters[randomIndex] = letters[secontIndex];
            letters[secontIndex] = randomLetter;
            var wrongWord = letters.ArrayToString();

            wrongwords.Add(wrongWord);
        }

        return wrongwords;
    }
}
