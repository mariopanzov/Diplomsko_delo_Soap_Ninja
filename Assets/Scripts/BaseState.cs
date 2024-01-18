using UnityEngine;

public abstract class BaseState
{   
    public abstract void onEnterState();
    public abstract void updateState(StateManagerScript state_manager);
    public abstract void transitionToNextState(StateManagerScript state_manager);
}
