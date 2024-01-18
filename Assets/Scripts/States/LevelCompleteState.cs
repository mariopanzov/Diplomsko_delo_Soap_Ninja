using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelCompleteState : BaseState
{
    private LTSeq seq;

    public LevelCompleteState()
    {
    }

    public override void onEnterState()
    {

    }

    public override void updateState(StateManagerScript state_manager)
    {
        seq = LeanTween.sequence();
        
        seq.append(
            () => {
                state_manager._animations.closePopupAnimation();
            }
        );
        seq.append(
            () => {
                state_manager._animations.cameraZoomOut();
            }
        );

        seq.append(0.8f);
        seq.append(
            () => {
                state_manager._animations.openStepsGridAnimation();
            }
        );

        seq.append(
            () => {
                state_manager._animations.resetPopupAnimation();
            }
        );

        //time to wait between states...
        seq.append(1.2f);
        seq.append(
            () => {
                state_manager._animations.checkmarkAnimation(state_manager._gameplay_loop - 1);
            }
        );
        seq.append(1.2f);


        if(state_manager._level_complete)
        {   
            seq.append(
                () => {
                  state_manager._animations.closeStepsGridAnimation();
                }
            );
            seq.append(
                () => {
                    state_manager._particles.enable_wave_particles();
                }
            );
            seq.append(1.2f);
            seq.append(
                () => {
                    state_manager._animations.waveAnimation();
                }
            );
            seq.append(4.4f);
            seq.append(
                () => {
                    //explicit scene name...
                    state_manager._gameEvent_exit.Raise(state_manager, "LevelSelectScene");
                }
            );
        }
        else
        {
            seq.append(
                () => {
                    // transitionToNextState(state_manager);
                    state_manager._gameEvent_change_state.Raise(state_manager, 0);
                }
            );

        }
    }

    public override void transitionToNextState(StateManagerScript state_manager)
    {
        state_manager.switchState(state_manager._buttons_state);
    }
}
