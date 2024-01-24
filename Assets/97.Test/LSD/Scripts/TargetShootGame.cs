using UnityEngine;

public class TargetShootGame : MiniGameSetting
{
    [SerializeField] private int m_level; // 현재 미니게임 난이도
    private float m_timer;
    private bool m_end = false;

    public int m_arrowCount; // 화살 개수
    public float m_targetSpeed; // 타겟 스피드
    public int m_clearCount = 1; // 승리 카운트 선언

    protected override void Awake()
    {
        base.Awake();
        m_level = 2; // 임시 레벨
        m_arrowCount = 3;
        switch (m_level)
        {
            case 0:
            case 1:
                m_targetSpeed = 5f;
                break;
            case 2:
                m_targetSpeed = 10f;
                break;
            case 3:
                m_targetSpeed = 15f;
                break;
        }
    }
    private void Start()
    {
        //인게임 text내용 설정 + 게임 승리조건
        m_missionText.text = "Target in " + m_arrowCount + " shots";
        m_timeText[0].text = "TimeLimit";
        m_countText[0].text = "Count";

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
        if (m_timer > 0.5 && m_missionPrefab.activeSelf == false)
            m_missionPrefab.SetActive(true);
        if (m_timer > 1.5 && m_missionPrefab.activeSelf == true)
            m_missionPrefab.SetActive(false);

        //2초 후 부터 실제 게임시작 - 시간제한과 클리어를 위한 카운트 ui를 출력
        if (m_timer > 2)
        {
            m_timePrefab.SetActive(true);
            m_countPrefab.SetActive(true);
        }

        //게임 패배조건
        if (m_timer > 12 && m_clearCount > 0)
        {
            m_failPrefab.SetActive(true);
            Invoke("GameFail", 1);
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
                Invoke("GameClear", 1);
            }
            if (m_timer > 12 || m_arrowCount <= 0)
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
