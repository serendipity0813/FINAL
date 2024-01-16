using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSceneController : MonoBehaviour
{
    public void LobbyClick()
    {
        GameSceneManager.Instance.SceneSelect(SCENES.LobbyScene);
    }

    public void OptionClick()
    {
        GameSceneManager.Instance.PopUpSelect(SCENES.OptionScene);
    }

    public void GatchaClick()
    {
        GameSceneManager.Instance.SceneSelect(SCENES.GatchaScene);
    }


}
