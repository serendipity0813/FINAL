using UnityEngine;

public class FoodEat : MiniGameSetting
{
    public int m_clearCount; // 승리 카운트 선언
    private float m_timer; // 현재 시간
    public bool m_startTimer = false; // 첫 스타트 시간 체크
    private float m_maxTime; // 게임 끝나는 시간 지점
    private bool m_end = false; // 게임이 끝났는가?

    public int m_repetition; // 과일을 몇번 뿌릴지
    public float m_speed; // 플레이어 이동 속도

    protected override void Awake()
    {
        base.Awake();

        // 1-1, 2-1, 3-1 에 해당되는 난이도
        switch (m_difficulty1)
        {
            case 0:
            case 1:
                m_repetition = 10;
                m_clearCount = m_repetition;
                m_speed = 7f;
                break;
            case 2:
                m_repetition = 15;
                m_clearCount = m_repetition;
                m_speed = 6f;
                break;
            case 3:
                m_repetition = 20;
                m_clearCount = m_repetition;
                m_speed = 5f;
                break;
        }

        // 1-1, 1-2, 1-3 에 해당되는 난이도
        switch (m_difficulty2)
        {
            case 0:
            case 1:
                m_maxTime = 15;
                break;
            case 2:
                m_maxTime = 12;
                break;
            case 3:
                m_maxTime = 10;
                break;
        }
        m_timer = m_maxTime;
    }
    private void Start()
    {
        //인게임 text내용 설정 + 게임 승리조건
        m_missionText.text = "음식을 " + m_maxTime  + "초 안에 " + m_repetition + "개 먹어라";

        CameraManager.Instance.ChangeCamera(CameraView.Angle90View);
    }
    private void Update()
    {
        UiTime();
        CheckWinLose();
    }
    void UiTime()
    {
        //시간과 카운트 반영되는 코드
        m_timeText.text = m_timer.ToString("0.00");
        m_countText.text = m_clearCount.ToString();

        //게임 시작 후 미션을 보여주고 나서 1초 후 지움
        if (m_timer > 0f)
        {
            if (!m_end)
            {
                m_timer -= Time.deltaTime;
            }
        }
        if (!m_startTimer)
        {
            if (m_timer < m_maxTime - 0.5f && m_missionPrefab.activeSelf == false)
                m_missionPrefab.SetActive(true);
            if (m_timer < m_maxTime - 1.5f && m_missionPrefab.activeSelf == true)
                m_missionPrefab.SetActive(false);

            //2초 후 부터 실제 게임시작 - 시간제한과 클리어를 위한 카운트 ui를 출력
            if (m_timer < m_maxTime - 2f)
            {
                m_timer = m_maxTime;
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
            if (m_clearCount <= 0)
            {
                // 승리시 로직
                //Debug.Log("이겼다!");
                m_clearPrefab.SetActive(true);
                m_end = true;
                Invoke("GameClear", 1);
            }
            if (m_timer <= 0f && m_clearCount > 0)
            {
                // 패배시 로직
                //Debug.Log("졌다!");
                m_failPrefab.SetActive(true);
                m_end = true;
                Invoke("GameFail", 1);
            }
        }
    }
}
