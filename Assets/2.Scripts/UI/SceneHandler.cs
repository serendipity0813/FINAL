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
    InGameScene,
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
    private GameObject m_PopupUI;

    private int m_popupNumber;
    private int m_sceneNumber;

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
        m_sceneNumber = (int)scene;
        m_choiceScene = Scenes[m_sceneNumber];
        Destroy(m_scenePrefab);
        m_scenePrefab = Instantiate(m_choiceScene);
        m_currentScene = m_choiceScene;
        ChangeSceneObject(m_sceneNumber);
    }

    private void ChangeSceneObject(int sceneNumber)
    {
        Destroy(m_sceneObject);

        if (sceneNumber == (int)SCENES.LobbyScene)
        {
            m_sceneObject = Instantiate(LobbyObject);
        }
        else if (sceneNumber == (int)SCENES.SelectScene)
        {
            m_sceneObject = Instantiate(SelectObject);
        }
    }

    public void PopUpChange(SCENES scene)
    {
        if(m_popupNumber == 0)
        {
            m_popupNumber = (int)scene;
            m_PopupUI = Instantiate(Scenes[m_popupNumber]);
        }
        else if(m_popupNumber == (int)scene)
        {
            Destroy(m_PopupUI);
            m_popupNumber = 0;
        }
        else
        {
            Destroy(m_PopupUI);
            m_PopupUI = Instantiate(Scenes[m_popupNumber]);
        }

    }

    public void PopUpDown()
    {
        Destroy(m_PopupUI);
    }


}
