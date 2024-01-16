using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneController : MonoBehaviour
{
    public void ChoiceModeClick()
    {
        UIManager.Instance.SceneSelect(SCENES.SelectScene);
    }

    public void RandomModeClick()
    {
        UIManager.Instance.SceneSelect(SCENES.GameChangeScene);
    }

    public void GatchaClick()
    {
        UIManager.Instance.SceneSelect(SCENES.GatchaScene);
    }

    public void OptionClick()
    {
        UIManager.Instance.PopUpSelect(SCENES.OptionScene);
    }

    public void ShopClick()
    {
       UIManager.Instance.PopUpSelect(SCENES.ShopScene);
    }

    public void RankClick()
    {
        UIManager.Instance.PopUpSelect(SCENES.RankScene);
    }
}
