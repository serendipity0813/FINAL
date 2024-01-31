using UnityEngine;

public class ToiletPaperRollController : MonoBehaviour
{
    private Rigidbody m_rigidbody; //플레이어의 Rigidbody
    private ToiletPaperRoll toiletPaperRoll;
    private bool m_down = false; // 점수가 올라가는 기준치
    private bool m_up = false;
    private float m_maxCount; // 난이도에 따른 롤 카운트 맥스값
    private float m_nowScale; // 프리팹 크기 조절 관련 선언
    private Quaternion paperQuaternion; // 생성할 paper 프리팹 앵글
    [SerializeField] private GameObject m_paper; // paper 프리팹

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        toiletPaperRoll = GetComponentInParent<ToiletPaperRoll>();
    }
    private void Start()
    {
        m_maxCount = toiletPaperRoll.m_rollCount; // 현재 난이도의 rollCount 값 가져오기
        m_rigidbody.maxAngularVelocity = toiletPaperRoll.m_angularVelocity; // 돌아가는 스피드 max 값
        paperQuaternion = Quaternion.Euler(0, 27.413f, 0); // paper 회전값
    }

    void Update()
    {
        Roll();
        RollCheck();
    }

    // 한바퀴 돌았는지 판단
    void RollCheck()
    {
        // 현재 오브젝트 Rotation 값을 Update로 받아오기
        Quaternion currentRotation = transform.rotation;
        

        // m_up, m_down 둘 다 체크되고 가로 절반이 넘었을 시
        if (m_up && m_down && (currentRotation.x < -0.5f || currentRotation.x > 0.5f))
        {
            m_up = false;
            m_down = false;

            // 회전 카운트가 0보다 클 시
            if (toiletPaperRoll.m_rollCount > 0)
            {
                toiletPaperRoll.m_rollCount--; // 회전 카운트 감소

                // 1부터 시작해서 0.3 까지 스케일 값이 점점 줄어듦
                m_nowScale = Mathf.Lerp(0.3f, 1f, toiletPaperRoll.m_rollCount / m_maxCount);
                transform.localScale = new Vector3(1f, m_nowScale, m_nowScale);
                
                // 인스턴스 프리팹 생성
                GameObject newPaper = Instantiate(m_paper,transform.position + new Vector3(0f, -0.5f, 0f), paperQuaternion, transform.parent);
                newPaper.transform.localScale = new Vector3(1f, m_nowScale + 0.5f, m_nowScale);
            }
        }

        // 화장지 회전 체크
        if (!m_up && currentRotation.x > 0.6f || currentRotation.x < -0.6f)
        {
            m_up = true;
        }
        if (!m_down && (currentRotation.x > 0f && currentRotation.x < -0.4f) || (currentRotation.x < 0f && currentRotation.x > -0.4f))
        {
            m_down = true;
        }
    }

    // 스핀 동작
    void Roll()
    {
        if (toiletPaperRoll.m_startTimer)
        {
            if (TouchManager.instance.IsDragDown())
            {
                m_rigidbody.angularVelocity = transform.TransformDirection(Vector3.left) * 100;
            }
        }
    }
}
