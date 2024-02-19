using UnityEngine;

public class RunningGame : MiniGameSetting
{
    [Header("Characters")]
    [SerializeField] private GameObject m_player;//플레이어 오브젝트
    [SerializeField] private GameObject[] m_npcs;//NPC들

    private Rigidbody m_playerRb;//플레이어의 Rigidbody
    private float m_maxSpeed;//플레이어의 최대 이동속도
    private float m_velocityZ;//플레이어의 현재속도
    private float m_speed = 10.0f;//플레이어의 이동속도
    private float m_maxTime = 20f;//제한 시간
    private float m_timer = 0f;//현재 시간
    private bool m_end = false;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_missionText.text = "결승선까지 달려라!";
        m_maxTime -= m_difficulty2;
        m_timer = m_maxTime;
        CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
        CameraManager.Instance.SetFollowTarget(m_player);
        CameraManager.Instance.SetFollowSpeed(10.0f);
        CameraManager.Instance.m_followEnabled = true;

        m_playerRb = m_player.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (m_timer < m_maxTime - 2.0f)//게임 시작하고 2초가 지나면
        {
            bool result = TouchManager.instance.IsBegan();
            if (result)
            {
                m_velocityZ = m_playerRb.velocity.z + m_speed;//플레이어의 현재 속도에 10만큼 더해준다.
                Vector3 vel = new Vector3(0.0f, 0.0f, m_velocityZ);
                m_playerRb.velocity = vel;
            }
        }

        foreach (GameObject obj in m_npcs)
        {
            MoveCharacters(obj, 10.0f);//NPC들을 일정 속도로 계속 달리게 
        }
    }

    // Update is called once per frame
    private void Update()
    {
        m_timeText.text = m_timer.ToString("0.00");


        m_timer = m_timer <= 0 ? 0 : m_timer - Time.deltaTime;
        if (m_timer < m_maxTime - 0.5f && m_missionPrefab.activeSelf == false)
        {
            m_missionPrefab.SetActive(true);
        }
        if (m_timer < m_maxTime - 1.5 && m_missionPrefab.activeSelf == true)
        {
            m_missionPrefab.SetActive(false);
        }

        if (m_timer < m_maxTime - 2.0f)
            m_timePrefab.SetActive(true);

        if (!m_end)
        {
            //게임 패배조건 - 제한시간내로 골에 도달하지 못했을 경우
            if (m_timer <= 0.0f)
            {
                EffectSoundManager.Instance.PlayEffect(22);
                m_failPrefab.SetActive(true);
                Invoke("GameFail", 1);
                m_end = true;
            }
        }
    }

    private void MoveCharacters(GameObject character, float speed)
    {
        Rigidbody rb = character.GetComponent<Rigidbody>();
        Vector3 vel = new Vector3(0.0f, 0.0f, speed);
        rb.velocity = vel;
    }

    public void Win()
    {
        EffectSoundManager.Instance.PlayEffect(21);
        m_clearPrefab.SetActive(true);
        Invoke("GameClear", 1);
        m_end = true;
    }


}
