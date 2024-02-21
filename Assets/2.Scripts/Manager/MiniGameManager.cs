using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;
    public MiniGameDataSO MiniGames;
    public GameObject[] InGameUIs;
    private GameObject m_currentGame;
    private Dictionary<string, int> m_gameDictionary = new Dictionary<string, int>();
    private int m_beforeGame;
    public bool m_endCheck;
    public int m_clearCheck = 0;
    public int GameNumber { get; set; }
    public bool RandomMod = false;
    public bool m_isWin;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Start()
    {
        //딕셔너리에 게임이름을 키값, 인스펙터창에 저장된 순번을 벨류값으로 저장
        for (int i = 1; i < MiniGames.games.Count; i++)
        {
            m_gameDictionary.Add(MiniGames.games[i].gameName, i);
        }
        m_beforeGame = -100;
    }

    //랜덤게임 진행시 불러오는 메소드
    public void RandomGameStart()
    {
        m_endCheck = false;
        m_clearCheck = 0;
        RandomMod = true;
        int random = RandomGameCheck();

        m_currentGame = Instantiate(MiniGames.games[random].gamePrefab);
        m_beforeGame = random;

        //바로 이전 게임은 등장하지 않거나 이전에 진행한 게임은 제외시키거나 하는 등의 로직 코드구현 필요
    }
    private int RandomGameCheck()
    {
        int random;
        while (true)
        {
            random = Random.Range(1, MiniGames.games.Count);

            if (PlayerDataManager.instance.m_playerData.haveGames[random])
            {
                if (random != m_beforeGame)  //게임이 중복으로 나오는 걸 막기 위한 코드
                {
                    return random;
                }
            }
        }
    }

    //선택게임 진행시 불러오는 메소드
    public void ChoiceGameStart()
    {
        m_endCheck = false;
        m_clearCheck = 0;
        if(GameNumber == 0)
        {
            RandomMod = true;
            int random = RandomGameCheck();

            m_currentGame = Instantiate(MiniGames.games[random].gamePrefab);
            m_beforeGame = random;
        }
        else
        {
            m_currentGame = Instantiate(MiniGames.games[GameNumber].gamePrefab);
            TutorialCheck();
        }

    }

    // 게임 클리어시 스테이지 변수를 1 올리고 게임 선택 씬으로 이동하면서 현재 게임 파괴
    public void GameClear()
    {
        //게임 클리어시 변수 재설정 및 스테이지 상승
        if (m_endCheck)
        {
            m_endCheck = true;
            m_clearCheck = 1;
            PlayerDataManager.instance.m_playerData.bonusPointIndex++;
            PlayerDataManager.instance.m_playerData.bonusPoint += PlayerDataManager.instance.m_playerData.bonusPointIndex * 10;
            PlayerDataManager.instance.m_playerData.stage++;
            GameSceneManager.Instance.SceneSelect(SCENES.GameChangeScene);
            TutorialCheck();
            Destroy(m_currentGame);
        }
    }

    //게임 실패시 현재 게임을 파괴하고 로비 씬으로 전환하도록 함
    public void GameFail()
    {
        if (m_endCheck)
        {
            m_endCheck = true;
            m_clearCheck = -1;
            PlayerDataManager.instance.m_playerData.bonusPoint += PlayerDataManager.instance.m_playerData.bonusPointIndex * 10;
            PlayerDataManager.instance.m_playerData.bonusPointIndex = 0;
            Destroy(m_currentGame);

            if (PlayerDataManager.instance.m_playerData.life >= 1)
            {
                PlayerDataManager.instance.m_playerData.life--;
                TutorialCheck();
                // 체력이 감소하고 나서 0일시 게임 종료
                if (PlayerDataManager.instance.m_playerData.life <= 0)
                {
                  
                    GameSave();
                    GameSceneManager.Instance.SceneSelect(SCENES.GameOverScene);
                    return; // 게임 끝
                }
                //체력이 감소하지 않았다면
                GameSceneManager.Instance.SceneSelect(SCENES.GameChangeScene);
            }
        }
    }



    // 코드가 길어져서 가독성을 위해 메소드로 변환
    private void GameSave()
    {
        //플레이어 리워드 계산 코드
        PlayerDataManager.instance.MinigameRewardCheck();

        // 랭킹 저장 관련 랜덤게임 일시
        if (RandomMod)
        {
            if (PlayerDataManager.instance.m_playerData.rankingPoint[0] < PlayerDataManager.instance.m_playerData.stage)
            {
                PlayerDataManager.instance.m_playerData.rankingPoint[0] = PlayerDataManager.instance.m_playerData.stage;
            }
            RandomMod = false;
        }
        // 랭킹 저장 관련 선택게임 일시
        else
        {
            if (PlayerDataManager.instance.m_playerData.rankingPoint[GameNumber] < PlayerDataManager.instance.m_playerData.stage)
            {
                PlayerDataManager.instance.m_playerData.rankingPoint[GameNumber] = PlayerDataManager.instance.m_playerData.stage;
            }
        }
        PlayerDataManager.instance.SaveJson();
    }

    //게임도중 나가거나 종료 후 초기화를 위한 메소드
    public void GameReset()
    {
        if (m_currentGame != null)
        {
            Destroy(m_currentGame);
        }
        m_clearCheck = 0;
        PlayerDataManager.instance.m_playerData.stage = 0;
        PlayerDataManager.instance.m_playerData.life = 3;
        PlayerDataManager.instance.m_playerData.rewardExp = 0;
        PlayerDataManager.instance.m_playerData.rewardCoin = 0;
        PlayerDataManager.instance.m_playerData.timePoint = 0;
        PlayerDataManager.instance.m_playerData.bonusPointIndex = 0;
        PlayerDataManager.instance.m_playerData.bonusPoint = 0;
    }

    public void EndCheck()
    {
        if (!m_endCheck)
        {
            m_endCheck = true;
            if (m_isWin)
            {
                GameClear();
            }
            else
            {
                GameFail();
            }
        }
    }
    private void TutorialCheck()
    {
        if (!PlayerDataManager.instance.m_playerData.tutorial)
        {
            TutorialUIController tutorialUIController = GameObject.Find("TutorialUI(Clone)").GetComponent<TutorialUIController>();
            tutorialUIController.SwitchButtonClick();

            if (PlayerDataManager.instance.m_playerData.life <= 2 && tutorialUIController.m_tutorialIndex == 12)
            {
                PlayerDataManager.instance.m_playerData.life++;
                m_endCheck = true;
                if (m_endCheck)
                {
                    m_endCheck = true;
                    m_clearCheck = 1;
                    PlayerDataManager.instance.m_playerData.stage++;
                    GameSceneManager.Instance.SceneSelect(SCENES.GameChangeScene);
                    Destroy(m_currentGame);
                }
                return;
            }
            else if (PlayerDataManager.instance.m_playerData.life <= 3 && tutorialUIController.m_tutorialIndex == 12)
            {
                tutorialUIController.m_tutorialIndex++;
                tutorialUIController.SwitchButtonClick();
                return;
            }
        }
    }
}
