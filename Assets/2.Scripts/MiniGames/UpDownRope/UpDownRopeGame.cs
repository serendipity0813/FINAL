using UnityEngine;

[DefaultExecutionOrder(1)]
public class UpDownRopeGame : MiniGameSetting
{
    [HideInInspector] public int clearCount;
    [HideInInspector] public int difficulty;
    [HideInInspector] public float timer;
    [SerializeField] private GameObject m_player;
    [SerializeField] private GameObject m_obstacle;

    private Rigidbody m_playerRigidbody;
    private Vector3 m_upVelocity;//사다리 오르는 방향
    private Vector3 m_downVelocity;//내려가는 방향
    private float m_climbSpeed = 5.0f;//오르고 내려가는 속도
    private bool m_toggle = false;

    private bool m_end = false;

    protected override void Awake()
    {
        /*부모클래스 필드 및 메소드 받아오기
         인게임 UI 목록 : m_missionUI, m_timeUI, m_countUI, m_clearUI, m_failUI
         인게임 Text 목록 : m_missionText, m_timeText, m_timeText, m_countText, m_countText
         배열의 경우 0번은 내용(남은시간, 남은횟수 등), 1번은 숫자 입니다.
         인게임 Method 목록 : GameClear(); , GameFail(); */

        base.Awake();
    }

    private void Start()
    {
        //카메라 설정
        CameraManager.Instance.ChangeCamera(CameraView.Angle30View);
        CameraManager.Instance.SetFollowSpeed(10.0f);
        CameraManager.Instance.SetFollowTarget(m_player);
        CameraManager.Instance.m_followEnabled = true;
        EffectSoundManager.Instance.PlayAudioLoop(29);//사다리 타는 효과음 루프 적용

        AnimatorUpdater animator = null;

        //캐릭터의 애니메이션 스크립트를 받아오는 부분
        for (int i = 0; i < m_player.transform.childCount; i++)
        {
            bool result = m_player.transform.GetChild(i).gameObject.activeSelf;

            if (result)//SetActive가 true 일 경우 반복 탈출
            {
                animator = m_player.transform.GetChild(i).GetComponent<AnimatorUpdater>();
                break;
            }
        }

        if (animator != null)
        {
            animator.ClimbCharacter();//사다리 모션으로 변경
        }

        //인게임 text내용 설정 + 게임 승리조건
        clearCount = 1 + ((m_difficulty1 + m_difficulty2) / 3);
        m_missionText.text = "장애물을 피해 바닥까지 내려가자!";
        difficulty = m_difficulty1;

        m_playerRigidbody = m_player.GetComponent<Rigidbody>();

        //Velocity 방향 초기화
        m_upVelocity = Vector3.up * m_climbSpeed;
        m_downVelocity = Vector3.down * m_climbSpeed;

        for (int i = 1; i <= m_difficulty2 + m_difficulty1 - 1; i++)
        {
            Instantiate(m_obstacle, m_obstacle.transform.position, Quaternion.identity, transform);
        }

    }

    private void FixedUpdate()
    {
        if (timer > 2 && !m_end)
        {
            //마우스를 클릭할 때 마우스 위치를 받아온 후 위쪽 클릭중이면 올라가고 아래쪽 클릭중이면 내려가도록 함
            bool result = TouchManager.instance.IsHolding();

            if (result)
            {
                float direction = Input.mousePosition.y - ((float)Screen.height / 2);

                if (direction > 0)
                {
                    m_playerRigidbody.velocity = m_upVelocity;//위쪽 클릭 시 위로 이동
                }
                else
                {
                    m_playerRigidbody.velocity = m_downVelocity;//아래 클릭 시 아래로 이동
                }

                if (!m_toggle) //false에서 true로 바뀔 때 
                {
                    EffectSoundManager.Instance.PlayLoop();
                    m_toggle = true;
                }
            }
            else
            {
                if (m_toggle) //true에서 false로 바뀔 때 
                {
                    EffectSoundManager.Instance.StopLoop();
                    m_toggle = false;
                }
            }
        }
    }

    private void Update()
    {

        //시간과 카운트 반영되는 코드
        m_timeText.text = (17 - timer).ToString("0.00");
        //m_countText.text = clearCount.ToString();

        //게임 시작 후 미션을 보여주고 나서 1초 후 지움
        if (!m_end)
        {
            timer = timer >= 17 ? 17 : timer + Time.deltaTime;
        }
        if (timer > 0.5 && m_missionPrefab.activeSelf == false)
            m_missionPrefab.SetActive(true);
        if (timer > 1.5 && m_missionPrefab.activeSelf == true)
            m_missionPrefab.SetActive(false);

        //2초 후 부터 실제 게임시작 - 시간제한과 클리어를 위한 카운트 ui를 출력
        if (timer > 2)
        {
            m_timePrefab.SetActive(true);
            //m_countPrefab.SetActive(true);
        }

        if (!m_end)
        {
            ////게임 승리조건
            //if (m_positiony < -32 && clearCount > 0)
            //{
            //    EffectSoundManager.Instance.PlayEffect(21);
            //    m_clearPrefab.SetActive(true);
            //    timer = 10;
            //    Invoke("GameClear", 1);
            //    m_end = true;
            //}

            //게임 패배조건
            if (timer > 17 || clearCount <= 0)
            {
                EffectSoundManager.Instance.PlayEffect(22);
                m_failPrefab.SetActive(true);
                Invoke("GameFail", 1);
                m_end = true;
                if (timer > 17)
                {
                    timer = 17;
                }
            }
        }
    }

    //제한시간 내로 결승선 Collider에 들어갔을 때 승리
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !m_end)
        {
            EffectSoundManager.Instance.PlayEffect(21);
            m_clearPrefab.SetActive(true);
            Invoke("GameClear", 1);
            m_end = true;
        }
    }
}
