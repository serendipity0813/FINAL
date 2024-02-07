using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findthiefgame : MiniGameSetting
{
    private float m_timer;
    public Vector3[] m_hidePosition { get; private set; }   //동굴 위치를 나타내는 백터 배열
    public bool IsGaiming { get; private set; }     //게임 진행중임을 체크
    public bool IsChanging { get; private set; }    //도둑들이 다음 경로를 찾아야하는지 여부 체크용
    public bool GameOver { get; private set; }
    private bool m_end = false;
    private bool m_clear;
    public GameObject Target;
    public GameObject Thief;
    public GameObject Cave;
    private Camera m_camera;

    protected override void Awake()
    {
        base.Awake();
    }


    // Start is called before the first frame update
    private void Start()
    {
        m_missionText.text = "보물을 가진 도둑을 잡아라!";

        CameraManager.Instance.ChangeCamera(CameraView.Angle90View);
        m_camera = CameraManager.Instance.GetCamera();

        m_clear = false;
        IsGaiming = false;
        IsChanging = true;
        m_hidePosition = new Vector3[12];

        //도둑이 숨을 나무 생성
        for (int i = 1; i < 4; i++)
        {
            for (int j = 1; j < 5; j++)
            {
                m_hidePosition[i * 4 + j - 5] = new Vector3(i * 3 - 6, 0, j * 4 - 10);
                Cave.transform.position = m_hidePosition[i * 4 + j - 5];
                Instantiate(Cave, m_hidePosition[i * 4 + j - 5], Quaternion.identity, transform);

            }
        }

        for (int i = 0; i < 12; i++)
        {
            if (i % 4 < m_difficulty2)
            {
                int position = Random.Range(0, 12);
                Thief.transform.position = m_hidePosition[position];
                Instantiate(Thief, m_hidePosition[position], Quaternion.identity, transform);
            }

            if (i == 0)
            {
                int targetposition = Random.Range(0, 12);
                Target.transform.position = m_hidePosition[targetposition];
                Instantiate(Target, m_hidePosition[targetposition], Quaternion.identity, transform);
            }
        }


    }

    private void Update()
    {
        m_timeText.text = (12 - m_timer).ToString("0.00");

        //시간 흐름에 따라 게임 진행 
        m_timer = m_timer >= 12 ? 12 : m_timer + Time.deltaTime;
        if (m_timer > 0.5 && m_missionPrefab.activeSelf == false)
        {
            m_missionPrefab.SetActive(true);
        }
        if (m_timer > 1.5 && m_missionPrefab.activeSelf == true)
        {
            m_missionPrefab.SetActive(false);
        }

        if (m_timer > 2)
        {
            //게임 진행이 시작되며 도둑들이 움직임
            IsGaiming = true;
        }
        if (m_timer > 7)
        {
            //도둑은 움직이지만 다음 경로를 찾는 행위는 중지됨
            IsChanging = false;
        }
        if (m_timer > 8)
        {
            //도둑들이 멈추고 플레이어가 타겟을 찾기 시작
            IsGaiming = false;
            m_timePrefab.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                // 마우스 클릭시 RAY를 활용하여 타겟 찾기
                Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (!m_end)
                    {
                        if (hit.collider.tag == "Target")
                        {
                            m_clearPrefab.SetActive(true);
                            Invoke("GameClear", 1);
                            m_end = true;
                        }
                        else
                        {
                            m_failPrefab.SetActive(true);
                            Invoke("GameFail", 1);
                            m_end = true;
                        }
                    }
           
                }
            }

        }

        if (m_timer > 15 && m_clear == false)
        {
            m_failPrefab.SetActive(true);
            Invoke("GameFail", 1);
            m_end = true;
        }

    }
}
