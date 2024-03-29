using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager instance { get; private set; }
    public PlayerData m_playerData;
    public ItemDataSO ItemData;
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
        PlatformCheck();
        DataCheck();
        Debug.Log(path);
    }

    /* 
        다른 곳에서 활용 예시 : 게임이 끝나고 결과창
        PlayerData 값 증가 로직 (Coin 이나 exe 같은 것)
        SaveJson(); Json 세이브
        LoadJson(); 안전한 초기화를 위해 Json 다시 불러오기 (필요 없어도 됌)
        PlayerData 값을 사용하여 결과창 표시 로직
    */

    private void PlatformCheck() // Json 파일위치 전처리문
    {
#if UNITY_ANDROID
        path = Path.Combine(Application.persistentDataPath, "playerDatas.json");
#elif UNITY_IOS
    path = Path.Combine(Application.persistentDataPath, "playerDatas.json");
#else
    path = Path.Combine(Application.dataPath, "Data", "playerDatas.json");
#endif
    }

    private void DataCheck() // 파일 체크
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

    // 현재 Json 파일을 저장하고 싶다면 메소드를 호출
    public void SaveJson()
    {
        string jsonData = JsonUtility.ToJson(m_playerData, true); // JSON 형태로 포멧팅
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonData); // Json을 바이트 배열로
        string code = System.Convert.ToBase64String(bytes); // 바이트 배열을 base-64 인코딩 문자로
        File.WriteAllText(path, code); // 파일 생성 및 저장
    }

    // 저장 되어있는 Json 파일을 불러오고 싶다면 메소드를 호출
    public void LoadJson()
    {
        string code = File.ReadAllText(path); // Json으로 저장된 base-64 인코딩 문자를 받아온다.
        byte[] bytes = System.Convert.FromBase64String(code); // base-64을 바이트 배열로
        string jsonData = System.Text.Encoding.UTF8.GetString(bytes); // 바이트 배열을 Json 문자열로
        // Json 문자열을 역직렬화하여 playerData에 넣어줌
        m_playerData = JsonUtility.FromJson<PlayerData>(jsonData); 
    }

    // 세이브 파일이 없을 시 초기화 설정 값
    void FirstSave()
    {
        // name은 게임 시작시 닉네임 실정에 따라 변경하게 만들 예정
        m_playerData.name = "name";
        m_playerData.level = 1;
        m_playerData.exp = 0;
        m_playerData.coin = 1000;
        m_playerData.tutorial = false;
        m_playerData.bgmVolume = 0.25f;
        m_playerData.sfxVolume = 0.3f;
        m_playerData.gameIndex = new List<int>();
        m_playerData.haveGames = new List<bool>();
        m_playerData.rankingPoint = new List<int>();
        m_playerData.haveSkin = new bool[10];
        m_playerData.equipSkin = new bool[10];

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

        //첫 번째는 기본 아바타, 나머지는 없도록 세이브
        for(int i=0; i< ItemData.items.Count; i++)
        {
            if(i==0)
            {
                m_playerData.haveSkin[i] = true;
                m_playerData.equipSkin[i] = true;
            }

            else
            {
                m_playerData.haveSkin[i] = false;
                m_playerData.equipSkin[i] = false;
            } 
        }
    }

    // 미니게임 SO 데이터 체크
    void MiniGameDataCheck()
    {
        // 미니게임 List 크기 만큼
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

    // 리워드 관련 메서드
    public void MinigameRewardCheck()
    {
        //플레이어 리워드 계산 코드
        m_playerData.rewardExp = m_playerData.stage;
        m_playerData.rewardCoin = (m_playerData.stage * 10) + (m_playerData.timePoint / 5) + (m_playerData.bonusPoint / 5);

        // 경험치 코인 증가
        m_playerData.exp += m_playerData.rewardExp;
        m_playerData.coin += m_playerData.rewardCoin;

        //레벨업 체크
        if(m_playerData.exp >= m_playerData.level * 15)
        {
            // 레벨업 했다면 레벨업 효과
            m_playerData.exp -= m_playerData.level * 15;
            m_playerData.level++;
        }
    }

    // 상점에서 스킨을 샀을 시
    public void GetItem(int ItemCode)
    {
        m_playerData.haveSkin[ItemCode] = true;

        SaveJson();
        LoadJson();
    }

    // 상점에서 코인을 샀을 시 (현재 비활성화)
    public void GetCoin(int index)
    {
        m_playerData.coin += index;

        SaveJson();
        LoadJson();
    }

    // 상점에서 구매한 캐릭터를 장착, 해제 메서드
    public void EquipItem(int ItemCode)
    {
        for (int i = 0; i < m_playerData.equipSkin.Length; i++)
        {
            if (i == ItemCode)
            {
                m_playerData.equipSkin[ItemCode] = true;
            }
            else
                m_playerData.equipSkin[i] = false;
        }

        SaveJson();
        LoadJson();
    }

    // 상점에서 캐릭터 구매 메서드
    public int GetSkin()
    {
        int count = 0;

        foreach(bool result in m_playerData.equipSkin)
        {
            if(result)//착용중인 스킨이 있으면 Count(스킨 번호) 리턴
            {
                return count;
            }

            count++;
        }
        
        //착용중인 스킨이 하나도 없을 경우 가장 첫번째 스킨 리턴
        return 0;
    }

    // 코인 단위 변경 메서드
    public string ChangeNumber(string number)
    {
        if (instance.m_playerData.coin > 99999)
        {
            int unit = 0;
            while (number.Length > 6)
            {
                unit++;
                number = number.Substring(0, number.Length - 3);
            }
            if (number.Length > 3)
            {
                char[] unitAlphabet = new char[3] { 'K', 'M', 'B' };
                int newInt = int.Parse(number);
                if (number.Length > 4)
                {
                    return (newInt / 1000).ToString() + unitAlphabet[unit];
                }
                else
                {
                    return (newInt / 1000f).ToString("0.0") + unitAlphabet[unit];
                }
            }
            else
            { return number; }
        }
        else { return number; }
    }

    // 게임 종료 시 실행할 작업
    private void OnApplicationQuit()
    {
        SaveJson();
    }
}


