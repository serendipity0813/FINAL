using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager instance {get; private set;}
    public PlayerData m_playerData;
    private string path;

    private void Awake()
    {
        // 싱글톤
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
        // Json 데이터 위치 불러오기
        path = Path.Combine(Application.dataPath, "Data", "playerData.json");
    }

    private void Start()
    {
        if (File.Exists(path))
        {
            // json 파일이 있다면 시작할 때 json 파일 데이터 불러오기
            LoadJson();
        }
        else
        {
            //파일이 없을 경우 파일 생성 및 초기화
            FirstSave(); // PlayerData를 FirstSave()에 설정되어 있는 값으로 초기화
            SaveJson(); // 이후 json에 저장
        }
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
        ★ PlayerDataManager.instance.LoadJson(); 안전한 초기화를 위해 Json 다시 불러오기 (필요 없어도 됌)
        게임 결과창 표시 로직
    */

    // 현재 Json 파일을 저장하고 싶다면 아래 메소드를 호출
    public void SaveJson()
    {   
        string jsonData = JsonUtility.ToJson(m_playerData,true); // JSON 형태로 포멧팅
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
        m_playerData.exe = 0;
        m_playerData.coin = 100;
        m_playerData.haveGamesIndex = new List<bool>() {true,true,true,false};
        m_playerData.rankingPoint = new List<int>() {0,0,0,0};
    }
    private void OnApplicationQuit()
    {
        // 게임 종료 시 실행할 작업
        SaveJson();
    }
}

[System.Serializable]
public class PlayerData // Json으로 파일을 Load 하거나 Save 할 때의 데이터 
{
    // public int id; 고유 id 코드를 불러오는 것인데 아직 필요한지 모르겠음
    public string name; // 플레이어 이름
    public int level;   // 플레이어 현재 레벨
    public float exe;   // 플레이어 현재 경험치 량
    public int coin;    // 플레이어가 가지고 있는 코인 재화

    // 미니게임을 가지고 있는지 없는지 판단 false은 없고 true은 가지고 있는걸로
    public List<bool> haveGamesIndex;

    // haveGamesIndex와 인덱스가 동일하게, 점수를 기록
    public List<int> rankingPoint;
}
