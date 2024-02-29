using UnityEngine;

public class JumpJumpGround : MonoBehaviour
{
    [SerializeField] private GameObject m_finish;
    [SerializeField] private GameObject m_start;
    private JumpJump m_jumpJump;
    private float rndPos;
    private float rndScale;
    private float m_rndPosSum;

    private void Awake()
    {
        m_jumpJump = GetComponentInParent<JumpJump>();
    }
    void Start()
    {
        // 시작하면 땅 만들기
        MakeGround();
    }
    
    // 바닥 생성
    void MakeGround()
    {
        // 처음 스타트 생성
        GameObject start = Instantiate(m_start, transform.position, Quaternion.identity, transform);
        start.transform.position = new Vector3(transform.position.x - 3f, transform.position.y - 2f, transform.position.z);

        // 마지막 피시니 구역 생성
        GameObject finish = Instantiate(m_finish, transform.position, Quaternion.identity, transform);
        finish.transform.position = new Vector3(m_jumpJump.m_JumpArrivalPoint, transform.position.y - 1.99f, transform.position.z);

        // 피니시를 넘어가기 전까지만 땅을 생성
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

    void RandomDistanceLocation()
    {
        // 난이도에 따라 랜덤하게 땅 거리 + 크기 변경
        switch (m_jumpJump.difficulty)
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
}
