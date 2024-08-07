using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattleGridState<T> : BaseState<T> where T : LevelSelectStateManagerScript
{
    private LTSeq seq;

    public override void updateState(T state_manager)
    {
        seq = LeanTween.sequence();
                    
        seq.append(
            () => {
                state_manager.gameSetup._gameEvent_toggle_touch_settings.Raise(state_manager, "TouchPress", "disable");
            }
        );
        
        seq.append(
            () => {
                state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "changeNinjaAnimation", "");
            }
        );

        if(state_manager._step_loop == 0)
        {
            seq.append(
                () => {
                    state_manager.gameSetup._gameEvent_toggle_grid_buttons.Raise(state_manager, "activate", 0);
                    state_manager._image_index = 0;
                }
            );
           
            seq.append(1.2f);   
            seq.append(
                () => {
                    state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "levelSelectCameraZoomInRight", 0);
                }
            );
           
            seq.append(1.8f);
            seq.append(
                () => {
                    state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "levelSelectCameraPanRightToLeft", 0);
                }
            );
            seq.append(1.2f);
        }
        else
        {
            seq.append(
                () => {
                    state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "closePopupAnimation", 0);
                }
            );
        }

        seq.append(
            () => {
                state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "openStepsGridAnimation", 0);
            }
        );
        seq.append(1.2f);


        if(state_manager._step_loop != 0)
        {
            seq.append(
                () => {
                    state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "checkmarkAnimation", state_manager._image_index - 1);
                }
            );
            seq.append(1.2f);
        }

        if(state_manager._step_loop == 2)
        {
            seq.append(
                () => {
                    state_manager.gameSetup._gameEvent_toggle_touch_settings.Raise(state_manager, "TouchPress", "disable");
                }
            );
            seq.append(
                () => {
                    state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "closeStepsGridAnimation", 0); 
                    state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "levelSelectCameraZoomOutLeft",  0);
                }
            );
            seq.append(1.2f);
            seq.append(
                () => {
                    state_manager.gameSetup._gameEvent_toggle_touch_settings.Raise(state_manager, "TouchPress", "enable");
                }
            );
        }
        else
        {
            seq.append(
                () => {
                    state_manager.gameSetup._gameEvent_focus_highlight_step_state.Raise(state_manager, "", state_manager._image_index);// loop 0, 1 or just 0 or again 0 1               
                    state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "showHighlightAnimation", 0);
                }
            );
        }

        seq.append(
            () => {
                state_manager.gameSetup._gameEvent_toggle_touch_settings.Raise(state_manager, "TouchPress", "enable");
            }
        );
    }

    public override void transitionToNextState(T state_manager)
    {
        state_manager.switchStates(state_manager._start_battle_button_state);
    }
}
