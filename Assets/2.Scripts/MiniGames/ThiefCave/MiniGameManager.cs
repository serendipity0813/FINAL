using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;
    public GameObject[] MiniGames;
    private GameObject m_currentGame;
    public int Stage {get; private set;}
    private int m_beforeStageNumber;
    private bool m_gameFlag;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }


    public void GameStart()
    {
        m_currentGame = Instantiate(MiniGames[0]);

        //if(Stage == 0)
        //{
        //    Stage++;
        //    m_beforeStageNumber = -1;
        //}

        //while(m_gameFlag) 
        //{
        //    int random = Random.Range(0, MiniGames.Length);
        //    if(random != m_beforeStageNumber )
        //    {
        //        m_currentGame = Instantiate(MiniGames[random]);
        //        m_gameFlag = false;
        //    }
        //}

    }

    public void GameClear()
    {
        Stage++;
        GameSceneManager.Instance.SceneSelect(SCENES.GameChangeScene);
        Destroy(m_currentGame);
    }

    public void GameFail()
    {
        Destroy(m_currentGame);
        GameSceneManager.Instance.SceneSelect(SCENES.LobbyScene);
    }

}
