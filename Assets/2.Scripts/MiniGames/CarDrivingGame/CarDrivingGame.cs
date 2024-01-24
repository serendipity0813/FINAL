using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrivingGame : MiniGameSetting
{
    [SerializeField] private GameObject m_map;
    private Vector3 m_mapPosition;
    private float m_positionx;
    private float m_positionz;
    public int clearCount;
    private float m_timer;
    private bool m_left;
    private bool m_right;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_missionText.text = "Drive Car with left, right Button";
        m_timeText[0].text = "Remain";
        m_countText[0].text = "Life";

        //맵의 초기 위치값 세팅
        m_mapPosition = m_map.transform.position;
        m_positionx = -1;
        m_positionz = 0;
        m_left = false; 
        m_right = false;
    }

    private void FixedUpdate()
    {
        //시간이 흐르면서 맵이 자동적으로 뒤로 가도록 함 

        if (m_positionz > -550 && m_timer > 2)      //수치제한
        {
            m_positionz -= Time.deltaTime * 30;
        }

        if (m_positionx < 6 && m_left == true)
            m_positionx += Time.deltaTime * 10;

        if (m_positionx > -12 && m_right == true)
            m_positionx -= Time.deltaTime * 10;

        //맵을 좌, 우로 옮기는 효과 - 플레이어는 캐릭터가 좌, 우로 움직이는 느낌
        m_map.transform.position = new Vector3(m_positionx, m_mapPosition.y, m_positionz);
    }

    private void Update()
    {
        m_timeText[1].text = (m_positionz + 500).ToString("0.00");
        m_countText[1].text = clearCount.ToString();

      
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

        if (clearCount >= 3)
            HitOver();


        if (m_positionz < -500)
        {
            m_clearPrefab.SetActive(true);
            Invoke("GameClear", 1);
        }

        


    }

    public void HitOver()
    {
        clearCount = 0;
        m_positionz = 0;
        m_failPrefab.SetActive(true);
        Invoke("GameFail", 1);
    }

    public void LeftBtn()
    {
        if (m_timer > 2)
        {
            m_left = true;
            m_right = false;
        }

    }

    public void RightBtn()
    {
        if (m_timer > 2)
        {
            m_left = false;
            m_right = true;
        }
    }



}
