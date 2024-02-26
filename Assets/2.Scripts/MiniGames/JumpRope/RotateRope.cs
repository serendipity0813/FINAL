using UnityEngine;

public class RotateRope : MonoBehaviour
{

    private JumpRopeGame m_game;//부모 객체의 스크립트를 가져오기 위한 변수
    private float m_rotateSpeed = 90.0f;//회전속도 초당 90도
    private bool m_once;//회전 한번에 하나만 카운트 하기 위한 변수
    private bool m_inside = false;//밧줄 안쪽에 들어왔는지 확인하는 변수


    private void Start()
    {
        m_game = transform.parent.GetComponent<JumpRopeGame>();

        Debug.Log(m_game.GetDifficulty());


        //m_rotateSpeed = 90.0f + m_game.GetDifficulty();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * m_rotateSpeed));

        if (m_inside && transform.rotation.eulerAngles.z < 10)
        {

            if (!m_once)
            {
                m_game.DecreaseCount();
                m_game.CheckWin();
            }

            m_once = true;
        }
        else
        {
            m_once = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            m_inside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            m_inside = false;
        }
    }
}
