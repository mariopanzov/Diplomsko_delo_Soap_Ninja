using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectSceneObjectManager : MonoBehaviour
{
    public GameSetup gameSetup;
    
    void Awake()
    {
        for(int i = 0; i < gameSetup.levelSetup.Length; i++)
        {
            if(gameSetup.levelSetup[i]._level_completed == true)
            {   
                gameSetup._gameEvent_animations.Raise(this, "defeatedBacteriaAnimation", i);
            }
        }
    }
}
