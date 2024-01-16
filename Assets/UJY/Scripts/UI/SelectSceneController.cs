using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSceneController : MonoBehaviour
{
    public void LobbyClick()
    {
        UIManager.Instance.SceneSelect(SCENES.LobbyScene);
    }

    public void OptionClick()
    {
        UIManager.Instance.PopUpSelect(SCENES.OptionScene);
    }

    public void GatchaClick()
    {
        UIManager.Instance.SceneSelect(SCENES.GatchaScene);
    }


}
