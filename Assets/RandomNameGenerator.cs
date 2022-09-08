using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNameGenerator {
    private static string _prefixesPath = "Text/Prefix";
    private static string _suffixesPath = "Text/Suffix";

    private static string[] _prefixes = null;
    private static string[] _suffixes = null;

    /// <summary>
    /// Generates a random country name.
    /// </summary>
    /// <returns>Country name</returns>
    public static string GenerateName() {
        string name = "";

        LoadTextAssets();

        name += _prefixes[Random.Range(0, _prefixes.Length)];
        name += " ";
        name += _suffixes[Random.Range(0, _suffixes.Length)];
        name = name.Replace("\n", "").Replace("\r", "");

        return name;
    }
    
    /// <summary>
    /// Used to load lists of prefixes and suffixes into memory
    /// </summary>
    private static void LoadTextAssets() {
        if(_prefixes == null) {
            TextAsset prefixText = Resources.Load<TextAsset>(_prefixesPath);
            _prefixes = prefixText.text.Split('\n');
        }

        if(_suffixes == null) {
            TextAsset suffixText = Resources.Load<TextAsset>(_suffixesPath);
            _suffixes = suffixText.text.Split('\n');
        }
    }

    /// <summary>
    /// Unloads the lists of prefixes and suffixes from memory
    /// </summary>
    private static void Reset() {
        _prefixes = null;
        _suffixes = null;
    }
}
