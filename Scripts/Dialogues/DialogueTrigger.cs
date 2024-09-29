using UnityEngine;

[System.Serializable]
public struct Dialogues
{
    public string dialogueEvent;
    public DialogueFile dialogueFile;
}

public abstract class DialogueTrigger : MonoBehaviour
{
    [SerializeField] protected string _currentEvent;
    [Space]
    [SerializeField] protected Dialogues[] _dialogues;

    protected DialogueFile EventDialogue()
    {
        foreach (var item in _dialogues)
        {
            if (_currentEvent == item.dialogueEvent)
                return item.dialogueFile;
        }

        return null;
    }
}
