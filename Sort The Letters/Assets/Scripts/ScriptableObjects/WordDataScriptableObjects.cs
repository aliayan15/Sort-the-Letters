using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/WordData")]
public class WordDataScriptableObjects : ScriptableObject
{
    public List<string> WordList = new List<string>();
}
