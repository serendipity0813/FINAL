using UnityEngine;

public class AnimatorUpdater : MonoBehaviour
{
    private DragToMoveController m_controller;
    private Animator m_animator;

    private int m_magnitude = Animator.StringToHash("Magnitude");
    private int m_velocityY = Animator.StringToHash("VelocityY");
    private int m_isGround = Animator.StringToHash("IsGround");
    private int m_sleep = Animator.StringToHash("Sleep");
    private int m_wake = Animator.StringToHash("Wake");

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_controller = transform.parent.GetComponent<DragToMoveController>();//부모객체의 이동 스크립트를 받아옴
       
        if (!m_controller)
        {
            Debug.Log("캐릭터의 부모 객체에 이동 스크립트가 없어 파라미터 업데이트 불가능");
        }
    }

    private void Update()
    {
        if (m_controller)
        {
            //캐릭터 이동 스크립트에서 값을 받아옴
            float value = m_controller.GetMagnitude();
            Vector3 velocity = m_controller.GetVelocity();
            bool isGround = m_controller.GetIsGround();

            //Animator의 파라미터에 값을 전달하기 위한 코드
            m_animator.SetFloat(m_magnitude, value);//플레이어의 Velocity의 magnitude 값을 애니메이터 변수에 전달
            m_animator.SetFloat(m_velocityY, velocity.y);
            m_animator.SetBool(m_isGround, isGround);
        }
    }

    public void SleepCharacter()
    {
       
        m_animator.SetTrigger(m_sleep);
    }

    public void WakeCharacter()
    {
        m_animator.SetTrigger(m_wake);
    }
}

