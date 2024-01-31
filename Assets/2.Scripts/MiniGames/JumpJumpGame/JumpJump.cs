using UnityEngine;

public class JumpJump : MiniGameSetting
{
    public int m_level1; // 현재 미니게임 난이도1
    public int m_level2; // 현재 미니게임 난이도2
    [SerializeField] private GameObject m_player;
    private float m_timer;
    private float m_maxTime;
    private bool m_startTimer = false;
    private bool m_end = false;
    public float m_JumpArrivalPoint; // 도착지점 거리
    public int m_clearCount = 1; // 승리 카운트 선언
    public float m_JumpForceSpeed;  // 점프 각도 스피드

    protected override void Awake()
    {
        base.Awake();
        m_level1 = 3; // 임시 레벨
        m_level2 = 3;

        // 1-1, 2-1, 3-1 에 해당되는 난이도
        switch (m_level1)
        {
            case 0:
            case 1:
                m_JumpArrivalPoint = 7.5f;
                break;
            case 2:
                m_JumpArrivalPoint = 9.5f;
                break;
            case 3:
                m_JumpArrivalPoint = 12.5f;
                break;
        }

        // 1-1, 1-2, 1-3 에 해당되는 난이도
        switch (m_level2)
        {
            case 0:
            case 1:
                m_JumpForceSpeed = 2f;
                m_maxTime = 12f;
                break;
            case 2:
                m_JumpForceSpeed = 2.5f;
                m_maxTime = 11f;
                break;
            case 3:
                m_JumpForceSpeed = 3f;
                m_maxTime = 10f;
                break;
        }
    }
    private void Start()
    {
        //인게임 text내용 설정 + 게임 승리조건
        m_missionText.text = "Jump In " + m_maxTime + " second";
        m_timeText[0].text = "TimeLimit";
        m_countText[0].text = "Count";

        CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
        CameraManager.Instance.SetFollowSpeed(5f);
        CameraManager.Instance.SetFollowTarget(m_player);
        CameraManager.Instance.ToggleCameraFollow();
    }
    private void Update()
    {
        UiTime();
        CheckWinLose();
    }
    private void UiTime()
    {
        //시간과 카운트 반영되는 코드
        m_timeText[1].text = m_timer.ToString("0.00");
        m_countText[1].text = m_clearCount.ToString();

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
    private void CheckWinLose() // 승리 패배 판단 함수
    {
        if (!m_end)
        {
            if (m_clearCount <= 0)
            {
                // 승리시 로직
                Debug.Log("이겼다!");
                m_clearPrefab.SetActive(true);
                m_end = true;
                CameraManager.Instance.ToggleCameraFollow();
                Invoke("GameClear", 1);
            }
            if (m_timer > m_maxTime)
            {
                // 패배시 로직
                Debug.Log("졌다!");
                m_failPrefab.SetActive(true);
                m_end = true;
                CameraManager.Instance.ToggleCameraFollow();
                Invoke("GameFail", 1);
            }
        }
    }
}

