using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
//using UnityEngine.ResourceManagement.AsyncOperations;


[CreateAssetMenu]
public class GameplaySceneSetup : ScriptableObject
{
    public int _level_pick = 0;
    public GameplaySceneLevelSetup[] levelSetup;
}

[System.Serializable]
public class GameplaySceneLevelSetup
{
    public AssetReferenceGameObject _bacteria_assetReferenceGameObject;
    //[Range(1,6)] public int _nubmer_of_steps;
    public string[] _hand_washing_steps;

    ~GameplaySceneLevelSetup(){}
}