using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, string,  object> {}

public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent;

    public CustomGameEvent response;

    private void OnEnable()
    {
        gameEvent.registerListener(this);
    }

    private void OnDisable()
    {
        gameEvent.unregisterListener(this);
    }
    
    //raising an Event
    public void OnEventRaised(Component sender, string function, object data)
    {
        response.Invoke(sender, function, data);
    }
}