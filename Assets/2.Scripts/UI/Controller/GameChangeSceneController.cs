using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameChangeSceneController : ButtonHandler
{
    private void Start()
    {
        if (MiniGameManager.Instance.GameName == "Random")
            Invoke("StartRandomGame", 1);
        else
            Invoke("StartChoiceGame", 1);

    }

    private void StartRandomGame()
    {
        GameSceneManager.Instance.SceneSelect(SCENES.InGameScene);
        MiniGameManager.Instance.RandomGameStart();
    }

    private void StartChoiceGame()
    {
        GameSceneManager.Instance.SceneSelect(SCENES.InGameScene);
        MiniGameManager.Instance.ChoiceGameStart();
    }

}
