using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBacteriaState : BaseState
{
    private LTSeq seq;

    public FightBacteriaState()
    {
        //seq = LeanTween.sequence();
    }

    public override void onEnterState()
    {

    }

    public override void updateState(StateManagerScript state_manager)
    {
        seq = LeanTween.sequence();

        seq.append(
            () => {
                state_manager._animations.closeButtonAnimation();
                state_manager._animations.openPopupAnimation();
            }
        );
        seq.append(1f);
        seq.append(
            () => {
                state_manager._animations.cameraPanLeftToRight();
            }
        );
        seq.append(0.2f);
        seq.append(
            () => {
                state_manager._animations.movePopupLeftAnimation();
            }
        );

        // transitionToNextState(state_manager);
    }

    public override void transitionToNextState(StateManagerScript state_manager)
    {
        state_manager.switchState(state_manager._level_complete_state);
    }
}
