using UnityEngine;

public class DialogueCollider : DialogueTrigger
{
    [Space]
    [SerializeField] private bool _oneUse;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            DialogueManager.instance.TriggerDialogue(EventDialogue());

            if (_oneUse)
                gameObject.SetActive(false);
        }
    }
}
