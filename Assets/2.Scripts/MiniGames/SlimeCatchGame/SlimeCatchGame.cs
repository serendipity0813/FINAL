using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SlimeCatchGame : MiniGameSetting
{
    [SerializeField] private GameObject Slime;
    private Camera m_camera;

    private float m_timer;
    private int m_clearCount;
    private bool m_end = false;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
        m_camera = CameraManager.Instance.GetCamera();

        m_clearCount = m_difficulty2 * 2 + 2;

        m_missionText.text = "슬라임을 전부 잡아라!";

        for (int i=0; i < m_clearCount; i++)
        {
            Instantiate(Slime, transform.position, Quaternion.identity, transform);
        }

    }

    private void Update()
    {
        m_timeText.text = (12 - m_timer).ToString("0.00");
        m_countText.text = m_clearCount.ToString();

        //게임 시작 후 미션을 보여주고 타임제한을 보여주도록 함
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
            m_timePrefab.SetActive(true);
            m_countPrefab.SetActive(true);
        }



        // 마우스 클릭시 RAY를 활용하여 타겟 찾기
        if (Input.GetMouseButtonDown(0) && m_timer > 2)
        {
            Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //클릭한 오브젝트가 타겟(모기)일 경우 해당 오브젝트의 SetActive를 false로 바꾸고 클리어카운트 줄이기
                if (hit.collider.tag == "Target")
                {
                    EffectSoundManager.Instance.PlayEffect(27);
                    hit.collider.gameObject.SetActive(false);
                    m_clearCount--;
                }
            }
        }


        if (!m_end)
        {
            //게임 승리조건
            if (m_clearCount == 0)
            {
                EffectSoundManager.Instance.PlayEffect(21);
                m_clearPrefab.SetActive(true);
                Invoke("GameClear", 1);
                m_end = true;
            }

            //게임 패배조건
            if (m_timer > 12 && m_clearCount > 0)
            {
                EffectSoundManager.Instance.PlayEffect(22);
                m_failPrefab.SetActive(true);
                Invoke("GameFail", 1);
                m_end = true;
            }

        }
     

    }

}
