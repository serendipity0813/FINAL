using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{

    public void LobbyClick()
    {
        GameSceneManager.Instance.PopupClear();
        GameSceneManager.Instance.SceneSelect(SCENES.LobbyScene);
        CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
    }

    public void SelectModeClick()
    {
        GameSceneManager.Instance.PopupClear();
        GameSceneManager.Instance.SceneSelect(SCENES.SelectScene);
        CameraManager.Instance.ChangeCamera(CameraView.Angle90View);
    }

    public void RandomModeClick()
    {
        MiniGameManager.Instance.GameNumber = -1;
        GameSceneManager.Instance.PopUpSelect(SCENES.GameChoiceScene);
        //CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
    }

    public void GatchaClick()
    {
        GameSceneManager.Instance.PopupClear();
        GameSceneManager.Instance.SceneSelect(SCENES.GatchaScene);
        //CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
    }

    public void OptionClick()
    {
        GameSceneManager.Instance.PopUpSelect(SCENES.OptionScene);
        //CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
    }

    public void ShopClick()
    {
        GameSceneManager.Instance.PopUpSelect(SCENES.ShopScene);
        //CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
    }

    public void ChoiceModeStartClick()
    {
        GameSceneManager.Instance.SceneSelect(SCENES.GameChangeScene);
        GameSceneManager.Instance.PopUpSelect(SCENES.GameChoiceScene);
        //CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
    }

    public void ChoiceXbtnClick()
    {
        GameSceneManager.Instance.PopUpSelect(SCENES.GameChoiceScene);
        //CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
    }

}
