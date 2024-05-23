using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class ButtonsState<T> : BaseState<T> where T : GameplayStateManagerScript
{
    private LTSeq seq;

    public override void updateState(T state_manager)
    {
        seq = LeanTween.sequence();


        if(state_manager._gameplay_loop == 0)
        {               
            seq.append(
                () => {
                    state_manager.gameSetup._gameEvent_toggle_touch_settings.Raise(state_manager, "TouchPress", "disable");
                }
            );
            seq.append(
                () => {
                    state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "openStepsGridAnimation", 0);
                }
            );
            seq.append(1.2f);
        }

        seq.append(
            () => {
                state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "changeNinjaAnimation", "");
            }
        );

        seq.append(
            () => {
                state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "showHighlightAnimation", 0);

            }
        );
        seq.append(0.6f);
                
        seq.append(
            () => {
                state_manager.gameSetup._gameEvent_toggle_touch_settings.Raise(state_manager, "TouchPress", "enable");
            }
        );
        seq.append(
            () => {
                state_manager._change_state_trigger = true;
            }
        );
    }

    public override void transitionToNextState(T state_manager)
    {
        state_manager.switchStates(state_manager._start_hand_washing_step_state);
    }
}
