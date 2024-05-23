using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectStateManagerScript : MonoBehaviour

{
    [Header("Game Setup")]
    public GameSetup gameSetup;

    [System.NonSerialized] public BaseState<LevelSelectStateManagerScript> _level_select_current_state;

    [System.NonSerialized] public StartBattleGridState<LevelSelectStateManagerScript> _start_battle_grid_state;
    [System.NonSerialized] public StartBattleButtonState<LevelSelectStateManagerScript> _start_battle_button_state;
    [System.NonSerialized] public StartBattlePopupState<LevelSelectStateManagerScript> _start_battle_popup_state;

    [System.NonSerialized] public EndBattleGridState<LevelSelectStateManagerScript> _end_battle_grid_state;
    [System.NonSerialized] public EndBattleButtonState<LevelSelectStateManagerScript> _end_battle_button_state;
    [System.NonSerialized] public EndBattlePopupState<LevelSelectStateManagerScript> _end_battle_popup_state;

    [System.NonSerialized] public int _step_loop;
    [System.NonSerialized] public int _image_index;

    private void Awake()
    {
        _step_loop = 0;
        
        _start_battle_grid_state = new StartBattleGridState<LevelSelectStateManagerScript>();
        _start_battle_button_state = new StartBattleButtonState<LevelSelectStateManagerScript>();
        _start_battle_popup_state = new StartBattlePopupState<LevelSelectStateManagerScript>();
        
        _end_battle_grid_state = new EndBattleGridState<LevelSelectStateManagerScript>();
        _end_battle_button_state = new EndBattleButtonState<LevelSelectStateManagerScript>();
        _end_battle_popup_state = new EndBattlePopupState<LevelSelectStateManagerScript>();

        if(gameSetup._game_complete)
        {
            Debug.Log("end battle states");
            _level_select_current_state = _end_battle_grid_state;
            _image_index = 2;
        }
        else
        {
            Debug.Log("start battle states");
            _level_select_current_state = _start_battle_grid_state;
            _image_index = 0;
        }
        
        //BEING CALLED ONLY ONCE WHEN THE SCENE LOADS
        manageState(this, "", 0);
    }

    public void manageState(Component sender, string function, object data)
    {
        _level_select_current_state.updateState(this);
        _level_select_current_state.transitionToNextState(this);
    }

    public void switchStates(BaseState<LevelSelectStateManagerScript> _state)
    {
        _level_select_current_state = _state;
    }
}

