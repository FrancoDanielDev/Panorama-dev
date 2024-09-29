using System.Collections;
using UnityEngine;

public class TimeEvent : DialogueTrigger
{
    private void Start()
    {
        StartCoroutine(FivesecondsEvent());
    }

    private IEnumerator FivesecondsEvent()
    {
        yield return new WaitForSeconds(5);

        DialogueManager.instance.TriggerDialogue(EventDialogue());
        _currentEvent = "Crazy";

        yield return new WaitForSeconds(5);

        DialogueManager.instance.TriggerDialogue(EventDialogue());
        _currentEvent = "Normal";

        yield return new WaitForSeconds(5);

        DialogueManager.instance.TriggerDialogue(EventDialogue());
    }
}
