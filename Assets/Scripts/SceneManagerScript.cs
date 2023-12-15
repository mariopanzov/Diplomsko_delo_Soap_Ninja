using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] private GameplaySceneSetup gameplaySceneSetup;

    [SerializeField] private Transform enemy_position;
    [SerializeField] private Transform button_grid_transform;

    AssetReferenceGameObject _bacteria;
    string[] _buttons;

    GameObject _instanceReference;


    private void Awake()
    {
        if(SceneManager.GetActiveScene().name == "GameplayScene")
        {
            if(gameplaySceneSetup.levelSetup[gameplaySceneSetup.level_pick]._bacteria_assetReferenceGameObject != null)
            {
                _bacteria = gameplaySceneSetup.levelSetup[gameplaySceneSetup.level_pick]._bacteria_assetReferenceGameObject;
                _buttons = (string[]) gameplaySceneSetup.levelSetup[gameplaySceneSetup.level_pick].handWashingStepButton.Clone();
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
            // case "LoadingScene":
            // SceneManager.LoadScene("LoadingScene");
            // break;
        }
        
    }

    private void loadGameplaySceneObjects()
    {
        //Load Bacteria
        
            // how i tink ill like it
                _bacteria.InstantiateAsync().Completed += OnAdressableInstantiated;
                    
            //Load from game object
            
                //Instantiate<GameObject>(gameplaySceneSetup.levelSetup[gameplaySceneSetup.level_pick].Bacteria, bacteria_transform);
            
            //Load from assetReference (but very dense and not really needed, there is a simpler way)

                // gameplaySceneSetup.levelSetup[gameplaySceneSetup.level_pick].assetReferenceGameObject_Bacteria.LoadAssetAsync<GameObject>().Completed +=
                //     (asyncOperationHandle) => {
                //         if(asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
                //         {
                //             Instantiate(asyncOperationHandle.Result, bacteria_transform);
                //         }
                //         else
                //         {
                //             Debug.Log("Failed to load!");
                //         }
                //     };

        //lOAD bUTTONS
    
             // Kopcinjata samo kako hidden objects
                
                for(int i = 0; i < _buttons.Length; i++)
                {
                    foreach(Transform child in button_grid_transform)
                    {
                        if(child.name == _buttons[i])
                        {
                            child.gameObject.SetActive(true);
                        }
                    }
                }

            //Load Buttons
                // for(int i = 0; i < gameplaySceneSetup.levelSetup[gameplaySceneSetup.level_pick].assetReferenceGameObject_HandWashingStepButton.Length; i++)
                // {
                //     gameplaySceneSetup.levelSetup[gameplaySceneSetup.level_pick].assetReferenceGameObject_HandWashingStepButton[i].LoadAssetAsync<GameObject>().Completed +=
                //     (asyncOperationHandle) => {
                //         if(asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
                //         {
                //             Instantiate(asyncOperationHandle.Result, button_grid_transform);
                //         }
                //         else
                //         {
                //             Debug.Log("Failed to load!");
                //         }
                //     };
                // }
    }

    void OnAdressableInstantiated(AsyncOperationHandle<GameObject> handle)
    {
        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            _instanceReference = handle.Result;

            _instanceReference.transform.parent = enemy_position;
            _instanceReference.transform.position = enemy_position.position;
        }
    }

    public void pickLevel_PrepareGameplayScene(Component sender, object data)
    {
        switch ((string)data)
        {
            case "BACTERIA_1":
            gameplaySceneSetup.level_pick = 0;
            break;
            case "BACTERIA_2":
            gameplaySceneSetup.level_pick = 1;
            break;
            case "BACTERIA_3":
            gameplaySceneSetup.level_pick = 2;
            break;
        }
    }
}
