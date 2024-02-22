using UnityEngine;

public class ShuffleThief : MonoBehaviour
{
    private Rigidbody m_rigidbody;//캐릭터의 Rigidbody
    private Vector3 m_targetPosition;//캐릭터의 이동 포인트
    private Vector3 m_velocity;//캐릭터의 속도
    private float m_speed = 6.0f;//이동 속도
    private bool m_move = false;//캐릭터의 이동 가능 여부

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = transform.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (m_move)
        {
            m_velocity = m_targetPosition - transform.position;
            if (m_velocity.magnitude < 0.1f)//이동 목표와 매우 가까우면 이동 중지
            {
                m_rigidbody.velocity = Vector3.zero;
                m_move = false;
            }
            else
            {
                m_rigidbody.velocity = m_velocity.normalized * m_speed;//캐릭터 이동
                transform.rotation = Quaternion.LookRotation(m_velocity, Vector3.up);//캐릭터를 해당방향으로 바라보게
            }
        }
    }

    //이동 목표 포인트 설정 후 이동 가능하게
    public void MoveToPoint(Vector3 pos)
    {
        m_targetPosition = pos;
        m_move = true;
    }

    //캐릭터의 이동속도 설정 배율
    public void SetSpeed(float multiplier)
    {
        m_speed = 6.0f * multiplier;
    }

}
