using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new  List<GameEventListener>();

    //Raise event through different methods signitures
    public void Raise(Component sender, object data)
    {
        for(int i = listeners.Count -1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(sender, data);
        }
    }

    //Mange Listeners
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
