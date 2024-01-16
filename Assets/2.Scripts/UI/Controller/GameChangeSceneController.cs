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
        UIManager.Instance.SceneSelect(SCENES.LobbyScene);
    }

    public void OptionClick()
    {
        UIManager.Instance.PopUpSelect(SCENES.OptionScene);
    }

    private void GoToGameScene()
    {
        UIManager.Instance.SceneSelect(SCENES.InGameScene);
        MiniGameManager.Instance.GameStart();
    }

}
