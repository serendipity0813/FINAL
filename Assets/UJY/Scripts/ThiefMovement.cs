using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ThiefMovement : MonoBehaviour
{
    private float m_delaytime;
    private Vector3[] m_hidePosition;
    private Vector3 m_targetPositon;
    private bool m_hide;

    private void Awake()
    {
        m_delaytime = 0.1f;
    }

    private void Start()
    {
        int length = ThiefCaveManager.Instance.m_hidePosition.Length;
        m_hidePosition = new Vector3[length];

        for (int i = 0; i < length; i++)
        {
            m_hidePosition[i] = ThiefCaveManager.Instance.m_hidePosition[i];
        }

        this.transform.position = m_hidePosition[Random.Range(0, m_hidePosition.Length)];

    }

    private void FixedUpdate()
    {
        ThiefMoving();
    }

    private void ThiefMoving()
    {
        m_targetPositon = m_hidePosition[Random.Range(0, m_hidePosition.Length)];
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, m_targetPositon, 0.05f);


    }


}
