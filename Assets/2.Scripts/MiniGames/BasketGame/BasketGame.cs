using UnityEngine;

public class BasketGame : MiniGameSetting
{

    [SerializeField] private GameObject m_cheese;//치즈 프리펩
    [SerializeField] private GameObject m_banana;//바나나 프리펩
    [SerializeField] private GameObject m_cherry;//체리 프리펩
    [SerializeField] private GameObject m_trash;//쓰레기 프리펩

    [SerializeField] private GameObject m_foodContainer;//음식 생성 부모 오브젝트
    [SerializeField] private GameObject m_player;//이동가능한 캐릭터

    [SerializeField] private GameObject m_leftWall;//왼쪽 벽
    [SerializeField] private GameObject m_rightWall;//오른쪽 벽

    private Rigidbody m_rigidbody;

    private int m_catchCounts = 0;//박스에 음식을 담은 개수
    private float m_speed = 4.5f;//캐릭터 이동속도
    private int m_difficulty = 1;
    private Vector3 m_velocity;//캐릭터 이동 방향
    private float m_lastTime = 0.0f;//음식을 생성하고 지난 시간
    private float m_screenWidth;//화면 해상도에 따른 오른쪽 끝의 World Point

    private int m_catchCount;
    private float m_timer;
    private bool m_end = false;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_difficulty = m_difficulty1 * 3 + m_difficulty2 - 3;//난이도 세팅
        m_catchCount = m_difficulty + 2;//받아야 하는 음식 갯수 초기화

        m_missionText.text = "떨어지는 음식을 바구니에 담자!";

        CameraManager.Instance.ChangeCamera(CameraView.ZeroView);//90도 각도로 내려다 보는 카메라로 변경
        m_rigidbody = m_player.GetComponent<Rigidbody>();

        //화면 해상도 기준 오른쪽 끝에 레이 발사
        Vector3 pos = new Vector3(Screen.width, Screen.height);
        Ray ray = CameraManager.Instance.GetCamera().ScreenPointToRay(pos);
        RaycastHit hit;

        //좌우 벽을 화면 끝에 배치하는 기능
        if (Physics.Raycast(ray, out hit))
        {
            m_screenWidth = hit.point.x - 0.2f;
            Vector3 screenLeft = new Vector3(-m_screenWidth, 9f, 5.0f);
            Vector3 screenRight = new Vector3(m_screenWidth, 9f, 5.0f);
            m_leftWall.transform.position = screenLeft;
            m_rightWall.transform.position = screenRight;
        }
        m_screenWidth -= 0.7f;//음식 생성 범위 조정 /음식이 벽 Collider에 튕기는 경우가 생겨서 벽보다 조금 안쪽에 생성되게
        m_timer = 12f; // 12초 초기화
    }

    private void FixedUpdate()
    {
        if (TouchManager.instance.IsHolding())//화면을 터치하고 있을 때
        {
            float direction = Input.mousePosition.x - ((float)Screen.width / 2);//화면을 절반으로 나누어 왼쪽, 오른쪽 부분 어디를 선택하였는지 체크

            if (direction > 0)//오른쪽을 클릭했을 때
            {
                Vector3 velocity = new Vector3(m_speed, 0, 0);
                m_player.transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);//캐릭터를 오른쪽으로 회전
                m_velocity = velocity;
            }
            else//왼쪽을 클릭했을 때
            {
                Vector3 velocity = new Vector3(-m_speed, 0, 0);
                m_player.transform.rotation = Quaternion.LookRotation(Vector3.left, Vector3.up);//캐릭터를 왼쪽으로 회전
                m_velocity = velocity;
            }

            try
            {
                m_rigidbody.velocity = m_velocity;//Rigidbody가 없을 경우 오류 발생
            }
            catch
            {
                Debug.Log("Player's Rigidbody is Null");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_lastTime += Time.deltaTime;
        if (m_lastTime > 1)//음식은 1초마다 생성
        {
            float randomTime = Random.Range(0.0f, 2.0f);//음식을 즉시 생성하거나 n초뒤에 생성하는 시간
            Invoke("GenerateFoods", randomTime);

            m_lastTime = 0;//음식을 생성하고 다시 0으로 초기화
        }

        #region   //게임 시간별 로직 + 성공실패 관리
        //시간과 카운트 반영되는 코드
        m_timeText.text = m_timer.ToString("0.00");
        m_countText.text = m_catchCounts.ToString() + "/" + m_catchCount.ToString();

        if (!m_end)
        {
            m_timer = m_timer <= 0 ? 0 : m_timer - Time.deltaTime;
        }


        //게임 시작 후 미션을 보여주고 나서 1초 후 지움
        if (m_timer < 11.5 && m_missionPrefab.activeSelf == false)
            m_missionPrefab.SetActive(true);
        if (m_timer < 10.5 && m_missionPrefab.activeSelf == true)
            m_missionPrefab.SetActive(false);

        //2초 후 부터 실제 게임시작 - 시간제한과 클리어를 위한 카운트 ui를 출력
        if (m_timer < 10f)
        {
            m_timer = 10f;

            m_timePrefab.SetActive(true);
            m_countPrefab.SetActive(true);
        }
        #endregion

    }


    //랜덤으로 과일을 생성하는 함수
    private void GenerateFoods()
    {
        int index = Random.Range(0, 4);//쓰레기가 스폰될 확률
        Vector3 randomPos = new Vector3(Random.Range(-(m_screenWidth), (m_screenWidth)), 10.0f, 8.0f);//무작위 x 이동
        Quaternion randomRot = Quaternion.Euler(0.0f, Random.Range(0.0f, 180.0f), 0.0f);//무작위 y 회전

        GameObject food;

        //음식은 미니게임 오브젝트의 첫번째 자식인 FoodContainer에 넣음
        switch (index)
        {
            case 0:
                food = Instantiate(m_cheese, m_foodContainer.transform);
                break;
            case 1:
                food = Instantiate(m_banana, m_foodContainer.transform);
                break;
            case 2:
                food = Instantiate(m_cherry, m_foodContainer.transform);
                break;

            default://이외에는 쓰레기를 생성
                food = Instantiate(m_trash, m_foodContainer.transform);
                break;
        }

        if (food != null)
        {
            food.transform.position = randomPos;
            food.transform.rotation = randomRot;
        }
    }

    //클리어 조건을 충족하였는지 체크하는 함수
    public bool CheckClear()
    {
        bool result = m_catchCounts < m_catchCount ? false : true;

        return result;
    }

    //클리어 조건을 충족하였을 때 호출
    public void Win()
    {
        if (!m_end)
        {
            m_clearPrefab.SetActive(true);
            Invoke("GameClear", 1);
            EffectSoundManager.Instance.PlayEffect(21);
            m_end = true;
        }
    }

    //쓰레기를 바구니에 담게되면 호출
    public void Lose()
    {
        if (!m_end)
        {
            m_failPrefab.SetActive(true);
            Invoke("GameFail", 1);
            EffectSoundManager.Instance.PlayEffect(22);
            m_end = true;
        }
    }

    //잡은 갯수 증가
    public void AddCount()
    {
        m_catchCounts++;
    }

    //난이도 설정
    public void SetLevel(int difficulty)
    {
        m_difficulty = difficulty;
    }
}
