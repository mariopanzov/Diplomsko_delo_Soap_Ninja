using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteState<T> : BaseState<T> where T : GameplayStateManagerScript
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
                state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "closePopupAnimation", 0);
            }
        );
        seq.append(
            () => {
                state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "cameraZoomOut", 0);
            }
        );

        seq.append(0.8f);
        seq.append(
            () => {
                state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "openStepsGridAnimation", 0);
            }
        );

        seq.append(
            () => {
                state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "resetPopupAnimation", 0);
            }
        );

        //time to wait between states...
        seq.append(1.2f);
        seq.append(
            () => {
                state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "checkmarkAnimation", state_manager._gameplay_loop - 1);
            }
        );
        seq.append(1.2f);

        seq.append(
            () => {
                state_manager._change_state_trigger = true;
            }
        );


        if(state_manager.gameSetup.levelSetup[state_manager.gameSetup._picked_level]._level_completed)
        {   
            seq.append(
                () => {
                  state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "closeStepsGridAnimation", 0);
                }
            );
            seq.append(
                () => {
                    state_manager.gameSetup._gameEvent_particles.Raise(state_manager, "enableWaveParticles", 0);
                    
                }
            );
            seq.append(1.2f);
            seq.append(
                () => {
                    state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "waveAnimation", 0);
                    state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "changeNinjaAnimation", "show");
                }
            );
            seq.append(1.2f);
            seq.append(
                () => {
                    state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "gameplayBacteriaWashAwayAnimation", 0);
                }
            );
            seq.append(1.2f);
            seq.append(
                () => {
                    state_manager.gameSetup._gameEvent_particles.Raise(state_manager, "disableWaveParticles", 0);
                    state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "changeNinjaAnimation", "");
                }
            );
            seq.append(2.8f);
            seq.append(
                () => {
                    //explicit scene name...
                    state_manager.gameSetup._gameEvent_change_scene.Raise(state_manager, "loadLevelSelectScene", 0);
                }
            );
        }
        else
        {

            seq.append(
                () => {                    
                    state_manager.gameSetup._gameEvent_change_state.Raise(state_manager, "", 0);
                }
            );

        }
    }

    public override void transitionToNextState(T state_manager)
    {
        if(!state_manager.gameSetup.levelSetup[state_manager.gameSetup._picked_level]._level_completed)
        {
            state_manager.switchStates(state_manager._buttons_state);
        }
    }
}
