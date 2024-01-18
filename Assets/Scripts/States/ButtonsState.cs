using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsState : BaseState
{
    private LTSeq seq;

    public ButtonsState()
    {
    }

    public override void onEnterState()
    {

    }

    public override void updateState(StateManagerScript state_manager)
    {
        seq = LeanTween.sequence();

        if(state_manager._gameplay_loop == 0)
        {
            seq.append(
                () => {
                    state_manager._animations.openStepsGridAnimation();
                }
            );
            seq.append(1.2f);
        }
        seq.append(
            () => {
                state_manager._animations.showHighlightAnimation();
            }
        );
        seq.append(1.2f);
        seq.append(
            () => {
                state_manager._animations.hideHighlightAnimation();
                state_manager._animations.closeStepsGridAnimation();
            }
        );

        seq.append(0.2f);
        seq.append(
            () => {
                state_manager._animations.cameraZoomInLeft();
            }
        );
        seq.append(
            () => {
                //change the button image here
                if(state_manager._gameplay_loop < state_manager._gameplay_number_of_steps)
                {
                    state_manager._gameEvent_change_button_image_state.Raise(state_manager, state_manager._gameplay_loop);
                    state_manager._gameEvent_add_checkmark.Raise(state_manager, state_manager._gameplay_loop);

                    state_manager._gameplay_loop += 1;

                }
                if(state_manager._gameplay_loop == state_manager._gameplay_number_of_steps)
                {
                    state_manager._level_complete = true;
                }
                else
                {
                    //change placement of the higlight but not when on the last loop
                    state_manager._gameEvent_focus_highlight_step_state.Raise(state_manager, state_manager._gameplay_loop);
                }
            }
        );
        //seq.append(float) <--- accepts a float for delay in seccondss
        seq.append(0.5f);
        seq.append(
            () => {
                state_manager._animations.openButtonAnimation();
            }       
        );
        
        // transitionToNextState(state_manager);
    }

    public override void transitionToNextState(StateManagerScript state_manager)
    {
        state_manager.switchState(state_manager._fight_bacteria_state);
    }
}
