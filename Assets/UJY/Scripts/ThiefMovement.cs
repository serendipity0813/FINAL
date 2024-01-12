using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ThiefMovement : MonoBehaviour
{
    private Vector3[] m_hidePosition;
    private Vector3 m_targetPositon;
    private float m_thiefSpeed;
    private int m_length;
    private bool m_isGaming; 
    private bool m_isChanging;

    private void Start()
    {

        m_thiefSpeed = 0.1f;
        m_length = ThiefCaveManager.Instance.m_hidePosition.Length;
        m_hidePosition = new Vector3[m_length];

        //ThiefCavaManager 에서 위치값을 받아와 캐싱해두기
        for (int i = 0; i < m_length; i++)
        {
            m_hidePosition[i] = ThiefCaveManager.Instance.m_hidePosition[i];
        }

        m_targetPositon = m_hidePosition[Random.Range(0, m_hidePosition.Length)];
    }

    private void FixedUpdate()
    {
        // 게임 진행중 여부를 프레임마다 받아오기
        m_isGaming = ThiefCaveManager.Instance.IsGaiming;
        m_isChanging = ThiefCaveManager.Instance.IsChanging;

        if (m_isGaming == true)
        {
            //게임이 진행중이면서 현재 위치와 타겟 위치가 다를 경우 다음 목표로 이동하기
            if (gameObject.transform.position != m_targetPositon)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_targetPositon, m_thiefSpeed);
            }
            //다음 목표 위치를 랜덤값으로 설정하는 스크립트
            else
            {
                if(m_isChanging)
                m_targetPositon = m_hidePosition[Random.Range(0, m_hidePosition.Length)];
            }
        }
          
    }

}
