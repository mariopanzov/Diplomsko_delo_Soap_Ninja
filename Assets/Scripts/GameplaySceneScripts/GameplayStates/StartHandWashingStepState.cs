using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartHandWashingStepState<T> : BaseState<T> where T : GameplayStateManagerScript
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
            }
        );

        seq.append(0.2f);
        seq.append(
            () => {
                state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "gameplayCameraZoomInLeft", 0);
            }
        );
                
        seq.append(
            () => {
                //change the button image here
                if(state_manager._gameplay_loop < state_manager._gameplay_number_of_steps)
                {
                    state_manager.gameSetup._gameEvent_change_button_image_state.Raise(state_manager, "", state_manager._gameplay_loop);
                    state_manager.gameSetup._gameEvent_add_checkmark.Raise(state_manager, "", state_manager._gameplay_loop);

                    state_manager._gameplay_loop += 1;

                }
                if(state_manager._gameplay_loop == state_manager._gameplay_number_of_steps)
                {
                    //the level of the chosen bacteria is completed and saved to the gameSetup script
                    state_manager.gameSetup.completeLevel(state_manager.gameSetup._picked_level);
                }
                else
                {
                    //change placement of the higlight but not when on the last loop
                    state_manager.gameSetup._gameEvent_focus_highlight_step_state.Raise(state_manager, "", state_manager._gameplay_loop);
                }
            }
        );
        //seq.append(float) <--- accepts a float for delay in seccondss
        
        seq.append(0.5f);
        seq.append(
            () => {
                // state_manager._animations.openButtonAnimation();
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
        seq.append(
            () => {
                state_manager._change_state_trigger = true;
            }
        );
            
    }
    
    public override void transitionToNextState(T state_manager)
    {
        state_manager.switchStates(state_manager._fight_bacteria_state);
    }
}
