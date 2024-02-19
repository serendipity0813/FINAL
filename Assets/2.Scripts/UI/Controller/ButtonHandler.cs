using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{

    public void LobbyClick()
    {
        CameraManager.Instance.m_followEnabled = false;
        GameSceneManager.Instance.PopupClear();
        MiniGameManager.Instance.GameReset();
        Time.timeScale = 1.0f;
        GameSceneManager.Instance.SceneSelect(SCENES.LobbyScene);
        CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
        EffectSoundManager.Instance.PlayEffect(1);
    }

    public void SelectModeClick()
    {
        GameSceneManager.Instance.PopupClear();
        GameSceneManager.Instance.SceneSelect(SCENES.SelectScene);
        CameraManager.Instance.ChangeCamera(CameraView.Angle90View);
        EffectSoundManager.Instance.PlayEffect(1);
    }

    public void RandomModeClick()
    {
        MiniGameManager.Instance.GameNumber = 0;
        GameSceneManager.Instance.PopUpSelect(SCENES.GameChoiceScene);
        CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
        EffectSoundManager.Instance.PlayEffect(1);
    }

    public void GatchaClick()
    {
        GameSceneManager.Instance.PopupClear();
        GameSceneManager.Instance.SceneSelect(SCENES.GatchaScene);
        CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
        EffectSoundManager.Instance.PlayEffect(1);
    }

    public void OptionClick()
    {
        GameSceneManager.Instance.PopUpSelect(SCENES.OptionScene);
        //CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
        EffectSoundManager.Instance.PlayEffect(1);
    }

    public void ShopClick()
    {
        GameSceneManager.Instance.PopupClear();
        GameSceneManager.Instance.SceneSelect(SCENES.ShopScene);
        CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
        EffectSoundManager.Instance.PlayEffect(1);
    }

    public void ChoiceModeStartClick()
    {
        GameSceneManager.Instance.SceneSelect(SCENES.GameChangeScene);
        GameSceneManager.Instance.PopUpSelect(SCENES.GameChoiceScene);
        CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
        EffectSoundManager.Instance.PlayEffect(1);
    }

    public void ChoiceXbtnClick()
    {
        GameSceneManager.Instance.PopUpSelect(SCENES.GameChoiceScene);
        //CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
    }

    public void RestartbtnClick()
    {
        Time.timeScale = 1.0f;
        GameSceneManager.Instance.SceneSelect(SCENES.GameChangeScene);
        MiniGameManager.Instance.GameReset();
        CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
        EffectSoundManager.Instance.PlayEffect(1);
    }

}
