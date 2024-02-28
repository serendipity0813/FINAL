using UnityEngine;

public class TargetMove : MonoBehaviour
{
    private TargetShootGame targetShootGame;
    private float m_speed;     // 이동 속도
    private bool m_moveCheck;  // 방향 체크
    private void Awake()
    {
        targetShootGame = transform.GetComponentInParent<TargetShootGame>();
    }
    private void Start()
    {
        m_speed = targetShootGame.m_targetSpeed; // 스피드값 가져오기

        int rnd = Random.Range(0, 2);
        if (rnd == 0)
            m_moveCheck = true;
        else
            m_moveCheck = false;
    }
    private void Update()
    {
        MoveTarget();
    }
    private void MoveTarget()
    {
        // 타겟을 오른쪽과 왼쪽으로 이동하도록 처리
        if (m_moveCheck)
        {
            transform.Translate(Vector3.right * m_speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * m_speed * Time.deltaTime);
        }

        // 타겟의 현재 위치가 초기 위치에서 일정 거리를 넘었는지 확인
        if (transform.position.x > 5f)
        {
            m_moveCheck = false;
        }
        if (transform.position.x < -5f)
        {
            m_moveCheck = true;
        }
    }
}
