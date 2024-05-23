using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBattlePopupState<T> : BaseState<T> where T : LevelSelectStateManagerScript
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
                state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "hideButtonHighlightAnimation", 0);
                state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "closeButtonAnimation", 0);
            }
        );
        
        if(state_manager._step_loop == 3)
        {
            seq.append(
                () => {
                    state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "openStepsGridAnimation", 0);
                }
            );

            seq.append(1.2f);

            seq.append(
                () => {
                    state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "checkmarkAnimation", state_manager._image_index);
                }
            );
            seq.append(1.2f);

            seq.append(
                () => {
                    state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "closeStepsGridAnimation", 0);
                }
            );
            
            seq.append(1.2f);
            seq.append(
                () => {
                    state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "openPopupAnimation", 0);
                }
            );

            seq.append(6f);

            seq.append(
                () => {
                    state_manager.gameSetup.resetGameSetup();
                    //state_manager.gameSetup._gameEvent_change_scene.Raise(state_manager, "loadLevelSelectScene", 0);
                }
            );
        }
        else
        {
            seq.append(
                () => {
                    state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "openPopupAnimation", 0);
                }
            );

            seq.append(1.2f);
            seq.append(
                () => {
                    if(state_manager._step_loop == 0)
                    {
                        state_manager.gameSetup._gameEvent_toggle_touch_settings.Raise(state_manager, "ParticleChoice", "water");
                    }
                    else
                    {
                        state_manager.gameSetup._gameEvent_toggle_touch_settings.Raise(state_manager, "ParticleChoice", "paper");
                    }
                    state_manager.gameSetup._gameEvent_toggle_touch_settings.Raise(state_manager, "TouchHold", "enable");
                    state_manager._step_loop += 1;
                    state_manager._image_index += 1;
                }
            );
        }
    }

    public override void transitionToNextState(T state_manager)
    {
        state_manager.switchStates(state_manager._end_battle_grid_state);
    }
}