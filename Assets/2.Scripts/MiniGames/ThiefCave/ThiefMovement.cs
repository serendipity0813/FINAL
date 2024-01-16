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

        //ThiefCavaManager ���� ��ġ���� �޾ƿ� ĳ���صα�
        for (int i = 0; i < m_length; i++)
        {
            m_hidePosition[i] = ThiefCaveManager.Instance.m_hidePosition[i];
        }

        m_targetPositon = m_hidePosition[Random.Range(0, m_hidePosition.Length)];
    }

    private void FixedUpdate()
    {
        // ���� ������ ���θ� �����Ӹ��� �޾ƿ���
        m_isGaming = ThiefCaveManager.Instance.IsGaiming;
        m_isChanging = ThiefCaveManager.Instance.IsChanging;

        if (m_isGaming == true)
        {
            //������ �������̸鼭 ���� ��ġ�� Ÿ�� ��ġ�� �ٸ� ��� ���� ��ǥ�� �̵��ϱ�
            if (gameObject.transform.position != m_targetPositon)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_targetPositon, m_thiefSpeed);
            }
            //���� ��ǥ ��ġ�� ���������� �����ϴ� ��ũ��Ʈ
            else
            {
                if(m_isChanging)
                m_targetPositon = m_hidePosition[Random.Range(0, m_hidePosition.Length)];
            }
        }
          
    }

}
