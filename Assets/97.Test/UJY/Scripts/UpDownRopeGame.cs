using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UpDownRopeGame : MiniGameSetting
{
    private int m_stage = 10;    //현재는 임시로 숫자 1 사용
    [HideInInspector]public int clearCount;
    [HideInInspector]public float timer;
    [SerializeField]private GameObject m_map;
    [SerializeField]private GameObject m_obstacle;
    private Vector3 m_mapPosition;
    private float m_positiony;

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
        m_missionText.text = "To The Ground With Click!";
        m_timeText[0].text = "TimeLimit";
        m_countText[0].text = "Life";

        //맵의 위치값과 변동을 줄 y값 받아오기
        m_mapPosition = m_map.transform.position;
        m_positiony = m_mapPosition.y;

        for(int i=0; i < m_stage; i++)
        {
            if(i % 3 == 1)
                Instantiate(m_obstacle, m_obstacle.transform.position, Quaternion.identity, transform);
        }

    }

    private void Update()
    {
        //마우스를 클릭할 때 마우스 위치를 받아온 후 위쪽 클릭중이면 올라가고 아래쪽 클릭중이면 내려가도록 함
        if (Input.GetMouseButton(0) && timer > 2)
        {
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));

            if (m_positiony > -36 && mousePoint.y > 0)
                m_positiony -= Time.deltaTime * 7;

            if (m_positiony < -1 && mousePoint.y < 0)
                m_positiony += Time.deltaTime * 7;
        }


        m_map.transform.position = new Vector3(m_mapPosition.x, m_positiony, m_mapPosition.z);

        //시간과 카운트 반영되는 코드
        m_timeText[1].text = (17- timer).ToString("0.00");
        m_countText[1].text = clearCount.ToString();

        //게임 시작 후 미션을 보여주고 나서 1초 후 지움
        timer += Time.deltaTime;
        if (timer > 0.5 && m_missionPrefab.activeSelf == false)
            m_missionPrefab.SetActive(true);
        if (timer > 1.5 && m_missionPrefab.activeSelf == true)
            m_missionPrefab.SetActive(false);

        //2초 후 부터 실제 게임시작 - 시간제한과 클리어를 위한 카운트 ui를 출력
        if (timer > 2)
        {
            m_timePrefab.SetActive(true);
            m_countPrefab.SetActive(true);
        }

        //게임 승리조건
        if (m_positiony > -3 && clearCount > 0)
        {
            m_clearPrefab.SetActive(true);
            timer = 10;
            Invoke("GameClear", 1);
        }

        //게임 패배조건
        if (timer > 17 || clearCount <= 0)
        {
            m_failPrefab.SetActive(true);
            Invoke("GameFail", 1);
        }


    }


}
