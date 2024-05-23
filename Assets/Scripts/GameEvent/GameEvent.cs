using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new  List<GameEventListener>();

    public void Raise(Component sender, string function, object data)
    {
        for(int i = listeners.Count -1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(sender, function, data);
        }
    }

    public void registerListener(GameEventListener listener)
    {
        if(!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public void unregisterListener(GameEventListener listener)
    {
        if(listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}
