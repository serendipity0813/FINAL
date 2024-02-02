using UnityEngine;

public class SpinnerController : MonoBehaviour
{
    private Rigidbody m_rigidbody;//플레이어의 Rigidbody
    private SpinnerGame spinnerGame;
    private bool m_down = false; // 점수가 올라가는 기준치
    private bool m_up = false; 
    private Vector3 m_halfScreen; // 스크린 위아래 나누기
    private Vector3 m_mousePosition; // 현재 마우스 포지션
    private bool m_isUpDown; // 화면 기준 터치가 위인지 아래인지

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        spinnerGame = GetComponentInParent<SpinnerGame>();
        // 화면 중간값 구하기
        m_halfScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
        m_rigidbody.maxAngularVelocity = 50; // 돌아가는 스피드 max 값
    }

    void Update()
    {
        Spin();
        SpinCheck();
    }

    // 한바퀴 돌았는지 판단
    void SpinCheck()
    {
        // 현재 오브젝트 Rotation 값을 Update로 받아오기
        Quaternion currentRotation = transform.rotation;

        // m_up, m_down 둘 다 체크되고 가로 절반이 넘었을 시
        if (m_up && m_down && (currentRotation.y < -0.5f || currentRotation.y > 0.5f))
        {
            m_up = false;
            m_down = false;
            if (spinnerGame.m_winCount > 0)
            {
                spinnerGame.m_winCount--; // 회전 카운트 감소
            }
        }

        // 위, 아래를 넘었을시
        if (!m_up && currentRotation.y > 0.5f || currentRotation.y < -0.5f)
        {
            m_up = true;
        }
        if (!m_down && (currentRotation.y > 0f && currentRotation.y < -0.5f) || (currentRotation.y < 0f && currentRotation.y > -0.5f))
        {
            m_down = true;
        }
    }

    // 스핀 동작
    void Spin()
    {
        if (spinnerGame.m_startTimer)
        {
            m_mousePosition = Input.mousePosition;
            m_isUpDown = m_mousePosition.y > m_halfScreen.y;
            if (m_isUpDown)
            {
                if (TouchManager.instance.IsDragLeft())
                {
                    m_rigidbody.angularVelocity = Vector3.down * 100;

                }
                if (TouchManager.instance.IsDragRight())
                {
                    m_rigidbody.angularVelocity = Vector3.up * 100;
                }
            }
            else
            {
                if (TouchManager.instance.IsDragLeft())
                {
                    m_rigidbody.angularVelocity = Vector3.up * 100;
                }
                if (TouchManager.instance.IsDragRight())
                {
                    m_rigidbody.angularVelocity = Vector3.down * 100;
                }
            }
        }
    }
}
