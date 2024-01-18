using System;
using Unity.VisualScripting;
using UnityEngine;

public class FoodEatController : MonoBehaviour
{
    private Rigidbody m_rigidbody;//플레이어의 Rigidbody
    [SerializeField] private float m_speed = 10.0f;//이동 속도

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        UpdateMove();
    }

    //점프를 제외하고 이동만 가능한 함수
    public void UpdateMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 hitpos;

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && TouchManager.instance.IsHolding() )
        {
            hitpos = hit.point;
            if (hit.transform.name == "Collider")
            {
                hitpos = transform.position;
            }
            hitpos = hitpos - transform.position;
            hitpos.y = 0;
            m_rigidbody.velocity = hitpos.normalized * m_speed;
        }
    }
}
