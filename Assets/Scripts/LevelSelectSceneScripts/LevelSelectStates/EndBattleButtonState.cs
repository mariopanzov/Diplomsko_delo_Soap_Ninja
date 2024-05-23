using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBattleButtonState<T> : BaseState<T> where T : LevelSelectStateManagerScript
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
                state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "hideHighlightAnimation", 0);
                state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "closeStepsGridAnimation", 0); 
                state_manager.gameSetup._gameEvent_change_button_image_state.Raise(state_manager, "", state_manager._image_index);
                state_manager.gameSetup._gameEvent_add_checkmark.Raise(state_manager, "", state_manager._image_index);
            }
        );

        seq.append(
            () => {
                state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "openButtonAnimation", 0);
            }
        );

        seq.append(1.2f);
        
        seq.append(
            () => {
                state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "showButtonHighlightAnimation", 0);
            }
        );

        seq.append(0.6f);
        seq.append(
            () => {
                state_manager.gameSetup._gameEvent_toggle_touch_settings.Raise(state_manager, "TouchPress", "enable");
            }
        );
    }

    public override void transitionToNextState(T state_manager)
    {
        state_manager.switchStates(state_manager._end_battle_popup_state);
    }
}