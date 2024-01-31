using UnityEngine;

public class JumpJumpGround : MonoBehaviour
{
    private JumpJump m_jumpJump;
    [SerializeField] private GameObject m_finish;
    [SerializeField] private GameObject m_start;

    private float rndPos;
    private float rndScale;
    private float m_rndPosSum;

    private void Awake()
    {
        m_jumpJump = GetComponentInParent<JumpJump>();
    }
    void Start()
    {
        MakeGround();
    }
    void RandomDistanceLocation()
    {
        switch (m_jumpJump.m_level1)
        {
            case 0:
            case 1:
                rndPos = Random.Range(2.5f, 3f);
                rndScale = Random.Range(1.5f, 2f);
                break;
            case 2:
                rndPos = Random.Range(2.75f, 3.25f);
                rndScale = Random.Range(1.25f, 1.75f);
                break;
            case 3:
                rndPos = Random.Range(2.8f, 3.5f);
                rndScale = Random.Range(1f, 1.75f);
                break;
        }
    }
    void MakeGround()
    {
        GameObject start = Instantiate(m_start, transform.position, Quaternion.identity, transform);
        start.transform.position = new Vector3(transform.position.x - 3f, transform.position.y - 2f, transform.position.z);

        GameObject finish = Instantiate(m_finish, transform.position, Quaternion.identity, transform);
        finish.transform.position = new Vector3(m_jumpJump.m_JumpArrivalPoint, transform.position.y - 1.99f, transform.position.z);

        while (true)
        {
            RandomDistanceLocation();
            m_rndPosSum += rndPos;
            GameObject ground = Instantiate(m_start, transform.position, Quaternion.identity, transform);
            ground.transform.position = new Vector3(transform.position.x - 3f + m_rndPosSum, transform.position.y - 2f, transform.position.z);
            ground.transform.localScale = new Vector3(rndScale, 1f, 1f);
            if(ground.transform.position.x > m_jumpJump.m_JumpArrivalPoint - 1.6f)
            {
                Destroy(ground);
                break;
            }
        }
    }
}
