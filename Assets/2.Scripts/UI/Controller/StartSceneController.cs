using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneController : MonoBehaviour
{
    [SerializeField] TutorialUIController tutorialUIController;
    public void FirstLobbyClick()
    {
        if (PlayerDataManager.instance.m_playerData.tutorial)
        {
            CameraManager.Instance.m_followEnabled = false;
            GameSceneManager.Instance.PopupClear();
            MiniGameManager.Instance.GameReset();
            Time.timeScale = 1.0f;
            GameSceneManager.Instance.SceneSelect(SCENES.LobbyScene);
            CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
        }
        else
        {
            Instantiate(tutorialUIController);
            CameraManager.Instance.m_followEnabled = false;
            GameSceneManager.Instance.PopupClear();
            MiniGameManager.Instance.GameReset();
            Time.timeScale = 1.0f;
            GameSceneManager.Instance.SceneSelect(SCENES.LobbyScene);
            CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
        }
        
    }
}
