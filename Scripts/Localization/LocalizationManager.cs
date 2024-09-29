using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance;
    public event Action TranslationEvent;

    [SerializeField] private string _language        = "English";
    [SerializeField] private string _defaultLanguage = "English";
    [Space]
    [SerializeField] private TextAsset _file;

    private Dictionary<string, Dictionary<string, string>> _translations = new();

    private void Awake()
    {
        instance = this;
        TextOfTSVConversion();
    }

    private void TextOfTSVConversion()
    {
        var fileText = string.Join("", Regex.Split(_file.text, @"(?:\r)"));
        var row = fileText.Split('\n');
        var header = row[0].Split('\t');

        for (int i = 1; i < row.Length; i++)
        {
            Dictionary<string, string> values = new();
            var column = row[i].Split('\t');

            for (int j = 1; j < header.Length; j++)
            {
                values.Add(header[j], column[j]);
            }

            _translations.Add(column[0], values);
        }
    }

    public void ChangeLanguage(string language)
    {
        if (_language != language)
        {
            _language = language;
            PlayerPrefs.SetString("Language", language);
            PlayerPrefs.Save();
            TranslationEvent?.Invoke();
        }
    }

    public string GetTranslation(string ID)
    {
        if (!_translations.ContainsKey(ID))
        {
            Debug.LogError("Incorrect key.");
            return "NO KEY";
        }

        if (_translations[ID].ContainsKey(_language))
        {
            return _translations[ID][_language];
        }

        if (_language == _defaultLanguage)
        {
            Debug.LogError("No KEY available in the" + _defaultLanguage + " language.");
            return "NO KEY";
        }

        if (_translations[ID].ContainsKey(_defaultLanguage))
        {
            Debug.LogError("No KEY available in the " + _language + " language." +
                "\n Successfully replaced by " + _defaultLanguage + ".");
            return _translations[ID][_defaultLanguage];
        }
        else
        {
            Debug.LogError("No KEY available neither in the " + _language + " nor in the " + _defaultLanguage + " one.");
        }

        return "NO KEY";
    }
}