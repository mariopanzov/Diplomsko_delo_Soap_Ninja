using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
//using UnityEngine.ResourceManagement.AsyncOperations;


[CreateAssetMenu]
public class GameplaySceneSetup : ScriptableObject
{
    public int level_pick = 0;
    [SerializeField] public GameplaySceneLevelSetup[] levelSetup;
}

[System.Serializable]
public class GameplaySceneLevelSetup
{
    public AssetReferenceGameObject _bacteria_assetReferenceGameObject;
    //public GameObject Bacteria;
    public string[] handWashingStepButton;
    //public AssetReferenceGameObject[] assetReferenceGameObject_HandWashingStepButton;

    ~GameplaySceneLevelSetup(){}
}