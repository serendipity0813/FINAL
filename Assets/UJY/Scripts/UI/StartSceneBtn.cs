using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneBtn : MonoBehaviour
{
    public void OnClick()
    {
        UIManager.Instance.SceneSelect(SCENES.LobbyScene);
    }

}
