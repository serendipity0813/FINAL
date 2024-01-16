using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatchaSceneController : MonoBehaviour
{
    public void LobbyClick()
    {
        UIManager.Instance.SceneSelect(SCENES.LobbyScene);
    }

    public void OptionClick()
    {
        UIManager.Instance.PopUpSelect(SCENES.OptionScene);
    }

    public void AdClick()
    {
        Debug.Log("광고보기");
    }


}
