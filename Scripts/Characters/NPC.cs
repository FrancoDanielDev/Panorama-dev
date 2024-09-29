public abstract class NPC : DialogueTrigger, IInteractableObj
{
    public void Interact()
    {
        Interaction();
    }

    protected virtual void Interaction()
    {
        DialogueManager.instance.TriggerDialogue(EventDialogue());
    }
}
