using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class RunningGame : MiniGameSetting
{
    [SerializeField] private GameObject m_map;
    private Vector3 m_mapPosition;
    private float m_positionz;
    private float m_timer;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_mapPosition = m_map.transform.position;
        m_positionz = -4;
    }

    // Update is called once per frame
    private void Update()
    {   
        //실시간으로 맵의 위치를 바꿔줌 - 버튼을 누를 때마다 일정 수치만큼 맵이 뒤로 -> 플레이어는 앞으로 가는 느낌
        m_map.transform.position = new Vector3(m_mapPosition.x, m_mapPosition.y, m_positionz);
        m_timer += Time.deltaTime;
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

        //게임 승리조건 - 맵이 일정량 이상 뒤로 가면
        if (m_positionz < -270)
        {
            m_clearPrefab.SetActive(true);
            Invoke("GameClear", 1);
        }

        //게임 패배조건 - 제한시간 내로 버튼을 충분히 누르지 못한 경우
        if (m_timer > 10 && m_positionz > -270)
        {
            m_failPrefab.SetActive(true);
            Invoke("GameFail", 1);
        }


    }

    public void RunBtn()
    {
        //버튼을 누르면 맵이 뒤로 가도록 설정
        if(m_positionz >= -270 && m_timer > 2)
        m_positionz -= 7;
    }

}
