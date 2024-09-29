using UnityEngine;
using TMPro;

public class LocalizedInterface : MonoBehaviour
{
    [SerializeField] private string _ID;
    [Space]
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        LocalizationManager.instance.TranslationEvent += UpdateText;
        UpdateText();
    }

    private void UpdateText()
    {
        _text.text = LocalizationManager.instance.GetTranslation(_ID);
    }
}
