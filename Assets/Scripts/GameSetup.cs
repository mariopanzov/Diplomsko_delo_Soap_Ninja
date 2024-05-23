using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
//using UnityEngine.ResourceManagement.AsyncOperations;


[CreateAssetMenu]
public class GameSetup : ScriptableObject
{ 
    [Header("Events")]
    public GameEvent _gameEvent_animations;
    public GameEvent _gameEvent_particles;
    public GameEvent _gameEvent_change_scene;
    public GameEvent _gameEvent_change_state;
    public GameEvent _gameEvent_change_button_image_state;
    public GameEvent _gameEvent_focus_highlight_step_state;
    public GameEvent _gameEvent_add_checkmark;
    public GameEvent _gameEvent_toggle_touch_settings;
    public GameEvent _gameEvent_toggle_grid_buttons;

    public bool _game_complete;
    public int _picked_level = 0;
    public int _n_of_completed_levels = 0;

    public GameplaySceneLevelSetup[] levelSetup;

    public void resetGameSetup()
    {
        _picked_level = 0;
        _game_complete = false;
        _n_of_completed_levels = 0;

        for(int i = 0; i < levelSetup.Length; i++)
        {
            levelSetup[i]._level_completed = false;
        }
        Debug.Log("reset");
    }

    public void completeLevel(int _picked_level)
    {
        levelSetup[_picked_level]._level_completed = true;
        _n_of_completed_levels++;
        completeGame();
    }

    public void completeGame()
    {
        _game_complete = true;

        for(int i = 0; i < levelSetup.Length; i++)
        {
            if(!levelSetup[i]._level_completed) _game_complete = false;
        }
    }
}

[System.Serializable]
public class GameplaySceneLevelSetup
{
    public AssetReferenceGameObject _bacteria_assetReferenceGameObject;
    
    public string[] _hand_washing_steps;

    public bool _level_completed = false;

    ~GameplaySceneLevelSetup(){}
}