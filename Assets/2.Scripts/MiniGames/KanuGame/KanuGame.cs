using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class KanuGame : MiniGameSetting
{
    [SerializeField] private GameObject[] m_playerAvartar;
    [SerializeField] private GameObject m_map;
    [SerializeField] private GameObject m_handle1;
    [SerializeField] private GameObject m_handle2;
    [SerializeField] private GameObject m_handle3;
    [SerializeField] private Slider m_slider;
    private Vector3 m_mapPosition;
    private float m_positionz;
    private float m_power = 7;
    private float m_maxTime = 15;
    private float m_timer = 0;
    private bool m_powerFlag;
    private bool m_delayChecker;
    private float m_powerDelay;
    private bool m_end = false;


    protected override void Awake()
    {
        /*부모클래스 필드 및 메소드 받아오기
         인게임 UI 목록 : m_missionUI, m_timeUI, m_countUI, m_clearUI, m_failUI
         인게임 Text 목록 : m_missionText, m_timeText, m_timeText, m_countText, m_countText
         배열의 경우 0번은 내용(남은시간, 남은횟수 등), 1번은 숫자 입니다.
         인게임 Method 목록 : GameClear(); , GameFail(); */

        base.Awake();
    }

    private void Start()
    {
        m_mapPosition = m_map.transform.position;
        m_positionz = m_mapPosition.y;
        m_maxTime -= m_difficulty1;
        //인게임 text내용 설정 + 게임 승리조건
        m_missionText.text = "위에서 땡기고 아래에서 밀어라!";    

    }

    private void FixedUpdate()
    {
        ///마우스를 누를 땐 시간당 힘 증가, 마우스를 때면 시간당 힘 감소
        ///누르고 있는 경우 힘에 비례하여 앞으로 가지만 힘 수치가 일정량 초과시 오히려 뒤로감
        ///때고 있는 경우 힘에 반비례하여 앞으로 가지만 힘 수치가 일정량 미달시 오히려 뒤로감
        m_powerDelay += Time.deltaTime;
        if(m_powerDelay >= 0.3)
            m_delayChecker = true;

        if (m_timer > 2)
        {
            m_positionz += m_difficulty2 * 0.2f;
            if (Input.GetMouseButtonDown(0) && m_powerDelay >= 0.3)
            {
                int index = Random.Range(0, 3) + 23;
                EffectSoundManager.Instance.PlayEffect(index);
            }
            else if (Input.GetMouseButtonUp(0) && m_delayChecker == true)
            {
                m_powerDelay = 0;
                m_delayChecker = false;
            }

            if (Input.GetMouseButton(0) && m_power <= 15 && m_powerDelay >= 0.3)
            {
                m_power += Time.deltaTime * (5 + m_difficulty1);
                if (m_power == 10)
                    m_powerFlag = false;
            }
            else if (m_power >= 0)
            {
                m_power -= Time.deltaTime * (5 + m_difficulty1);
                if (m_power == 5)
                    m_powerFlag = true;

            }

            //힘을 기반으로 배가 움직이는 로직
            if (m_powerFlag)
            {
                m_positionz -= m_power;
            }
            else
            {
                if (m_power < 5)
                    m_positionz += (5 - m_power);
                else
                    m_positionz -= (10 - m_power);
            }
        }
     

        //노 젓는 모션을 위한 함수
        if (m_power < 7)
        {
            m_handle1.SetActive(true);
            m_handle2.SetActive(false);
            m_handle3.SetActive(false);
        }
        else if( 7 <= m_power && m_power <= 8)
        {
            m_handle1.SetActive(false);
            m_handle2.SetActive(true);
            m_handle3.SetActive(false);
        }
        else if (m_power > 8)
        {
            m_handle1.SetActive(false);
            m_handle2.SetActive(false);
            m_handle3.SetActive(true);
        }


        m_slider.value = m_power;
        m_map.transform.position = new Vector3(m_mapPosition.x, m_mapPosition.y, m_positionz / 5);
    }

    private void Update()
    {

        //시간과 카운트 반영되는 코드
        m_timeText.text = (m_maxTime-m_timer).ToString("0.00");

        //게임 시작 후 미션을 보여주고 나서 1초 후 지움
        m_timer = m_timer >= m_maxTime ? m_maxTime : m_timer + Time.deltaTime;
        if (m_timer > 0.5 && m_missionPrefab.activeSelf == false)
            m_missionPrefab.SetActive(true);
        if (m_timer > 1.5 && m_missionPrefab.activeSelf == true)
            m_missionPrefab.SetActive(false);

        //2초 후 부터 실제 게임시작 - 시간제한과 클리어를 위한 카운트 ui를 출력
        if (m_timer > 2)
        {
            m_timePrefab.SetActive(true);
        }

        if (!m_end)
        {
            //게임 승리조건
            if (m_positionz < -1000)
            {
                EffectSoundManager.Instance.PlayEffect(21);
                m_clearPrefab.SetActive(true);
                Invoke("GameClear", 1);
                m_end = true;
            }

            //게임 패배조건
            if (m_timer > m_maxTime || m_positionz > 300)
            {
                EffectSoundManager.Instance.PlayEffect(22);
                m_power = 0;
                m_positionz = -500;
                m_failPrefab.SetActive(true);
                Invoke("GameFail", 1);
                m_end = true;
            }

        }
        


    }
}
