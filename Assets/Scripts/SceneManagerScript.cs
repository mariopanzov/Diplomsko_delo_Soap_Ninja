
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] private GameplaySceneSetup gameplaySceneSetup;

    [SerializeField] private Transform bacteria_transform;
    [SerializeField] private Transform button_grid_transform;


    private void Awake()
    {
        if(SceneManager.GetActiveScene().name == "GameplayScene")
        {
            loadGameplaySceneObjects();
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
            SceneManager.LoadScene("LevelSelectScene");
            break;
            case "LoadingScene":
            SceneManager.LoadScene("LoadingScene");
            break;
        }
        
    }

    private void loadGameplaySceneObjects()
    {
        //Load Bacteria
        Instantiate<GameObject>(gameplaySceneSetup.levelSetup[gameplaySceneSetup.level_pick].Bacteria, bacteria_transform);
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
        for(int i = 0; i < gameplaySceneSetup.levelSetup[gameplaySceneSetup.level_pick].handWashingStepButton.Length; i++)
        {
            foreach(Transform child in button_grid_transform)
            {
                if(child.name == gameplaySceneSetup.levelSetup[gameplaySceneSetup.level_pick].handWashingStepButton[i])
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
