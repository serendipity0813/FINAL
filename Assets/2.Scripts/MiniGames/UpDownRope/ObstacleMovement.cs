using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private UpDownRopeGame upDownRopeGame;
    private Vector3 m_targetPosition;
    private float m_speed;

    private void Awake()
    {
        upDownRopeGame = transform.parent.GetComponent<UpDownRopeGame>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        m_targetPosition.x = Random.Range(9.0f, 12.0f);
        m_targetPosition.y = Random.Range(-31, -5);
        m_speed = Random.Range(0.05f, 0.1f) + upDownRopeGame.difficulty * 0.05f;
        gameObject.transform.position = m_targetPosition;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (gameObject.transform.position == m_targetPosition)
            m_targetPosition.x = -m_targetPosition.x;
        else
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_targetPosition, m_speed);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            Debug.Log("hit");
            if (other.gameObject.CompareTag("Target"))
            {
                Debug.Log("hit2");
                upDownRopeGame.clearCount--;
            }
        }
    }
}
