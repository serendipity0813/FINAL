using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorUpdater : MonoBehaviour
{
    private DragToMoveController m_controller;
    private Animator m_animator;

    private int m_magnitude = Animator.StringToHash("Magnitude");
    private int m_velocityY = Animator.StringToHash("VelocityY");
    private int m_isGround = Animator.StringToHash("IsGround");


    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_controller = transform.parent.GetComponent<DragToMoveController>();
    }

    private void Update()
    {
        float value = m_controller.GetMagnitude();
        Vector3 velocity = m_controller.GetVelocity();
        bool isGround = m_controller.GetIsGround();

        m_animator.SetFloat(m_magnitude, value);//플레이어의 Velocity의 magnitude 값을 애니메이터 변수에 전달
        m_animator.SetFloat(m_velocityY, velocity.y);
        m_animator.SetBool(m_isGround, isGround);

    }
}

