using UnityEngine;

public class BasketGame : MiniGameSetting
{

    [SerializeField] private GameObject m_cheese;//치즈 프리펩
    [SerializeField] private GameObject m_banana;//바나나 프리펩
    [SerializeField] private GameObject m_cherry;//체리 프리펩
    [SerializeField] private GameObject m_trash;//쓰레기 프리펩

    [SerializeField] private GameObject m_player;//이동가능한 캐릭터

    private Rigidbody m_rigidbody;

    private int m_catchCounts = 0;//박스에 음식을 담은 개수
    private float m_speed = 4.0f;//캐릭터 이동속도
    private int m_difficulty = 1;
    private Vector3 m_velocity;//캐릭터 이동 방향
    private float m_lastTime = 0.0f;//음식을 생성하고 지난 시간

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        CameraManager.Instance.ChangeCamera(CameraView.ZeroView);//90도 각도로 내려다 보는 카메라로 변경
        m_rigidbody = m_player.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        if (TouchManager.instance.IsHolding())
        {
            float direction = Input.mousePosition.x - ((float)Screen.width / 2);

            if (direction > 0)
            {
                m_velocity = new Vector3(m_speed, 0, 0);
            }
            else
            {
                m_velocity = new Vector3(-m_speed, 0, 0);
            }


            try
            {
                m_rigidbody.velocity = m_velocity;
            }
            catch
            {
                Debug.Log("Player's Rigidbody is Null");
            }
        }

        m_lastTime += Time.deltaTime;
        if (m_lastTime > 1)//음식은 1초마다 생성
        {
            float randomTime = Random.Range(0.0f, 2.0f);//음식을 즉시 생성하거나 n초뒤에 생성하는 시간
            Invoke("GenerateFoods", randomTime);

            m_lastTime = 0;//음식을 생성하고 다시 0으로 초기화
        }
    }


    //랜덤으로 과일을 생성하는 함수
    private void GenerateFoods()
    {
        int index = Random.Range(0, 3 + m_difficulty);//난이도만큼 쓰레기가 스폰될 확률 증가
        Vector3 randomPos = new Vector3(Random.Range(-2.0f, 2.0f), 8.0f, 8.0f);//무작위 x 이동
        Quaternion randomRot = Quaternion.Euler(0.0f, Random.Range(0.0f, 180.0f), 0.0f);//무작위 y 회전

        GameObject food;

        //음식은 미니게임 오브젝트의 첫번째 자식인 FoodContainer에 넣음
        switch (index)
        {
            case 0:
                food = Instantiate(m_cheese, transform.GetChild(0));
                break;
            case 1:
                food = Instantiate(m_banana, transform.GetChild(0));
                break;
            case 2:
                food = Instantiate(m_cherry, transform.GetChild(0));
                break;

            default://이외에는 쓰레기를 생성
                food = Instantiate(m_trash, transform.GetChild(0));
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
        bool result = false;

        //난이도 만큼 잡아야 하는 과일 개수를 못채웠을 경우 false 리턴
        switch (m_difficulty)
        {
            case 1:
                result = m_catchCounts < 3 ? false : true;
                break;
            case 2:
                result = m_catchCounts < 5 ? false : true;
                break;
            case 3:
                result = m_catchCounts < 7 ? false : true;
                break;
            case 4:
                result = m_catchCounts < 9 ? false : true;
                break;
            case 5:
                result = m_catchCounts < 10 ? false : true;
                break;
        }

        return result;
    }

    //클리어 조건을 충족하였을 때 호출
    public void Win()
    {
        GameClear();
    }

    //쓰레기를 바구니에 담게되면 호출
    public void Lose()
    {
        GameFail();
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
