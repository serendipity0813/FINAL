using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SCENES
{
    StartScene = 0,
    LobbyScene,
    SelectScene,
    GatchaScene,
    GameChangeScene,
    OptionScene,
    RankScene,
    ShopScene
}

public class SceneHandler : MonoBehaviour
{
    public GameObject[] Scenes;
    public GameObject LobbyObject;
    public GameObject SelectObject;
    private GameObject m_scenePrefab;
    private GameObject m_currentScene;
    private GameObject m_choiceScene;
    private GameObject m_sceneObject;

    // Start is called before the first frame update
    private void Start()
    {
        if (m_currentScene == null)
        {
            m_currentScene = Scenes[0];
            m_scenePrefab = Instantiate(m_currentScene);
        }

    }

    public void ChangeScene(SCENES scene)
    {
        int sceneNumber = (int)scene;
        m_choiceScene = Scenes[sceneNumber];
        Destroy(m_scenePrefab);
        m_scenePrefab = Instantiate(m_choiceScene);
        m_currentScene = m_choiceScene;
        ChangeSceneObject(sceneNumber);
    }

    private void ChangeSceneObject(int SceneNumber)
    {
        Destroy(m_sceneObject);

        if (SceneNumber == (int)SCENES.LobbyScene)
        {
            m_sceneObject = Instantiate(LobbyObject);
        }
        else if (SceneNumber == (int)SCENES.SelectScene)
        {
            m_sceneObject = Instantiate(SelectObject);
        }
    }

    private void CameraChange(int SceneNumber)
    {

    }

}
