using UnityEngine;

public class FoodShoot : MiniGameSetting
{
    [SerializeField] private int m_bulletCount; // 처음에 주어지는 총알 개수
    [SerializeField] private int m_winCount = 0; // 승리 카운트 선언
    [SerializeField] private int m_win; // 미니 게임에 승리하기 위한 변수
    [SerializeField] private int m_level; // 현재 미니게임 난이도
    public int m_repetition; // 과일을 몇번 뿌릴지
    private float m_timer;

    protected override void Awake()
    {
        base.Awake();
        // m_level을 매니저에서 가져오기 임시로 레벨 1로 부여
        m_level = 1;
        switch (m_level)
        {
            case 0:
            case 1:
                m_bulletCount = 5;
                m_win = 3;
                m_repetition = 7;
                break;
            case 2:
                m_bulletCount = 5;
                m_win = 3;
                m_repetition = 5;
                break;
            case 3:
                m_bulletCount = 4;
                m_win = 3;
                m_repetition = 4;
                break;
        }
    }
    private void Start()
    {
        //인게임 text내용 설정 + 게임 승리조건
        m_missionText.text = "Shoot " + (m_win) + " Food";
        m_timeText[0].text = "TimeLimit";
        m_countText[0].text = "Count";
    }
    void Update()
    {
        UiTime();
        Bullet(); // 보기 편하게 만들기 위해 함수로 변경
    }
    void UiTime()
    {
        //게임 시작 후 미션을 보여주고 타임제한을 보여주도록 함
        m_timer += Time.deltaTime;
        if (m_timer > 0 && m_missionPrefab.activeSelf == false)
        {
            m_missionPrefab.SetActive(true);

        }
        if (m_timer > 1 && m_missionPrefab.activeSelf == true)
        {
            m_missionPrefab.SetActive(false);
            m_timePrefab.SetActive(true);
        }

        //2초 후 부터 실제 게임시작 - 시간제한과 클리어를 위한 카운트 ui를 출력
        if (m_timer > 2)
        {
            m_timePrefab.SetActive(true);
            m_countPrefab.SetActive(true);
        }
    }
    void Bullet() // 업데이트에서 동작
    {
        if (m_bulletCount != 0) // 현재 총알이 0개가 아닐경우
        {
            if (Input.GetMouseButtonDown(0))  // 마우스 왼쪽 버튼 클릭 시
            {
                m_bulletCount--; // 총알 개수 -
                HitFood();  // 레이 발사 함수 호출
                CheckWinLose(); // 승리 패배 판단 함수 호출
            }
        }
    }

    void HitFood() // 레이를 쏴서 푸드를 맞췄을 시
    {
        // 마우스 위치에서 레이 생성
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit; // 레이캐스트 히트 정보 받아오기

        if (Physics.Raycast(ray, out hit))  // 레이캐스트 실행
        {
            GameObject hitObject = hit.collider.gameObject; // 맞은 오브젝트를 가져옴
            m_winCount++; // 승리 카운터 증가
            Destroy(hitObject); // 맞은 오브젝트를 삭제
        }
    }

    void CheckWinLose() // 승리 패배 판단 함수
    {
        if (m_winCount >= m_win)
        {
            // 승리시 로직
            m_bulletCount = 0; // 승리했을 시 총알 개수를 0개로 변경
            Debug.Log("이겼다!");
            m_clearPrefab.SetActive(true);
            Invoke("GameClear", 1);
        }
        else if (m_winCount < m_win && m_bulletCount == 0 || m_timer > 12)
        {
            // 패배시 로직
            Debug.Log("졌다!");
            m_failPrefab.SetActive(true);
            Invoke("GameFail", 1);
        }
    }
}
