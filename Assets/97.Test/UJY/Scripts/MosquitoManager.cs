using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MosquitoManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Mosquitos;

    private int stage = 10;
    private float m_timer;
    private int m_clearCount;

    private void Start()
    {
        m_clearCount = stage * 2;

        for (int i=0; i < m_clearCount; i++)
        {
            Mosquitos[i].SetActive(true);
        }


    }


    private void Update()
    {
        m_timer += Time.deltaTime;
        //if (m_timer > 0.5 && Mission.activeSelf == false)
        //{
        //    Mission.SetActive(true);
        //}
        //if (m_timer > 1.5 && Mission.activeSelf == true)
        //{
        //    Mission.SetActive(false);
        //}

        //시간 흐름에 따라 게임 진행 
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 클릭시 RAY를 활용하여 타겟 찾기
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
            //Clear.SetActive(true);
            Invoke("GameClear", 1);
        }

        if (m_timer > 10 && m_clearCount > 0)
        {
            //Fail.SetActive(true);
            Invoke("GameFail", 1);
        }


    }
    private void GameClear()
    {
        //Clear.SetActive(false);
        MiniGameManager.Instance.GameClear();
    }

    public void GameFail()
    {
        //Fail.SetActive(false);
        MiniGameManager.Instance.GameFail();
    }
}
