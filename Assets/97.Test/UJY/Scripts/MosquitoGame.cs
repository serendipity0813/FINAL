using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class MosquitoGame : MiniGameSetting
{
    [SerializeField] private GameObject Mosquito;
    //[SerializeField] private GameObject m_missionUI;
    //private GameObject m_missionPrefab;
    //[SerializeField] private GameObject m_timeUI;
    //private GameObject m_timePrefab;
    //[SerializeField] private GameObject m_clearUI;
    //private GameObject m_clearPrefab;
    //[SerializeField] private GameObject m_failUI;
    //private GameObject m_failPrefab;

    private int stage = 10;
    private float m_timer;
    private int m_clearCount;

    private void Start()
    {
        m_clearCount = stage * 2;

        for (int i=0; i < m_clearCount; i++)
        {
            Instantiate(Mosquito,transform.position, Quaternion.identity, transform);
        }

        StartSetting();

        //m_missionPrefab = Instantiate(m_missionUI, transform.position, Quaternion.identity, transform);
        //m_timePrefab = Instantiate(m_timeUI, transform.position, Quaternion.identity, transform);
        //m_clearPrefab = Instantiate(m_clearUI, transform.position, Quaternion.identity, transform);
        //m_failPrefab = Instantiate(m_failUI, transform.position, Quaternion.identity, transform);
        //m_missionPrefab.SetActive(false);
        //m_timePrefab.SetActive(false);
        //m_clearPrefab.SetActive(false);
        //m_failPrefab.SetActive(false);
    }

    private void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer > 0.5 && m_missionPrefab.activeSelf == false)
        {
            m_missionPrefab.SetActive(true);
        }
        if (m_timer > 1.5 && m_missionPrefab.activeSelf == true )
        {
            m_missionPrefab.SetActive(false);
        }

        if(m_timer > 2)
            m_timePrefab.SetActive(true);


        // 마우스 클릭시 RAY를 활용하여 타겟 찾기
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Target")
                {
                    hit.collider.gameObject.SetActive(false);
                    m_clearCount--;
                }

            }
        }

        if(m_clearCount == 0)
        {
            m_clearPrefab.SetActive(true);
            Invoke("GameClear", 1);
        }

        if (m_timer > 10 && m_clearCount > 0)
        {
            m_failPrefab.SetActive(true);
            Invoke("GameFail", 1);
        }


    }
    //private void GameClear()
    //{        
    //    MiniGameManager.Instance.GameClear();
    //}

    //private void GameFail()
    //{
    //    MiniGameManager.Instance.GameFail();
    //}
}
