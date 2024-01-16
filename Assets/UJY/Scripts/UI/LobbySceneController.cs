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
        UIManager.Instance.SceneSelect(SCENES.OptionScene);
    }

    public void ShopClick()
    {
       UIManager.Instance.SceneSelect(SCENES.ShopScene);
    }

    public void RankClick()
    {
        UIManager.Instance.SceneSelect(SCENES.RankScene);
    }
}
