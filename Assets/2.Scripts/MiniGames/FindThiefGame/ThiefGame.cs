using UnityEngine;

public class ThiefGame : MiniGameSetting
{
    [SerializeField] private GameObject[] m_models;//캐릭터 스킨들
    [SerializeField] private GameObject m_thief;//태그가 Target인 도둑 오브젝트
    [SerializeField] private GameObject m_citizen;//태그가 Player인 시민 오브젝트
    [SerializeField] private GameObject m_tree;//캐릭터들이 숨을 나무
    [SerializeField] private GameObject m_mixingBox;//캐릭터들이 들어가있는 오브젝트
    [SerializeField] private GameObject m_disguiseEffect;//연기 이펙트


    private GameObject[] m_characters;//필드에 배치된 캐릭터들

    private int[,] m_board = new int[3, 4];//캐릭을 섞을 보드판
    private int m_characterNum = 4;//도둑 포함 캐릭터 개수 (첫번째 캐릭터가 도둑)
    private float m_maxTimer = 17.0f;//최대 시간
    private float m_timer;//타이머
    private bool m_once = true;//한번만 작동
    private bool m_suffle = true;//여러번 섞는 것을 방지
    private bool m_end = false;//게임 종료 확인 bool
    private int m_difficulty = 0;
    private float m_multiplier = 1.0f;//난이도에 따라 증가할 속도 배율 (섞는 속도)

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
        m_difficulty = m_difficulty1 * 3 + m_difficulty2 - 3;
        m_multiplier = 1.05f - (m_difficulty * 0.05f);//난이도가 증가할 수록 5% 빠르게

        CameraManager.Instance.ChangeCamera(CameraView.Angle90View);
        m_camera = CameraManager.Instance.GetCamera();

        m_characters = new GameObject[m_characterNum];

        //보드판 초기화
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                m_board[x, y] = 0;
            }
        }

        {//캐릭터 랜덤 배치
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

        {//캐릭터 생성
            int index = PlayerDataManager.instance.GetSkin();
            int randSkin = Random.Range(0, m_models.Length);

            //랜덤으로 고른 스킨이 착용 스킨과 동일할 때
            if (index == randSkin)
            {
                randSkin = randSkin + 1 > m_models.Length ? randSkin - 1 : randSkin + 1;//배열 초과하지 않는 선에서 번호를 한칸 내리거나 올리기
            }

            for (int i = 0; i < m_characters.Length; i++)
            {
                if (i < 1)//첫번째 캐릭터는 도둑
                {
                    m_characters[i] = Instantiate(m_thief, m_mixingBox.transform);//도둑 배치
                    Instantiate(m_models[randSkin], m_characters[i].transform);//스킨 적용
                }
                else
                {
                    m_characters[i] = Instantiate(m_citizen, m_mixingBox.transform);//시민 배치
                    Instantiate(m_models[index], m_characters[i].transform);//스킨 적용
                }

                //하단에 캐릭터들을 차례대로 나열
                Vector3 pos = new Vector3(i * 2 - 3.0f, -4.0f, -6.0f);
                m_characters[i].transform.position = pos;
            }
        }

        //나무 생성
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                GameObject obj = Instantiate(m_tree, m_mixingBox.transform);//나무들을 MixingBox에 배치
                Vector3 pos = new Vector3(x * 3 - 3.0f, -5.5f, y * 3 - 4.5f);
                obj.transform.position = pos;
            }
        }
    }

    private void Update()
    {
        //시간과 카운트 반영되는 코드
        m_timeText.text = m_timer.ToString("0.00");
        m_timer = m_timer <= 0 ? 0 : m_timer - Time.deltaTime;//타이머 감소

        //미션 Ui 출력
        if (m_timer < m_maxTimer - 1.0f && m_missionPrefab.activeSelf == false)
            m_missionPrefab.SetActive(true);
        if (m_timer < m_maxTimer - 2.0f && m_missionPrefab.activeSelf == true)
            m_missionPrefab.SetActive(false);


        if (m_timer < m_maxTimer - 2.5f && m_once)//시작후 3초뒤에 나무 뒤로 숨기
        {
            int index = PlayerDataManager.instance.GetSkin();
            Destroy(m_characters[0].transform.GetChild(0).gameObject);//도둑 변장
            Instantiate(m_models[index], m_characters[0].transform);//스킨 적용
            Instantiate(m_disguiseEffect, m_characters[0].transform);//연기 이펙트 출력
            Invoke("Hide", 2.0f);
            m_once = false;
        }

        if (m_timer < m_maxTimer - 6.0f && m_suffle)//5초 후 도둑 섞기
        {//1초마다 총 5번 실행되게
            Invoke("Suffle", 1.0f * m_multiplier);//약간의 시간차를 두어 도둑 섞기
            Invoke("Suffle", 2.0f * m_multiplier);//약간의 시간차를 두어 도둑 섞기
            Invoke("Suffle", 3.0f * m_multiplier);//약간의 시간차를 두어 도둑 섞기
            Invoke("Suffle", 4.0f * m_multiplier);//약간의 시간차를 두어 도둑 섞기
            Invoke("Suffle", 5.0f * m_multiplier);//약간의 시간차를 두어 도둑 섞기
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
                        EffectSoundManager.Instance.PlayEffect(27);
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

        if (m_timer <= 0.0f && !m_end)//못찾았을 경우
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
        //걷는 소리 출력
        EffectSoundManager.Instance.PlayEffect(20);

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (m_board[x, y] != 0)
                {
                    Vector3 pos = new Vector3(x * 3 - 3.0f, -4f, y * 3 - 4.5f);
                    ShuffleThief m = m_characters[m_board[x, y] - 1].transform.GetComponent<ShuffleThief>();
                    m.MoveToPoint(pos);
                }
            }
        }

        m_timer = 7.0f;
    }


    //캐릭터들을 나무에 숨기기
    private void Hide()
    {
        float speed = 1.0f + (m_difficulty * 0.2f);//이동속도도 난이도에 따라 빠르게

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (m_board[x, y] != 0)
                {
                    Vector3 pos = new Vector3(x * 3 - 3.0f, -4f, y * 3 - 4.5f);
                    ShuffleThief m = m_characters[m_board[x, y] - 1].transform.GetComponent<ShuffleThief>();
                    m.SetSpeed(speed);
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
