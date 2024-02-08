using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.VersionControl;
using UnityEngine;

public class RunningGame : MiniGameSetting
{
    [SerializeField] private GameObject m_map;
    private Vector3 m_mapPosition;
    private float m_positionz;
    private float m_maxTime = 15f;
    private float m_timer = 0f;
    private bool m_end = false;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_missionText.text = "결승선까지 달려라!";
        m_maxTime -= m_difficulty2;
        m_mapPosition = m_map.transform.position;
        m_positionz = -4;
    }

    private void FixedUpdate()
    {
        //실시간으로 맵의 위치를 바꿔줌 - 버튼을 누를 때마다 일정 수치만큼 맵이 뒤로 -> 플레이어는 앞으로 가는 느낌
        m_map.transform.position = new Vector3(m_mapPosition.x, m_mapPosition.y, m_positionz);
    }

    // Update is called once per frame
    private void Update()
    {
        m_timeText.text = (m_maxTime - m_timer).ToString("0.00");


        m_timer = m_timer >= m_maxTime ? m_maxTime : m_timer + Time.deltaTime;
        if (m_timer > 0.5 && m_missionPrefab.activeSelf == false)
        {
            m_missionPrefab.SetActive(true);
        }
        if (m_timer > 1.5 && m_missionPrefab.activeSelf == true)
        {
            m_missionPrefab.SetActive(false);
        }

        if (m_timer > 2)
            m_timePrefab.SetActive(true);

        if (!m_end)
        {
            //게임 승리조건 - 맵이 일정량 이상 뒤로 가면
            if (m_positionz < -270)
            {
                m_clearPrefab.SetActive(true);
                Invoke("GameClear", 1);
                m_end = true;
            }

            //게임 패배조건 - 제한시간 내로 버튼을 충분히 누르지 못한 경우
            if ((m_maxTime - m_timer) < 0 && m_positionz > -270)
            {
                m_failPrefab.SetActive(true);
                Invoke("GameFail", 1);
                m_end = true;

            }

        }

    }

    public void RunBtn()
    {
        //버튼을 누르면 맵이 뒤로 가도록 설정
        if(m_positionz >= -270 && m_timer > 2)
        m_positionz -= 10 - m_difficulty1;
    }

}