// Json으로 파일을 Load 하거나 Save 할 때의 클래스 데이터
[System.Serializable] // 직열화
public class PlayerData
{
    // Json에 저장되는 데이터
    // public int id; 고유 id 코드를 불러오는 것인데 아직 필요한지 모르겠음
    public string name; // 플레이어 이름
    public int profileIndex; // 프로필 패턴 넘버
    public int level;   // 플레이어 현재 레벨
    public float exp;   // 플레이어 현재 경험치 량
    public int coin;    // 플레이어가 가지고 있는 코인 재화
    public bool tutorial; // 플레이어 튜토리얼 수행 여부 false는 안함, true 는 함
    public float bgmVolume; // BGM 볼륨
    public float sfxVolume; // SFX 볼륨
    public bool[] haveSkin; //플레이어 스킨 소지여부
    public bool[] equipSkin; // 플레이어 스킨 장착여부
    public List<int> gameIndex; // 미니게임 인덱스값 저장, 0번은 랜덤게임으로 고정
    // 미니게임을 가지고 있는지 없는지 판단 false은 없고 true은 가지고 있는걸로
    public List<bool> haveGames;
    // haveGamesIndex와 인덱스가 동일하게, 점수를 기록, 배열 0번은 랜덤 게임
    public List<int> rankingPoint;


    // Json에 저장되지 않는 변수
    public int stage { get; set; }   // 게임 진행시 현재 진행 스테이지
    public int life { get; set; }   // 게임 진행시 플레이어의 목숨 수치
    public int rewardExp { get; set; }   // 게임 진행 후 얻을 경험치
    public int rewardCoin { get; set; }  // 게임 진행 후 얻을 코인
    public int timePoint { get; set; }   // 게임 진행 시간 보너스 점수
    public int bonusPointIndex { get; set; }  // 게임 진행 기타 보너스 점수
    public int bonusPoint { get; set; }  // 게임 진행 기타 보너스 점수
}
