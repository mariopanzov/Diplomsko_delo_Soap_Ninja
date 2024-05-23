using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayStateManagerScript : MonoBehaviour

{
    [Header("Game Setup")]
    public GameSetup gameSetup;

    [System.NonSerialized] public int _gameplay_loop;
    [System.NonSerialized] public int _gameplay_number_of_steps;

    [System.NonSerialized] public BaseState<GameplayStateManagerScript> _gameplay_current_state;
    [System.NonSerialized] public ButtonsState<GameplayStateManagerScript> _buttons_state;
    [System.NonSerialized] public FightBacteriaState<GameplayStateManagerScript> _fight_bacteria_state;
    [System.NonSerialized] public LevelCompleteState<GameplayStateManagerScript> _level_complete_state;
    [System.NonSerialized] public StartHandWashingStepState<GameplayStateManagerScript> _start_hand_washing_step_state;

    [System.NonSerialized] public bool _change_state_trigger;

    private void Awake()
    {
        _change_state_trigger = true;

        _buttons_state = new ButtonsState<GameplayStateManagerScript>();
        _start_hand_washing_step_state = new StartHandWashingStepState<GameplayStateManagerScript>();
        _fight_bacteria_state = new FightBacteriaState<GameplayStateManagerScript>();
        _level_complete_state = new LevelCompleteState<GameplayStateManagerScript>();

        _gameplay_current_state = _buttons_state; 

        _gameplay_number_of_steps = gameSetup.levelSetup[gameSetup._n_of_completed_levels]._hand_washing_steps.Length;
        _gameplay_loop = 0;

        manageState(this, "", 0);
    }

    public void manageState(Component sender, string function, object data)
    {
        if(_change_state_trigger)
        {
            _change_state_trigger = false;
            _gameplay_current_state.updateState(this);
            _gameplay_current_state.transitionToNextState(this);
        }
    } 

    public void switchStates(BaseState<GameplayStateManagerScript> _state)
    {
        _gameplay_current_state = _state;
    }
}
