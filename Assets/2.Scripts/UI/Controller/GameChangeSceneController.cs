using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameChangeSceneController : MonoBehaviour
{
    private void Start()
    {
        Invoke("GoToGameScene", 1);
    }

    public void LobbyClick()
    {
        GameSceneManager.Instance.SceneSelect(SCENES.LobbyScene);
    }

    public void OptionClick()
    {
        GameSceneManager.Instance.PopUpSelect(SCENES.OptionScene);
    }

    private void GoToGameScene()
    {
        GameSceneManager.Instance.SceneSelect(SCENES.InGameScene);
        MiniGameManager.Instance.GameStart();
    }

}
