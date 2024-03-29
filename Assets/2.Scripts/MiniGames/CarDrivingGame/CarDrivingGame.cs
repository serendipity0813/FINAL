using UnityEngine;

public class CarDrivingGame : MiniGameSetting
{
    [SerializeField] private GameObject m_player;

    private Rigidbody m_playerBody;
    private CarGenerator m_generator;

    private float m_runningSpeed = 20.0f;
    private float m_timer = 0.0f;
    private bool m_end = false;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    private void Start()
    {
        CameraManager.Instance.ChangeCamera(CameraView.Angle30View);
        CameraManager.Instance.SetFollowSpeed(10.0f);
        CameraManager.Instance.SetFollowTarget(m_player);
        CameraManager.Instance.m_followEnabled = true;

        m_playerBody = m_player.GetComponent<Rigidbody>();
        m_generator = transform.GetChild(2).GetComponent<CarGenerator>();
        m_generator.GenerateCars();//난이도에 맞춰서 차 생성

        m_missionText.text = "차를 피해서 목표에 도달하자!";

    }

    private void FixedUpdate()
    {
        if (m_timer > 3.0f)//3초 지나고 시작
        {
            try
            {
                Vector3 velocity;
                Vector3 pos;
                velocity = new Vector3(0, 0, m_runningSpeed);
                pos = m_player.transform.position;

                if (TouchManager.instance.IsBegan())
                {
                    float direction = Input.mousePosition.x - ((float)Screen.width / 2);//화면을 절반으로 나누어 왼쪽, 오른쪽 부분 어디를 선택하였는지 체크

                    if (direction > 0)//오른쪽 클릭
                    {
                        if (m_player.transform.position.x < 1)
                        {
                            pos.x = 1.5f;
                            EffectSoundManager.Instance.PlayEffect(26);
                        }
                    }
                    else//왼쪽 클릭
                    {
                        if (m_player.transform.position.x > -1)
                        {
                            pos.x = -1.5f;
                            EffectSoundManager.Instance.PlayEffect(26);
                        }
                    }

                    m_player.transform.position = pos;
                }
                else
                {
                    velocity.x = 0.0f;
                }

                m_playerBody.velocity = velocity;
            }
            catch
            {
                Debug.Log("Player's Rigidbody is Null");
            }
        }

    }

    private void Update()
    {
        #region   //게임 시간별 로직 + 성공실패 관리
        //시간과 카운트 반영되는 코드
        m_timeText.text = (m_timer - 3).ToString("0.00");
        m_countText.text = "1";

        //게임 시작 후 미션을 보여주고 나서 1초 후 지움
        m_timer += Time.deltaTime;
        if (m_timer > 1 && m_missionPrefab.activeSelf == false)
            m_missionPrefab.SetActive(true);
        if (m_timer > 2 && m_missionPrefab.activeSelf == true)
            m_missionPrefab.SetActive(false);

        #endregion
    }

    public int GetDifficulty()
    {
        int m_difficulty = 1;

        m_difficulty = m_difficulty1 * 3 + m_difficulty2 - 3;

        return m_difficulty;
    }

    public void Win()
    {
        if (!m_end)
        {
            EffectSoundManager.Instance.PlayEffect(21);
            CameraManager.Instance.m_followEnabled = false;
            m_clearPrefab.SetActive(true);
            Invoke("GameClear", 1);
            m_end = true;
        }

    }

    public void Lose()
    {
        if (!m_end)
        {
            EffectSoundManager.Instance.PlayEffect(22);
            CameraManager.Instance.m_followEnabled = false;
            m_failPrefab.SetActive(true);
            Invoke("GameFail", 1);
            m_end = true;
        }
    }


}
