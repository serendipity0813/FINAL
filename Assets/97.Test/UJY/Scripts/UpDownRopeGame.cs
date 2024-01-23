using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownRopeGame : MiniGameSetting
{
    private int m_stage = 1;    //현재는 임시로 숫자 1 사용
    public int clearCount;
    private float m_timer;
    [SerializeField] private GameObject m_map;
    private Vector3 m_mapPosition;
    private float m_positiony;
    private bool m_up;
    private bool m_down;

    protected override void Awake()
    {
        /*부모클래스 필드 및 메소드 받아오기
         인게임 UI 목록 : m_missionUI, m_timeUI, m_countUI, m_clearUI, m_failUI
         인게임 Text 목록 : m_missionText, m_timeText[0], m_timeText[1], m_countText[0], m_countText[1]
         배열의 경우 0번은 내용(남은시간, 남은횟수 등), 1번은 숫자 입니다.
         인게임 Method 목록 : GameClear(); , GameFail(); */

        base.Awake();
    }

    private void Start()
    {
        //인게임 text내용 설정 + 게임 승리조건
        clearCount = 3;
        m_missionText.text = "To The Ground!";
        m_timeText[0].text = "TimeLimit";
        m_countText[0].text = "Life";

        m_mapPosition = m_map.transform.position;
        m_positiony = m_mapPosition.y;

    }

    private void Update()
    {

        if (m_positiony > -36 && m_up == true)
            m_positiony -= Time.deltaTime * 5;

        if (m_positiony < -1 && m_down == true)
            m_positiony += Time.deltaTime * 5;

        m_map.transform.position = new Vector3(m_mapPosition.x, m_positiony, m_mapPosition.z);

        //시간과 카운트 반영되는 코드
        m_timeText[1].text = (17-m_timer).ToString("0.00");
        m_countText[1].text = clearCount.ToString();

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

        //게임 승리조건
        if (m_positiony > -3 && clearCount > 0)
        {
            m_clearPrefab.SetActive(true);
            Invoke("GameClear", 1);
        }

        //게임 패배조건
        if (m_timer > 17 || clearCount <= 0)
        {
            m_failPrefab.SetActive(true);
            Invoke("GameFail", 1);
        }


    }

    public void HitOver()
    {
        m_positiony = 0;
        m_failPrefab.SetActive(true);
        Invoke("GameFail", 1);
    }

    public void UpBtn()
    {
        if (m_timer > 2)
        {
            m_up = true;
            m_down = false;
        }

    }

    public void DownBtn()
    {
        if (m_timer > 2)
        {
            m_up = false;
            m_down = true;
        }
    }


}
