using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneController : MonoBehaviour
{
    public void OnClick()
    {
        GameSceneManager.Instance.SceneSelect(SCENES.LobbyScene);
    }

}
