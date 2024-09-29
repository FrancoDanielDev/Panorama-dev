using UnityEngine;
using UnityEngine.UI;

public class ContinueConversation2 : MonoBehaviour
{
    [SerializeField] private Image _image;
    [Space]
    [SerializeField] private Sprite _continueSprite;
    [SerializeField] private Sprite _endConversationSprite;

    private void Start()
    {
        DialogueManager.instance.EndConversationEvent += EndConversation;
        DialogueManager.instance.EndedConversationEvent += EndedConversation;
    }

    private void EndConversation()
    {
        _image.sprite = _endConversationSprite;
    }

    private void EndedConversation()
    {
        _image.sprite = _continueSprite;

        if (FindObjectOfType<Player>() != null)
            Player.instance.OnEnabled(true);
    }
}
