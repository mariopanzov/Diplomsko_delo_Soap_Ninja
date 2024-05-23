using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameSetup gameSetup;

    public void loadScene(Component sender, string function, object data)
    {
        switch (function)
        {
            case "loadGameplayScene":
                prepareGameplayScene_pickLevel(data);

                if(!gameSetup.levelSetup[gameSetup._picked_level]._level_completed)
                {
                    loadGameplayScene(data);
                }
                break;

            case "loadLevelSelectScene":
                loadLevelSelectScene(data);
                break;
        }
        
    }

    private void loadGameplayScene(object data)
    {
        SceneManager.LoadScene("GameplayScene");    
    }

    private void loadLevelSelectScene(object data)
    {
        SceneManager.LoadScene("LevelSelectScene");
    }
    private void loadFinnishScene(object data)
    {
        SceneManager.LoadScene("FinnishScene");
    }

    public void prepareGameplayScene_pickLevel(object data)
    {
        switch ((string)data)
        {
            case "BACTERIA_1":
            gameSetup._picked_level = 0;
            break;
            case "BACTERIA_2":
            gameSetup._picked_level = 1;
            break;
            case "BACTERIA_3":
            gameSetup._picked_level = 2;
            break;
        }
    }
}
