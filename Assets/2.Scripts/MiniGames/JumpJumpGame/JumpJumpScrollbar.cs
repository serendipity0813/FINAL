using UnityEngine;
using UnityEngine.UI;

public class JumpJumpScrollbar : MonoBehaviour
{
    private Scrollbar m_scrollbar;
    private JumpJumpController jumpJumpController;

    void Start()
    {
        m_scrollbar = GetComponent<Scrollbar>();
        jumpJumpController = GetComponentInParent<JumpJumpController>();
    }

    // Update is called once per frame
    void Update()
    {
        m_scrollbar.size = Mathf.InverseLerp(jumpJumpController.m_minJumpForce, jumpJumpController.m_maxJumpForce,
            jumpJumpController.m_curJumpForce);
    }
}
