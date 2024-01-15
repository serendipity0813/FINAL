using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private LobbySceneHandler lobbySceneHandler;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        lobbySceneHandler = GetComponent<LobbySceneHandler>();
    }

    public void SceneSelect(SCENES scene)
    {
        lobbySceneHandler.ChangeScene(scene);
    }

}
