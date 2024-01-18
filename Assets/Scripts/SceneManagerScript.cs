using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] private GameplaySceneSetup gameplaySceneSetup;

    [SerializeField] private Transform[] _enemy_positions;
    [SerializeField] private Transform _steps_grid_transform;

    AssetReferenceGameObject _bacteria;
    private string[] _steps;

    [System.NonSerialized] public GameObject _instanceReference;


    private void Awake()
    {
        if(SceneManager.GetActiveScene().name == "GameplayScene")
        {
            if(gameplaySceneSetup.levelSetup[gameplaySceneSetup._level_pick]._bacteria_assetReferenceGameObject != null)
            {
                _bacteria = gameplaySceneSetup.levelSetup[gameplaySceneSetup._level_pick]._bacteria_assetReferenceGameObject;
                _steps = (string[]) gameplaySceneSetup.levelSetup[gameplaySceneSetup._level_pick]._hand_washing_steps.Clone();
                
                loadGameplaySceneObjects();
            }
        }
    }

    public void loadScene(Component sender, object data)
    {
        switch ((string)data)
        {
            case "GameplayScene":
           
                SceneManager.LoadScene("GameplayScene");
            
            break;

            case "LevelSelectScene":
           
                // release instantiated objects
                if(_instanceReference != null && _bacteria != null)
                {
                    _bacteria.ReleaseInstance(_instanceReference);
                }

                SceneManager.LoadScene("LevelSelectScene");
                
            break;
        }
        
    }

    private void loadGameplaySceneObjects()
    {
        //Load Bacteria
        _bacteria.InstantiateAsync().Completed += OnAdressableInstantiated;     
        //lOAD bUTTONS
        // Kopcinjata samo kako hidden objects       
        for(int i = 0; i < _steps.Length; i++)
        {
            foreach(Transform child in _steps_grid_transform)
            {
                if(child.name == _steps[i])
                {
                    child.gameObject.SetActive(true);
                }
            }
        }

    }

    private void OnAdressableInstantiated(AsyncOperationHandle<GameObject> handle)
    {
        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            _instanceReference = handle.Result;

            if(_enemy_positions[gameplaySceneSetup._level_pick] != null)
            {
                _instanceReference.transform.parent = _enemy_positions[gameplaySceneSetup._level_pick];
                _instanceReference.transform.position = _enemy_positions[gameplaySceneSetup._level_pick].position;
                _instanceReference.transform.rotation = _enemy_positions[gameplaySceneSetup._level_pick].rotation;
            }
        }
    }

    public void pickLevel_PrepareGameplayScene(Component sender, object data)
    {
        switch ((string)data)
        {
            case "BACTERIA_1":
            gameplaySceneSetup._level_pick = 0;
            break;
            case "BACTERIA_2":
            gameplaySceneSetup._level_pick = 1;
            break;
            case "BACTERIA_3":
            gameplaySceneSetup._level_pick = 2;
            break;
        }
    }
}
