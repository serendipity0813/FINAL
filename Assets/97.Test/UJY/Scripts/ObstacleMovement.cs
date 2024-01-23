using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private Vector3 m_targetPosition;
    private float m_speed;
    // Start is called before the first frame update
    private void Start()
    {
        m_targetPosition.x = (float)Random.Range(4, 8);
        m_targetPosition.y = Random.Range(-32, 0);
        m_speed = Random.Range(0.1f, 0.2f);

        gameObject.transform.position = m_targetPosition;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (gameObject.transform.position == m_targetPosition)
            m_targetPosition.x = -m_targetPosition.x;

        if (Input.GetMouseButton(0))
        {
            m_targetPosition.y += Time.deltaTime * 7;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_targetPosition, m_speed);
        }
        else
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_targetPosition, m_speed);
        }

    }
}
