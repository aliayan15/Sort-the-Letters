#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadExcel : MonoBehaviour
{
    [SerializeField] private WordDataScriptableObjects wordData;


    public void LoadWorddData()
    {
        wordData.WordList.Clear();

        // Read CSV file
        List<Dictionary<string, object>> data = CSVReader.Read("WordsCSV");
        var count = data.Count;
        for (int i = 0; i < count; i++)
        {
            string newWord = data[i]["Word"].ToString();
            wordData.WordList.Add(newWord);
        }
    }
}
#endif
