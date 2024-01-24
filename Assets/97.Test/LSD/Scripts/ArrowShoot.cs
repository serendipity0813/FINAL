using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    private Rigidbody m_rb;
    private TargetShootGame targetShootGame;
    private Vector3 m_zeroVector = new Vector3(0f, 0f, 0f);
    public bool m_isfly;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        targetShootGame = transform.GetComponentInParent<TargetShootGame>();
    }
    private void Start()
    {
        m_isfly = false;
    }
    private void Update()
    {
        MoveArrow();
        if (m_isfly)
        {
            float rotation = 955f * Time.deltaTime;
            transform.Rotate(Vector3.forward, rotation);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            targetShootGame.m_clearCount--;
            m_rb.velocity = m_zeroVector;
            m_isfly = false;
            StickToTarget(collision.transform);
        }
    }
    private void StickToTarget(Transform target)
    {
        transform.SetParent(target);
    }
    private void MoveArrow()
    {
        if (transform.position.z > 45f)
        {
            targetShootGame.m_arrowCount--;
            if (targetShootGame.m_arrowCount <= 0)
            {
                Destroy(gameObject);
            }
            m_rb.velocity = m_zeroVector;
            m_isfly = false;
            transform.rotation = Quaternion.Euler(-180f, 0f, 90f);
            transform.position = new Vector3(0f, 1.5f, -13f);
        }
    }
    public void ShootArrow()
    {
        if (targetShootGame.m_arrowCount > 0)
        {
            m_isfly = true;
            m_rb.velocity = new Vector3(0f, 0f, 55f);
        }
    }
}
