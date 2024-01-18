using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameChangeSceneController : ButtonHandler
{
    private void Start()
    {
        Invoke("GoToGameScene", 1);
    }

    private void GoToGameScene()
    {
        GameSceneManager.Instance.SceneSelect(SCENES.InGameScene);
        MiniGameManager.Instance.RandomGameStart();
    }

}
