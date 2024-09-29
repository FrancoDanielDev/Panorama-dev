using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour, IObservable
{
    public static MenuManager instance;

    [SerializeField] private GameObject _pauseMenu;

    private List<IObserver> _allObservers = new List<IObserver>();
    private bool _paused = false;
    private bool _interactionsInCooldown = false;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        Unpause();
    }

    private void Update()
    {
        if (_paused) return;

        if (Input.GetKeyDown(KeyCode.E) && !_interactionsInCooldown)
            InteractButton();

        if (Input.GetKeyDown(KeyCode.P))
            Pause();
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
    }

    public void InteractButton()
    {
        NotifyToObservers("Interact");
    }

    public void Pause()
    {
        Time.timeScale = 0;
        _paused = true;
        _pauseMenu.SetActive(true);
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        _paused = false;
        _pauseMenu.SetActive(false);
    }

    public void InteractionsInCooldown(bool value)
    {
        _interactionsInCooldown = value;
    }
}
