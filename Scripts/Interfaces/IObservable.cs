public interface IObservable
{
    void Subscribe(IObserver obs);

    void Unsubscribe(IObserver obs);

    void NotifyToObservers(string action);
}

// Complete the Observable's script with this pattern.

/*
    public static "Script's name here" instance;

    List<IObserver> _allObservers = new List<IObserver>();

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    // Condition as example
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            InteractButton();
        }
    }

    public void Subscribe(IObserver obs)
    {
        if (!_allObservers.Contains(obs))
            _allObservers.Add(obs);
    }

    public void Unsubscribe(IObserver obs)
    {
        if (_allObservers.Contains(obs))
            _allObservers.Remove(obs);
    }

    public void NotifyToObservers(string action)
    {
        for (int i = 0; i < _allObservers.Count; i++)
        {
            _allObservers[i].Notify(action);
        }
            Debug.Log(_allObservers.Count);
    }

    public void InteractButton()
    {
        NotifyToObservers("Interact");
    }
*/
