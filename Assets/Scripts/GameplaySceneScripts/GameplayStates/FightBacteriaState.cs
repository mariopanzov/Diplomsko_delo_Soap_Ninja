using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBacteriaState<T> : BaseState<T> where T : GameplayStateManagerScript
{
    private LTSeq seq;

    public override void updateState(T state_manager)
    {
        seq = LeanTween.sequence();

        seq.append(
            () => {
                state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "hideButtonHighlightAnimation", 0);
                state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "closeButtonAnimation", 0);
                state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "openPopupAnimation", 0);

                // i can call the enable soap here but eh i dont think it matters
            }
        );
        seq.append(0.6f);
        seq.append(
            () => {
                state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "cameraPanLeftToRight", 0);
            }
        );
        seq.append(0.2f);
        seq.append(
            () => {
                state_manager.gameSetup._gameEvent_animations.Raise(state_manager, "movePopupLeftAnimation", 0);
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
        state_manager.switchStates(state_manager._level_complete_state);
    }
}
