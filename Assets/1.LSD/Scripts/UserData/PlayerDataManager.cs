using System;
using System.IO;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager instance {get; private set;}
    public PlayerData m_playerData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance != this)
                Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (File.Exists("playerData.json") == false)
        {
            //파일이 없을 경우 파일 생성 및 초기화
            SaveJson();
            FirstSave();
        }
        else
        {
            // 게임을 처음 시작할 때 Json 파일 데이터 불러오기
            LoadJson();
        }
        SaveJson(); // 초기화를 위해 Json 파일 세이브 (필요 없어도 됨)
    }
    /* 
        다른 곳에서 활용 예시 : 게임이 끝나고 결과창
        PlayerData m_playerData < 편하게 하고싶다면 선언 필요

        게임 결과 로직
        if (현재 스코어 > m_playerData.rankingPoint[게임 번호])
        {
            m_playerData.rankingPoint[게임 번호] = 현재 스코어;
        }
        m_playerData.exe += 로직 결과;
        m_playerData.coin += 로직 결과;

        if(m_playerData.exe >= 레벨업 관련 함수[m_playerData.level])
        {
            m_playerData.exe -= 레벨업 관련 함수[m_playerData.level];
            m_playerData.level++;
        }
        ★ PlayerDataManager.instance.SaveJson(); 이후 로직 마지막에 Json 세이브
        ★ PlayerDataManager.instance.LoadJson(); 초기화를 위한 Json 로드 (필요 없어도 됨)
        게임 결과창 표시 로직
    */

    // 현재 Json 파일을 저장하고 싶다면 아래 메소드를 호출
    void SaveJson()
    {   
        // JSON 형태로 포멧팅
        string jsonData = JsonUtility.ToJson(m_playerData,true);
        // 데이터를 저장할 경로
        string path = Path.Combine(Application.dataPath, "Data", "playerData.json");
        // 파일 생성 및 저장
        File.WriteAllText(path, jsonData);
    }

    // 저장 되어있는 Json 파일을 불러오고 싶다면 아래 메소드를 호출
    void LoadJson()
    {
        // 데이터를 불러올 경로 지정
        string path = Path.Combine(Application.dataPath, "Data", "playerData.json");
        // 파일의 텍스트를 string으로 저장
        string jsonData = File.ReadAllText(path);
        // 이 Json데이터를 역직렬화하여 playerData에 넣어줌
        m_playerData = JsonUtility.FromJson<PlayerData>(jsonData);
    }

    // 세이브 파일이 없을 시 초기화 설정 값
    void FirstSave()
    {
        // name은 게임 시작시 닉네임 실정에 따라 변경하게 만들 예정
        m_playerData.name = "name";
        m_playerData.level = 1;
        m_playerData.exe = 0;
        m_playerData.coin = 0;
        m_playerData.haveGamesIndex = new bool[3] {true,true,true};
        m_playerData.rankingPoint = new int[3] {0,0,0};
    }
}

[Serializable]
public class PlayerData // Json으로 파일을 Load 하거나 Save 할 때의 데이터 
{
    // public int id; 고유 id 코드를 불러오는 것인데 아직 필요한지 모르겠음
    public string name; // 플레이어 이름
    public int level; // 플레이어 현재 레벨
    public float exe; // 플레이어 현재 경험치 량
    public int coin; // 플레이어가 가지고 있는 코인 재화

    // 미니게임을 가지고 있는지 없는지 판단 false은 없고 true은 가지고 있는걸로
    public bool[] haveGamesIndex;

    // haveGamesIndex와 인덱스가 동일하게, 점수를 기록
    public int[] rankingPoint;
}
