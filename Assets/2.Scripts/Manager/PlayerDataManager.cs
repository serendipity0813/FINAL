using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager instance { get; private set; }
    public PlayerData m_playerData;
    private string path;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        // Json 데이터 위치 불러오기
        path = Path.Combine(Application.dataPath, "Data", "playerData.json");
    }

    private void Start()
    {
        if (File.Exists(path))
        {
            // json 파일이 있다면 시작할 때 json 파일 데이터 불러오기
            LoadJson();
            MiniGameDataCheck(); // 미니게임 데이터 체크 (SO 데이터와 맞는지)
            SaveJson(); // 이후 json에 저장
        }
        else
        {
            //파일이 없을 경우 파일 생성 및 초기화
            FirstSave(); // PlayerData를 FirstSave()에 설정되어 있는 값으로 초기화
            SaveJson(); // 이후 json에 저장
        }
    }

    // 현재 Json 파일을 저장하고 싶다면 아래 메소드를 호출
    public void SaveJson()
    {
        string jsonData = JsonUtility.ToJson(m_playerData, true); // JSON 형태로 포멧팅
        File.WriteAllText(path, jsonData); // 파일 생성 및 저장
    }

    // 저장 되어있는 Json 파일을 불러오고 싶다면 아래 메소드를 호출
    public void LoadJson()
    {
        string jsonData = File.ReadAllText(path); // 파일의 텍스트를 string으로 저장
        m_playerData = JsonUtility.FromJson<PlayerData>(jsonData); // 이 Json데이터를 역직렬화하여 playerData에 넣어줌
    }

    // 세이브 파일이 없을 시 초기화 설정 값
    void FirstSave()
    {
        // name은 게임 시작시 닉네임 실정에 따라 변경하게 만들 예정
        m_playerData.name = "name";
        m_playerData.level = 1;
        m_playerData.exp = 0;
        m_playerData.coin = 100;
        m_playerData.gameIndex = new List<int>();
        m_playerData.haveGames = new List<bool>();
        m_playerData.rankingPoint = new List<int>();

        for (int i = 0; i < MiniGameManager.Instance.MiniGames.games.Count; i++)
        {
            // miniGameDataSO의 List 수만큼 추가
            m_playerData.gameIndex.Add(i);
            m_playerData.haveGames.Add(false);
            m_playerData.rankingPoint.Add(0);
        }
        for (int i = 0; i < 6; i++)
        {
            // 처음 5개의 게임은 주어질 예정
            m_playerData.haveGames[i] = true;
        }
    }
    void MiniGameDataCheck()
    {
        for (int i = 0; i < MiniGameManager.Instance.MiniGames.games.Count; i++)
        {
            // 만약 m_playerData.gameIndex 에 i가 포함되어있지 않다면
            if (!m_playerData.gameIndex.Contains(i))
            {
                // 그 위치에 값을 추가
                m_playerData.gameIndex.Insert(i, i);
                m_playerData.haveGames.Insert(i, false);
                m_playerData.rankingPoint.Insert(i, 0);
            }
        }
    }
    private void OnApplicationQuit()
    {
        // 게임 종료 시 실행할 작업
        SaveJson();
    }
}

    /* 
        다른 곳에서 활용 예시 : 게임이 끝나고 결과창
        PlayerData 값 증가 로직 (Coin 이나 exe 같은 것)
        PlayerDataManager.instance.SaveJson(); Json 세이브
        PlayerDataManager.instance.LoadJson(); 안전한 초기화를 위해 Json 다시 불러오기 (필요 없어도 됌)
        PlayerData 값을 사용하여 결과창 표시 로직
    */

[System.Serializable]
public class PlayerData // Json으로 파일을 Load 하거나 Save 할 때의 데이터 
{
    // public int id; 고유 id 코드를 불러오는 것인데 아직 필요한지 모르겠음
    public string name; // 플레이어 이름
    public int level;   // 플레이어 현재 레벨
    public float exp;   // 플레이어 현재 경험치 량
    public int coin;    // 플레이어가 가지고 있는 코인 재화
    public int diamond; // 플레이어가 가지고 있는 보석 재화
    public int ticket;   // 플레이어가 가지고 있는 게임 뽑기 티켓 수
    public int stage { get; set; }   // 게임 진행시 현재 진행 스테이지
    public int life { get; set; }   // 게임 진행시 플레이어의 목숨 수치
    public int rewardExp { get; set; }   // 게임 진행 후 얻을 경험치
    public int rewardCoin { get; set; }  // 게임 진행 후 얻을 코인
    public int timePoint { get; set; }   // 게임 진행 시간 보너스 점수
    public int bonusPoint { get; set; }  // 게임 진행 기타 보너스 점수

    // 미니게임 인덱스값 저장, 0번은 랜덤게임으로 고정
    public List<int> gameIndex;

    // 미니게임을 가지고 있는지 없는지 판단 false은 없고 true은 가지고 있는걸로
    public List<bool> haveGames;

    // haveGamesIndex와 인덱스가 동일하게, 점수를 기록, 배열 0번은 랜덤 게임
    public List<int> rankingPoint;
}
