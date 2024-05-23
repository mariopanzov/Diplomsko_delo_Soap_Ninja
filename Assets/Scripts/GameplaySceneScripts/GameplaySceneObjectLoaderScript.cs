using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class GameplaySceneObjectManager : MonoBehaviour
{
    [SerializeField] private GameSetup gameSetup;
    [SerializeField] private Transform[] _enemy_positions;

    private AssetReferenceGameObject _bacteria;

    [System.NonSerialized] public GameObject _instanceReference;

    // Start is called before the first frame update
    void Awake()
    {

        if(gameSetup.levelSetup[gameSetup._picked_level]._bacteria_assetReferenceGameObject != null)
        {
            _bacteria = gameSetup.levelSetup[gameSetup._picked_level]._bacteria_assetReferenceGameObject;            
            loadGameplaySceneObjects();
        }
    }

    void OnDisable()
    {
        // release instantiated objects
        if(_instanceReference != null && _bacteria != null)
        {
            _bacteria.ReleaseInstance(_instanceReference);
        }
    }
    
    private void loadGameplaySceneObjects()
    {
        //Load Bacteria
        _bacteria.InstantiateAsync().Completed += OnAdressableInstantiated;     
    }

    private void OnAdressableInstantiated(AsyncOperationHandle<GameObject> handle)
    {
        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            _instanceReference = handle.Result;

            if(_enemy_positions[gameSetup._picked_level] != null)
            {
                _instanceReference.transform.parent = _enemy_positions[gameSetup._picked_level];
                _instanceReference.transform.position = _enemy_positions[gameSetup._picked_level].position;
                _instanceReference.transform.rotation = _enemy_positions[gameSetup._picked_level].rotation;
            }
        }
    }
}
