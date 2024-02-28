using UnityEngine;

public class JumpJumpController : MonoBehaviour
{
    private JumpJump m_jumpJump;
    private Rigidbody m_rd;
    public bool m_isJump = true; // 점프인지 아닌지 true 가 점프
    private bool m_checkUpDown = true; // 점프 힘 감소 or 증가
    public float m_curJumpForce; // 현재 점프 힘
    public float m_minJumpForce; // 최소 점프 힘
    public float m_maxJumpForce; // 최대 점프 힘
    public Vector3 fallSave;  // 낭떠러지 저장구간

    
    private void Awake()
    {
        m_jumpJump = GetComponentInParent<JumpJump>();
        m_rd = GetComponent<Rigidbody>();
    }
    void Start()
    {
        // 초기화 값
        m_minJumpForce = 1000f;
        m_maxJumpForce = 2000f;
        m_curJumpForce = m_minJumpForce;
    }
    private void Update()
    {
        HoldPower();
    }
    public void Jump()
    {
        m_rd.AddForce(m_curJumpForce, m_curJumpForce, 0f);
    }
    public void HoldPower()
    {
        if (!m_isJump)
        {
            if (m_checkUpDown)
            {
                m_curJumpForce += Mathf.Lerp(m_minJumpForce, m_maxJumpForce, 0.1f) * m_jumpJump.m_JumpForceSpeed * Time.deltaTime;
                if (m_curJumpForce > m_maxJumpForce - 1f)
                {
                    m_checkUpDown = false;
                }
            }
            if (!m_checkUpDown)
            {
                m_curJumpForce -= Mathf.Lerp(m_minJumpForce, m_maxJumpForce, 0.1f) * m_jumpJump.m_JumpForceSpeed * Time.deltaTime;
                if (m_curJumpForce < m_minJumpForce + 1f)
                {
                    m_checkUpDown = true;
                }
            }
        }
    }
    public void PlayerSave()
    {
        transform.position = fallSave;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            EffectSoundManager.Instance.PlayEffect(8);
            m_jumpJump.m_clearCount--;
            m_isJump = true;
        }
        if (collision.gameObject.CompareTag("Terrain"))
        {
            fallSave = transform.position;
            EffectSoundManager.Instance.PlayEffect(1);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            m_rd.mass = 6f;
            m_isJump = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            m_isJump = true;
        }
    }
}
