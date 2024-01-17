using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatchaSceneController : MonoBehaviour
{
    public void LobbyClick()
    {
        GameSceneManager.Instance.SceneSelect(SCENES.LobbyScene);
    }

    public void OptionClick()
    {
        GameSceneManager.Instance.PopUpSelect(SCENES.OptionScene);
    }

    public void AdClick()
    {
        Debug.Log("광고보기");
    }


}
