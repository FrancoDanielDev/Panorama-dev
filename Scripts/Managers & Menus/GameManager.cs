using UnityEngine;

public enum Perspective
{
    Side,
    Top
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Perspective _currentPerspective = Perspective.Side;

    public bool onTransition;

    public Perspective Perspective
    {
        get => _currentPerspective;
        set
        {
            if (value != _currentPerspective)
            {
                _currentPerspective = value;
                EventManager.CallEvent(Events.ChangePerspective);
            }
        }
    }

    private void Awake()
    {
        if (Instance) Destroy(gameObject);
        else Instance = this;
    }
}
