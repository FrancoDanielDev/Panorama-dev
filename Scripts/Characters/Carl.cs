using UnityEngine;

public class Carl : NPC
{
    [SerializeField] private Collider _collider;

    private int _conversations = 0;

    protected override void Interaction()
    {
        switch (_conversations)
        {
            case 0:
                DialogueManager.instance.TriggerDialogue(EventDialogue());
                _currentEvent = "Normal2";
                break;

            case 1:
                DialogueManager.instance.TriggerDialogue(EventDialogue());
                _collider.enabled = false;
                break;
        }

        _conversations++;
    }
}
