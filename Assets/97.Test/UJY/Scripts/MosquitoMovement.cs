using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoMovement : MonoBehaviour
{
    private Vector3 m_targetPosition;
    private float m_mosquitoSpeed;
    float m_xPosition;
    float m_yPosition;

    private void Start()
    {
        m_targetPosition = new Vector3(0, 0, 0);
        m_mosquitoSpeed = 0.2f;
    }

    private void FixedUpdate()
    {
        if (true)
        {
            //현재 오브젝트의 위치가 다음 포지션과 다를 경우 해당 포지션으로 이동하도록 함
            if (gameObject.transform.position != m_targetPosition)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_targetPosition, m_mosquitoSpeed);
            }
            //만약 같을 경우 다음 포지션을 찾아야하는지 체크 후 랜덤한 다음 포지션을 설정
            else
            {
                m_xPosition = Random.Range(-4, 5);
                m_yPosition = Random.Range(-8, 8);
                m_targetPosition = new Vector3(m_xPosition, m_yPosition, 0);
            }
        }
    }
}
