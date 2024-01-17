using System;
using Unity.VisualScripting;
using UnityEngine;

public class DragToMoveController : MonoBehaviour
{
    private Rigidbody m_rigidbody;//플레이어의 Rigidbody
    private float m_speed = 3.0f;//이동 속도
    private float m_jumpPower = 300.0f;//점프 거리 
    private bool m_readyJump;//점프가능한 상태인지 확인하는 변수
    private bool m_rayHitted;//레이캐스트 hit한 물체가 있는지 확인하는 변수

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    //점프도 가능한 캐릭터 이동 함수
    public void UpdateMoveWithJump()
    {

        float dis = 0.0f;

        {//땅과의 거리 측정
            Ray ray = new Ray(transform.position, Vector3.down);//캐릭터 위치에서 y -1방향으로 레이 생성
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
                dis = ray.origin.y - hit.point.y;
        }

        if (dis < 0.51f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 hitpos;

            RaycastHit hit;
            m_rayHitted = Physics.Raycast(ray, out hit);

            if (m_rayHitted && TouchManager.instance.IsBegan())//처음 터치한 대상이 플레이어일 때 점프가능한 상태로 만들기
            {//점프 가능한 상태일 때는 플레이어 이동이 안됨
                Debug.Log("PlayerClicked");
                m_readyJump = hit.transform.tag == "Player" ? true : false;
            }

            if (m_readyJump && !TouchManager.instance.IsHolding())//점프 준비 상태에서 손을 땟을 때
            {
                if (TouchManager.instance.IsDragUp())//위로 드래그 했을 경우
                {
                    m_readyJump = false;//준비 상태 해제
                    m_rigidbody.AddForce(new Vector3(0, m_jumpPower, 0));//위로 AddForce
                }
            }
            
            if (m_rayHitted && TouchManager.instance.IsHolding() && !m_readyJump)//터치 누르고 있는 상태일 때 & 점프 준비 상태가 아닐 때
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
            }
        }
    }

    //점프를 제외하고 이동만 가능한 함수
    public void UpdateMove()
    {
     
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 hitpos;

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && TouchManager.instance.IsHolding() )
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
            }
        
    }


}
