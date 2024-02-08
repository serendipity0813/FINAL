using System;
using UnityEngine;

[DefaultExecutionOrder(1)]

class DragToMoveController : MonoBehaviour
{
    private Rigidbody m_rigidbody;//플레이어의 Rigidbody
    private Camera m_camera;//현재 포커싱 카메라
    private float m_speed = 3.0f;//이동 속도
    private float m_jumpPower = 300.0f;//점프 거리 
    private bool m_readyJump;//점프가능한 상태인지 확인하는 변수
    private bool m_isGround;//캐릭터가 땅에 닿아 있는지 확인하는 변수
    private bool m_rayHitted;//레이캐스트 hit한 물체가 있는지 확인하는 변수

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_camera = CameraManager.Instance.GetCamera();//현재 작동중인 카메라를 받아옴
        m_isGround = true;//GroundCheck없이 사용할 게임을 위해 true로 초기화
    }

    //점프도 가능한 캐릭터 이동 함수
    public void UpdateMoveWithJump()
    {

        //캐릭터가 땅에 있을 때만 작동 
        if (m_isGround)
        {
            Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);//카메라로 부터 클릭한 위치까지의 레이 생성
            Vector3 hitpos;

            RaycastHit hit;
            m_rayHitted = Physics.Raycast(ray, out hit);

            if (m_rayHitted && TouchManager.instance.IsBegan())//처음 터치한 대상이 플레이어일 때 점프가능한 상태로 만들기
            {
                m_readyJump = hit.transform.tag == "Player" ? true : false;
            }

            if (m_readyJump && !TouchManager.instance.IsHolding())//점프 준비 상태에서 손을 땟을 때
            {
                if (TouchManager.instance.IsDragUp())//위로 드래그 했을 경우
                {
                    Vector3 force = new Vector3(0, m_jumpPower, 0);
                    m_readyJump = false;//준비 상태 해제
                    m_rigidbody.AddForce(force);//위로 AddForce
                }
            }

            if (m_rayHitted && TouchManager.instance.IsHolding() && !m_readyJump)//터치 누르고 있는 상태일 때 & 점프 준비 상태가 아닐 때
            {

                if (hit.transform.tag == "Terrain")//Terrain을 클릭했을 때
                {
                    hitpos = hit.point;//클릭한 위치를 저장

                    hitpos = hitpos - transform.position;//이동할 위치의 좌표로 향하는 방향 벡터 생성
                    hitpos.y = 0;//y축 이동은 제외
                    m_rigidbody.velocity = hitpos.normalized * m_speed;//법선 벡터로 변환하여 속도만큼 곱해준다.

                    transform.rotation = Quaternion.LookRotation(hitpos, Vector3.up);
                }
                else
                {
                    hitpos = transform.position;//이외에는 플레이어 위치 그대로
                }

         
            }
        }
    }

    //점프를 제외하고 이동만 가능한 함수
    public void UpdateMove()
    {

        Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
        Vector3 hitpos;

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && TouchManager.instance.IsHolding())
        {

            if (hit.transform.tag == "Terrain")
            {
                hitpos = hit.point;
            }
            else
            {
                hitpos = transform.position;
            }

            hitpos = hitpos - transform.position;
            hitpos.y = 0;
            m_rigidbody.velocity = hitpos.normalized * m_speed;
            transform.rotation = Quaternion.LookRotation(hitpos, Vector3.up);
        }
    }


    public void SetJumpPower(float power)
    {
        m_jumpPower = power;
    }

    public void SetMoveSpeed(float speed)
    {
        m_speed = speed;
    }

    public void SetIsGround(bool isGround)
    {
        m_isGround = isGround;
    }
    
    public float GetMagnitude()
    {
        return m_rigidbody.velocity.magnitude;
    }

    public Vector3 GetVelocity()
    {
        return m_rigidbody.velocity;
    }

    public bool GetIsGround()
    {
        return m_isGround;
    }
}
