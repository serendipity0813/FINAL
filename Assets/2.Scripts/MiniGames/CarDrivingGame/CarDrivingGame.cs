using UnityEditor;
using UnityEngine;

public class CarDrivingGame : MiniGameSetting
{
    [SerializeField] private GameObject m_cameraTarget;

    private CharacterController m_playerController;
    private CarGenerator m_generator;
    private int m_difficulty = 1;

    private float m_runningSpeed = 0.4f;
    private float m_sideSpeed = 0.2f;
    private float m_timer = 0.0f;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    private void Start()
    {
        CameraManager.Instance.ChangeCamera(CameraView.Angle30View);
        CameraManager.Instance.SetFollowSpeed(10.0f);
        CameraManager.Instance.SetFollowTarget(m_cameraTarget.transform.GetChild(0).gameObject);
        CameraManager.Instance.m_followEnabled = true;

        m_playerController = m_cameraTarget.GetComponent<CharacterController>();
        m_generator = transform.GetChild(2).GetComponent<CarGenerator>();
        m_generator.GenerateCars();//난이도에 맞춰서 차 생성

        m_missionText.text = "차를 피해서 목표에 도달하자!";

    }

    private void FixedUpdate()
    {
        m_timer += Time.deltaTime;

        if (m_timer > 3.0f)//3초 지나고 시작
        {
            try
            {
                m_playerController.Move(new Vector3(0, 0, m_runningSpeed));

                if (TouchManager.instance.IsHolding())
                {
                    float direction = Input.mousePosition.x - ((float)Screen.width / 2);//화면을 절반으로 나누어 왼쪽, 오른쪽 부분 어디를 선택하였는지 체크

                    if (direction > 0)
                    {
                        m_playerController.Move(new Vector3(m_sideSpeed, 0, 0));
                    }
                    else
                    {
                        m_playerController.Move(new Vector3(-m_sideSpeed, 0, 0));
                    }
                }
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
        m_timeText.text = (m_timer-3).ToString("0.00");

        //게임 시작 후 미션을 보여주고 나서 1초 후 지움
        m_timer += Time.deltaTime;
        if (m_timer > 1 && m_missionPrefab.activeSelf == false)
            m_missionPrefab.SetActive(true);
        if (m_timer > 2 && m_missionPrefab.activeSelf == true)
            m_missionPrefab.SetActive(false);

        //2초 후 부터 실제 게임시작 - 시간제한과 클리어를 위한 카운트 ui를 출력
        if (m_timer > 3)
        {
            m_timePrefab.SetActive(true);
            m_countPrefab.SetActive(true);
        }

      
        #endregion
    }

    public int GetDifficulty()
    {
        m_difficulty = m_difficulty1 * 3 + m_difficulty2 - 3;
        return m_difficulty;
    }

    public void Win()
    {
        CameraManager.Instance.m_followEnabled = false;
        m_clearPrefab.SetActive(true);
        Invoke("GameClear", 1);
    }

    public void Lose()
    {
        CameraManager.Instance.m_followEnabled = false;
        m_failPrefab.SetActive(true);
        Invoke("GameFail", 1);
    }


}
