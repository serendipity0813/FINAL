using UnityEngine;

public class FoodEatController : MonoBehaviour
{
    private Rigidbody m_rigidbody;//플레이어의 Rigidbody
    private Camera m_camera;//현재 포커싱 중인 카메라
    private FoodEat m_foodEat;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_foodEat = GetComponentInParent<FoodEat>();
    }
    private void Update()
    {
        UpdateMove();
    }

    public void UpdateMove()
    {
        m_camera = CameraManager.Instance.GetCamera();
        Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
        Vector3 hitpos;

        RaycastHit hit;

        if (m_foodEat.m_startTimer)
        {
            if (Physics.Raycast(ray, out hit) && TouchManager.instance.IsHolding())
            {
                hitpos = hit.point;
                if (hit.transform.name == "Collider")
                {
                    hitpos = transform.position;
                }
                hitpos = hitpos - transform.position;
                hitpos.y = 0;
                m_rigidbody.velocity = hitpos.normalized * m_foodEat.m_speed;
            }
        }
    }
}
