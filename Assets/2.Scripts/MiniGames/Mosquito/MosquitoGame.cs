using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class MosquitoGame : MiniGameSetting
{
    [SerializeField] private GameObject Mosquito;
    private Camera m_camera;

    private int m_stage = 5;
    private float m_timer;
    private int m_clearCount;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
        m_camera = CameraManager.Instance.GetCamera();
        //현재 스테이지에 2배수로 모기 생성
        m_clearCount = m_stage * 2;

        m_missionText.text = "kill " + (m_stage * 2) + " Mosquito";
        m_timeText[0].text = "TimeLimit";
        m_countText[0].text = "Count";

        for (int i=0; i < m_clearCount; i++)
        {
            Instantiate(Mosquito,transform.position, Quaternion.identity, transform);
        }

    }

    private void Update()
    {
        m_timeText[1].text = (12 - m_timer).ToString("0.00");
        m_countText[1].text = m_clearCount.ToString();

        //게임 시작 후 미션을 보여주고 타임제한을 보여주도록 함
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
        {
            m_timePrefab.SetActive(true);
            m_countPrefab.SetActive(true);
        }



        // 마우스 클릭시 RAY를 활용하여 타겟 찾기
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //클릭한 오브젝트가 타겟(모기)일 경우 해당 오브젝트의 SetActive를 false로 바꾸고 클리어카운트 줄이기
                if (hit.collider.tag == "Target")
                {
                    hit.collider.gameObject.SetActive(false);
                    m_clearCount--;
                }

            }
        }

        //게임 승리조건
        if(m_clearCount == 0)
        {
            m_clearPrefab.SetActive(true);
            Invoke("GameClear", 1);
        }

        //게임 패배조건
        if (m_timer > 12 && m_clearCount > 0)
        {
            m_failPrefab.SetActive(true);
            Invoke("GameFail", 1);
        }


    }

}
