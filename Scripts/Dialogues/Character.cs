using UnityEngine;

[System.Serializable]
public struct CharacterExpressions
{
    public Expressions expression;
    public Sprite sprite;
}

public enum Expressions
{
    Neutral,
    Happy,
    Angry,
    Sad,
    Scared
}

[CreateAssetMenu(menuName = "Data/CharacterData")]
public class Character : ScriptableObject
{
    public string Name;
    public CharacterExpressions[] Expressions;

    public Sprite GetExpression(Expressions expression)
    {
        foreach (var item in Expressions)
        {
            if (expression == item.expression)
                return item.sprite;
        }

        return null;
    }
}