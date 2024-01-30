using UnityEngine;
using UnityEngine.UI;

public class JumpJumpBtn : MonoBehaviour
{
    private JumpJumpController m_jumpCon; // 스크립트
    private Button m_button;              // 자신 버튼
    public Vector3 fallSave;              // 낭떠러지 저장구간

    private void Awake()
    {
        m_button = GetComponent<Button>();
        // 자신,부모의,부모 안에서 JumpJumpController 스크립트 가져오기
        m_jumpCon = transform.parent.parent.GetComponentInChildren<JumpJumpController>();
        m_button.onClick.AddListener(Jump);
    }
    private void Jump()
    {
        // 점프 상태가 아니라면 (바닥 이라면)
        if (!m_jumpCon.m_isJump)
        {
            m_jumpCon.Jump(); // m_jumpCon 안의 Jump() 메소드 실행
            m_jumpCon.m_isJump = true;
            fallSave = m_jumpCon.transform.position;
        }
    }
    public void PlayerSave()
    {
        m_jumpCon.transform.position = fallSave;
    }
}
