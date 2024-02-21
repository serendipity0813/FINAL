using UnityEngine;

public class ThiefGame : MiniGameSetting
{
    [SerializeField] private GameObject[] m_models;//캐릭터 스킨들
    [SerializeField] private GameObject m_player;//현재 플레이어가 선택한 캐릭터
    [SerializeField] private GameObject m_tree;//캐릭터들이 숨을 나무
    [SerializeField] private GameObject m_mixingBox;//캐릭터들이 모여있는 풀


    private GameObject[] m_thiefs;//필드에 배치된 캐릭터들

    private int[,] m_board = new int[3, 4];//캐릭을 섞을 보드판
    private int m_characterNum = 4;//도둑 포함 캐릭터 개수 (첫번째 캐릭터가 도둑)
    private float m_maxTimer = 20.0f;//최대 시간
    private float m_timer;//타이머
    private bool m_once = true;//한번만 작동
    private bool m_suffle = true;//여러번 섞는 것을 방지
    private bool m_end = false;//게임 종료 확인 bool

    private Camera m_camera;//현재 포커싱 카메라

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_missionText.text = "진짜 도둑을 잡아라!";
        m_timer = m_maxTimer;

        CameraManager.Instance.ChangeCamera(CameraView.Angle90View);
        m_camera = CameraManager.Instance.GetCamera();

        m_thiefs = new GameObject[m_characterNum];

