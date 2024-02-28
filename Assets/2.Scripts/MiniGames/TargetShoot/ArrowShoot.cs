using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    private Rigidbody m_rb;                  // 화살 리지드바디
    private TargetShootGame targetShootGame; // 게임 안의 매니저 역할 접근
    public bool m_isfly = false;             // 날아가고 있는지 없는지

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        targetShootGame = transform.GetComponentInParent<TargetShootGame>();
    }
    private void FixedUpdate()
    {
        MoveArrow();
        if (m_isfly) // 날고 있을시
        {
            float rotation = 1500f * Time.deltaTime; // 화살 회전값
            transform.Rotate(Vector3.forward, rotation); // 화살 회전
        }
    }

    // 타겟에 맞았을 시
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            targetShootGame.m_clearCount--; // 클리어 카운트 낮추기
            m_rb.velocity = Vector3.zero; // 초기화
            m_isfly = false; // 초기화
            EffectSoundManager.Instance.PlayEffect(8);
            StickToTarget(collision.transform);
        }
    }

    // 타겟을 맞추면 해당 오브젝트가 맞춘 오브젝트 자식으로 들어간다.
    private void StickToTarget(Transform target)
    {
        transform.SetParent(target);
    }

    // 화살 이동 관련
    private void MoveArrow()
    {
        if (transform.position.z > 45f)
        {
            targetShootGame.m_arrowCount--; // z값 45 위치까지 이동해야 카운트를 -
            EffectSoundManager.Instance.PlayEffect(18);
            if (targetShootGame.m_arrowCount <= 0)
            {
                Destroy(gameObject);
            }
            m_rb.velocity = Vector3.zero; // 초기화
            m_isfly = false; // 초기화
            transform.rotation = Quaternion.Euler(-180f, 0f, 90f); // 회전을 멈추고 처음과 동일하게
            transform.position = new Vector3(0f, 1.7f, -11.75f); //  m_rb.velocity를 멈춘뒤 포지션값을 처음과 동일하게
        }
    }

    // 버튼에 연결할 메소드
    public void ShootArrow()
    {
        if (targetShootGame.m_arrowCount > 0)
        {
            m_isfly = true; // 날아가는 중
            m_rb.velocity = new Vector3(0f, 0f, 70f); // 날아가는 힘
        }
    }
}
