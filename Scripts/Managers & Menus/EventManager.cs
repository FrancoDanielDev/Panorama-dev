using System;
using System.Collections.Generic;

public enum Events
{
    ChangePerspective
}

public static class EventManager
{
    private static Dictionary<Events, Action> _events = new();

    public static void Subscribe(Action listener, Events toEvent)
    {
        if (!_events.ContainsKey(toEvent))
        {
            _events.Add(toEvent, listener);
        }
        else
        {
            _events[toEvent] += listener;
        }
    }

    public static void Unsubscribe(Action listener, Events fromEvent)
    {
        if (_events.ContainsKey(fromEvent))
        {
            _events[fromEvent] -= listener;
        }
    }

    public static void CallEvent(Events toEvent)
    {
        if (_events.ContainsKey(toEvent))
        {
            _events[toEvent].Invoke();
        }
    }
}
