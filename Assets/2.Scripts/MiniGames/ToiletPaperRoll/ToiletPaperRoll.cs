using UnityEngine;

public class ToiletPaperRoll : MiniGameSetting
{
    public int m_level1; // 현재 미니게임 난이도1
    public int m_level2; // 현재 미니게임 난이도2
    private float m_timer; // 현재 시간
    private int m_winCount = 1; // 승리 카운트 선언
    public bool m_startTimer = false; // 첫 스타트 시간 체크
    private float m_maxTime; // 게임 끝나는 시간 지점
    private bool m_end = false; // 게임이 끝났는가?
    
    public int m_rollCount; // 실제 휴지 회전 카운트
    public float m_angularVelocity; // 돌아가는 스피드 max 값


    protected override void Awake()
    {
        base.Awake();
        m_level1 = 3; // 임시 레벨
        m_level2 = 3;
        m_angularVelocity = 80; // 돌리는 힘값 고정

        // 1-1, 2-1, 3-1 에 해당되는 난이도
        switch (m_level1)
        {
            case 0:
            case 1:
                m_maxTime = 15f;
                break;
            case 2:
                m_maxTime = 12f;
                break;
            case 3:
                m_maxTime = 10f;
                break;
        }

        // 1-1, 1-2, 1-3 에 해당되는 난이도
        switch (m_level2)
        {
            case 0:
            case 1:
                m_rollCount = 30;
                break;
            case 2:
                m_rollCount = 40;
                break;
            case 3:
                m_rollCount = 50;
                break;
        }
    }
    private void Start()
    {
        //인게임 text내용 설정 + 게임 승리조건
        m_missionText.text = "all Use toilet paper " + m_maxTime + " second";

        CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
    }
    void Update()
    {
        UiTime();
        CheckWinLose();
    }
    void UiTime()
    {
        //시간과 카운트 반영되는 코드
        m_timeText.text = m_timer.ToString("0.00");
        m_countText.text = m_winCount.ToString();

        //게임 시작 후 미션을 보여주고 나서 1초 후 지움
        m_timer += Time.deltaTime;
        if (!m_startTimer)
        {
            if (m_timer > 0.5 && m_missionPrefab.activeSelf == false)
                m_missionPrefab.SetActive(true);
            if (m_timer > 1.5 && m_missionPrefab.activeSelf == true)
                m_missionPrefab.SetActive(false);

            //2초 후 부터 실제 게임시작 - 시간제한과 클리어를 위한 카운트 ui를 출력
            if (m_timer > 2)
            {
                m_timer = 0f;
                m_startTimer = true;
                m_timePrefab.SetActive(true);
                m_countPrefab.SetActive(true);
            }
        }
    }

    void CheckWinLose() // 승리 패배 판단 함수
    {
        if (!m_end)
        {
            if (m_rollCount <= 0)
            {
                // 승리시 로직
                m_winCount = 0;
                Debug.Log("이겼다!");
                m_clearPrefab.SetActive(true);
                m_end = true;
                Invoke("GameClear", 1);
            }
            else if (m_winCount > 0 && m_timer > m_maxTime)
            {
                // 패배시 로직
                Debug.Log("졌다!");
                m_failPrefab.SetActive(true);
                m_end = true;
                Invoke("GameFail", 1);
            }
        }
    }
}