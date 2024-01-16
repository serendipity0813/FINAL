using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private SceneHandler sceneHandler;

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

        sceneHandler = GetComponent<SceneHandler>();
    }

    public void SceneSelect(SCENES scene)
    {
        sceneHandler.ChangeScene(scene);
    }

    public void PopUpSelect(SCENES scene)
    {
        sceneHandler.PopUpChange(scene);
    }

}
