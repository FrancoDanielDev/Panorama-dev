using UnityEngine;
using TMPro;

public class ContinueConversation : MonoBehaviour
{
    [SerializeField] private string _continueID;
    [SerializeField] private string _endConversationID;
    [Space]
    [SerializeField] private TMP_Text _text;

    private string _originalID;

    private void Start()
    {
        _originalID = _continueID;

        DialogueManager.instance.EndConversationEvent += EndConversation;
        DialogueManager.instance.EndedConversationEvent += EndedConversation;
        LocalizationManager.instance.TranslationEvent += UpdateText;

        UpdateText();
    }

    private void EndConversation()
    {
        _continueID = _endConversationID;
        UpdateText();
    }

    private void EndedConversation()
    {
        _continueID = _originalID;
        UpdateText();

        if (FindObjectOfType<Player>() != null)
            Player.instance.OnEnabled(true);
    }

    private void UpdateText()
    {
        _text.text = LocalizationManager.instance.GetTranslation(_continueID);
    }
}