        //보드판 초기화
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                m_board[x, y] = 0;
            }
        }

        {
            int CharacterNum = 1;
            while (CharacterNum <= m_characterNum)//배치된 캐릭터 수가 적을 경우
            {
                int x = Random.Range(0, 3);//랜덤 x 위치
                int y = Random.Range(0, 4);//랜덤 y 위치

                //자리가 이미 있는 경우 반복 처음으로 돌아가기
                if (m_board[x, y] != 0)
                {
                    continue;
                }

                m_board[x, y] = CharacterNum;//랜덤 위치에 캐릭터 배치 (캐릭터 번호)
                CharacterNum++;//캐릭터 번호 증가
            }
        }




        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                GameObject obj = Instantiate(m_tree, m_mixingBox.transform);//나무들을 MixingBox에 배치
                Vector3 pos = new Vector3(x * 3 - 3.0f, -5.5f, y * 3 - 4.5f);
                obj.transform.position = pos;
            }
        }


        {
            int count = 0;

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (m_board[x, y] != 0)
                    {
                        Vector3 pos = new Vector3(x * 3 - 3.0f, -4f, y * 3 - 6f);

                        m_thiefs[count] = Instantiate(m_player, m_mixingBox.transform);//캐릭터들을 배치
                        m_thiefs[count].transform.position = pos;
                        count++;
                    }
                }
            }
        }

        //캐릭터 스킨 입히기 Test 랜덤으로 입히게 해두었음
        foreach (GameObject obj in m_thiefs)
        {
            int index = Random.Range(0, m_models.Length);
            Instantiate(m_models[index], obj.transform);
        }
    }

    private void Update()
    {
        //시간과 카운트 반영되는 코드
        m_timeText.text = m_timer.ToString("0.00");
        m_timer = m_timer <= 0 ? 0 : m_timer - Time.deltaTime;//타이머 감소
        Debug.Log(m_timer);

        if (m_timer < m_maxTimer - 1.0f && m_missionPrefab.activeSelf == false)
            m_missionPrefab.SetActive(true);
        if (m_timer < m_maxTimer - 2.0f && m_missionPrefab.activeSelf == true)
            m_missionPrefab.SetActive(false);


        if (m_timer < m_maxTimer - 3.0f && m_once)
        {
            Hide();
            m_once = false;
        }

        if (m_timer < m_maxTimer - 6.0f && m_suffle)
        {//1초마다 총 5번 실행되게
            Invoke("Suffle", 1.0f);//약간의 시간차를 두어 도둑 섞기
            Invoke("Suffle", 2.0f);//약간의 시간차를 두어 도둑 섞기
            Invoke("Suffle", 3.0f);//약간의 시간차를 두어 도둑 섞기
            Invoke("Suffle", 4.0f);//약간의 시간차를 두어 도둑 섞기
            Invoke("Suffle", 5.0f);//약간의 시간차를 두어 도둑 섞기
            m_suffle = false;
        }

        if (m_timer < 5.0f && !m_end)//플레이어에게 5초간 찾을 시간을 준다. & 게임이 종료되지 않았을 경우
        {
            //타이머 활성화
            m_timePrefab.SetActive(true);


            //화면 어딘가 터치 했을 때
            if (TouchManager.instance.IsBegan())
            {
                //마우스 클릭시 RAY를 활용하여 타겟 찾기
                Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Target")//타겟을 터치했을 경우 승리
                    {
                        EffectSoundManager.Instance.PlayEffect(21);
                        m_clearPrefab.SetActive(true);
                        Invoke("GameClear", 1);
                        m_end = true;
                    }
                    else//다른곳을 터치했을 때 게임 오버
                    {
                        EffectSoundManager.Instance.PlayEffect(22);
                        m_failPrefab.SetActive(true);
                        Invoke("GameFail", 1);
                        m_end = true;
                    }
                }
            }

        }

        if (m_timer <= 0.0f && !m_end)//5초가 지나고 못찾았을 경우
        {
            EffectSoundManager.Instance.PlayEffect(22);
            m_failPrefab.SetActive(true);
            Invoke("GameFail", 1);
            m_end = true;
        }
    }

    private void Suffle()
    {
        bool result = false;
        while (true)
        {
            result = FindDirection();//경로 탐색

            if (result)//성공 했을 경우 탈출
                break;
        }


        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (m_board[x, y] != 0)
                {
                    Vector3 pos = new Vector3(x * 3 - 3.0f, -4f, y * 3 - 4.5f);
                    ShuffleThief m = m_thiefs[m_board[x, y] - 1].transform.GetComponent<ShuffleThief>();
                    m.MoveToPoint(pos);
                }
            }
        }
    }

    //캐릭터들을 나무에 숨기기
    private void Hide()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (m_board[x, y] != 0)
                {
                    Vector3 pos = new Vector3(x * 3 - 3.0f, -4f, y * 3 - 4.5f);
                    ShuffleThief m = m_thiefs[m_board[x, y] - 1].transform.GetComponent<ShuffleThief>();
                    m.MoveToPoint(pos);
                }
            }
        }
    }

    //경로 탐색 함수
    private bool FindDirection()
    {
        int[,] tempBoard = new int[3, 4];

        //보드판 초기화
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                tempBoard[x, y] = 0;
            }
        }

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (m_board[x, y] != 0)
                {
                    while (true)
                    {
                        int direction = Random.Range(0, 8);//랜덤 8 방향

                        int dX = 0, dY = 0;//direction X, Y

                        switch (direction)
                        {
                            case 0: //왼쪽 위 ↖
                                dX = -1;
                                dY = +1;
                                break;

                            case 1: //위쪽 ↑
                                dX = 0;
                                dY = +1;
                                break;
                            case 2: //오른쪽 위 ↗
                                dX = +1;
                                dY = +1;
                                break;
                            case 3: //왼쪽 ←
                                dX = -1;
                                dY = 0;
                                break;
                            case 4: //오른쪽 →
                                dX = +1;
                                dY = 0;
                                break;
                            case 5: //아래 왼쪽 ↙
                                dX = -1;
                                dY = -1;
                                break;
                            case 6: //아래쪽 ↓
                                dX = 0;
                                dY = -1;
                                break;
                            case 7: //아래 오른쪽 ↘
                                dX = +1;
                                dY = -1;
                                break;
                        }


                        if (x + dX >= 0 && x + dX < 3 && y + dY >= 0 && y + dY < 4)//배열 범위를 벗어나지 않을 때
                        {
                            tempBoard[x + dX, y + dY] = m_board[x, y];
                            break;//이동할 위치에 캐릭터 배치 후 반복문 빠져나가기
                        }
                    }
                }
            }
        }

        int count = 0;
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (tempBoard[x, y] != 0)
                {
                    count++;
                }
            }
        }

        if (count < m_characterNum)//배치된 캐릭터 수가 적을 경우 false 리턴
        {
            return false;
        }

        m_board = tempBoard;

        return true;
    }
}
