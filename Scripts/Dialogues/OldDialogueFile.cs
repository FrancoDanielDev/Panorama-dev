using UnityEngine;

[CreateAssetMenu(menuName = "Data/OldDialogueData")]
public class OldDialogueFile : ScriptableObject
{
    public OldDialogueLine[] DialogueLines;
}

[System.Serializable]
public struct OldDialogueLine
{
    public string name;
    public string expression;
    public string line;
}