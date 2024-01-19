using UnityEngine;

public class FoodEat : MiniGameSetting
{
    [SerializeField] private int m_level; // 현재 미니게임 난이도
    public int m_winCount; // 승리 카운트 선언
    public int m_repetition; // 과일을 몇번 뿌릴지
    private float m_timer;

    private void Awake()
    {
        StartSetting();
        
        m_level = 1;
        switch (m_level)
        {
            case 0:
            case 1:
                m_repetition = 10;
                m_winCount = m_repetition;
                break;
            case 2:
                m_repetition = 15;
                m_winCount = m_repetition;
                break;
            case 3:
                m_repetition = 20;
                m_winCount = m_repetition;
                break;
        }
    }
    private void Update()
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
        CheckWinLose();
    }
    void CheckWinLose() // 승리 패배 판단 함수
    {
        if (m_winCount <= 0)
        {
            // 승리시 로직
            Debug.Log("이겼다!");
            m_clearPrefab.SetActive(true);
            Invoke("GameClear", 1);
        }
        //else if (m_winCount > 0 && 시간 == 0)
        //{// 만약 제한시간 내에 음식을 모두 못먹을 시
        //    // 패배시 로직
        //    Debug.Log("졌다!");
        //    m_failPrefab.SetActive(true);
        //    Invoke("GameFail", 1);
        //}
    }
}
