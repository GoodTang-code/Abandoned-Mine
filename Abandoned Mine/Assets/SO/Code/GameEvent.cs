//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "SO/GameEvent", order = 1)]
public class GameEvent : ScriptableObject
{

    private readonly List<GameEventListener> eventListeners = new List<GameEventListener>();

    public void Raise()
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--) //Reverse Loop, in case anything remove mid-run.
            eventListeners[i].OnEventRaised();
    }

    public void RegisterListener(GameEventListener listener) // OnEnable
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener) // OnDisable
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}
