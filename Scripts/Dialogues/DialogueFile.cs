using UnityEngine;

[CreateAssetMenu(menuName = "Data/DialogueData")]
public class DialogueFile : ScriptableObject
{
    public DialogueLines[] secondDialogueLines;
}

[System.Serializable]
public struct DialogueLines
{
    public Character character;
    public Expressions expressions;
    public string line;
}
