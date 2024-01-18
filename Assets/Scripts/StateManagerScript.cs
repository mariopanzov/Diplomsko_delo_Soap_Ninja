using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManagerScript : MonoBehaviour
{
    [Header("Animation Manager")]
    public GameObject _animationManager;
    public GameEvent _gameEvent_exit;
    public GameEvent _gameEvent_change_state;
    
    [Header("Particle Manager")]
    public GameObject _particleManager;
    [System.NonSerialized] public ParticleManagerScript _particles; 



    [Header("Level properties")]
    public GameplaySceneSetup gameplaySceneSetup;

    [Header("Events")]
    public GameEvent _gameEvent_change_button_image_state;
    public GameEvent _gameEvent_focus_highlight_step_state;
    public GameEvent _gameEvent_add_checkmark;


    [System.NonSerialized] public bool _level_complete;

    [System.NonSerialized] public int _gameplay_loop;
    [System.NonSerialized] public int _gameplay_number_of_steps;
    
    [System.NonSerialized] public AnimationManagerScript _animations; 
    
    [System.NonSerialized] public BaseState _current_state;
    [System.NonSerialized] public ButtonsState _buttons_state;
    [System.NonSerialized] public FightBacteriaState _fight_bacteria_state;
    [System.NonSerialized] public LevelCompleteState _level_complete_state;

    private void Awake()
    {
        _gameplay_number_of_steps = gameplaySceneSetup.levelSetup[gameplaySceneSetup._level_pick]._hand_washing_steps.Length;
        _gameplay_loop = 0;
        _level_complete = false;
        _animations = _animationManager.GetComponent<AnimationManagerScript>();
        _particles = _particleManager.GetComponent<ParticleManagerScript>(); 

        _buttons_state = new ButtonsState();
        _fight_bacteria_state = new FightBacteriaState();
        _level_complete_state = new LevelCompleteState();
        _current_state = _level_complete_state;

        manageState(this, 0);
    }

    public void manageState(Component sender, object data)
    {
        _current_state.transitionToNextState(this);
        _current_state.updateState(this);
    }

    public void switchState(BaseState state)
    {
        _current_state = state;
    }    
}
