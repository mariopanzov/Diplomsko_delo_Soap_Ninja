using UnityEngine;

public abstract class BaseState<T>
{   
    public abstract void updateState(T state_manager);
    public abstract void transitionToNextState(T state_manager);
}
