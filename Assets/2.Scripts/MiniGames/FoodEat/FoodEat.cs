using UnityEngine;

public class FoodEat : MiniGameSetting
{
    [SerializeField] private int m_level; // 현재 미니게임 난이도
    public int m_repetition; // 과일을 몇번 뿌릴지
    private int m_stage = 1;    //현재는 임시로 숫자 1 사용
    public int m_clearCount; // 승리 카운트 선언
    private float m_timer;
    private bool m_end = false;

    protected override void Awake()
    {
        base.Awake();
        m_level = 1; // 임시 레벨
        switch (m_level)
        {
            case 0:
            case 1:
                m_repetition = 10;
                m_clearCount = m_repetition;
                break;
            case 2:
                m_repetition = 15;
                m_clearCount = m_repetition;
                break;
            case 3:
                m_repetition = 20;
                m_clearCount = m_repetition;
                break;
        }
    }
    private void Start()
    {
        //인게임 text내용 설정 + 게임 승리조건
        m_missionText.text = "Eat " + (m_repetition) + " Food";
        m_timeText[0].text = "TimeLimit";
        m_countText[0].text = "Count";

    }
    private void Update()
    {
        UiTime();
        CheckWinLose();
    }
    void UiTime()
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
    void CheckWinLose() // 승리 패배 판단 함수
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
            if (m_timer > 12 && m_clearCount > 0 || m_timer > 12)
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
