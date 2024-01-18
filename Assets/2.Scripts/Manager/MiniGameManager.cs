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


    public void RandomGameStart()
    {
        int random = Random.Range(0, MiniGames.Length);
        m_currentGame = Instantiate(MiniGames[random]);

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

    // 게임 클리어시 스테이지 변수를 1 올리고 게임 선택 씬으로 이동하면서 현재 게임 파괴
    public void GameClear()
    {
        Stage++;
        GameSceneManager.Instance.SceneSelect(SCENES.GameChangeScene);
        Destroy(m_currentGame);
    }

    //게임 실패시 현재 게임을 파괴하고 로비 씬으로 전환하도록 함
    public void GameFail()
    {
        Destroy(m_currentGame);
        GameSceneManager.Instance.SceneSelect(SCENES.GameOverScene);
    }

}
