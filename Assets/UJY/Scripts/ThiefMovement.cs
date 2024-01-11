using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ThiefMovement : MonoBehaviour
{
    private Vector3[] m_hidePosition;
    private Vector3 m_targetPositon;
    private int m_length;
    private bool m_isGaming;

    private void Start()
    {
        m_isGaming = ThiefCaveManager.Instance.IsGaiming;
        m_length = ThiefCaveManager.Instance.m_hidePosition.Length;
        m_hidePosition = new Vector3[m_length];

        for (int i = 0; i < m_length; i++)
        {
            m_hidePosition[i] = ThiefCaveManager.Instance.m_hidePosition[i];
        }

        m_targetPositon = m_hidePosition[Random.Range(0, m_hidePosition.Length)];
    }

    private void FixedUpdate()
    {
        
        if (gameObject.transform.position != m_targetPositon)
        {

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_targetPositon, 0.1f);
        }
        else
        {
            //if(m_isGaming == true)
            m_targetPositon = m_hidePosition[Random.Range(0, m_hidePosition.Length)];
        }

    }

}
