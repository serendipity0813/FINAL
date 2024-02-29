using UnityEngine;

public class UpDownSlimeGame : MiniGameSetting
{
    [SerializeField] private GameObject[] m_slimeSpawner;
    [SerializeField] private GameObject m_slime;
    private Camera m_camera;
    private int m_clearCount;
    private float m_timer;
    private float m_slimeSpawnRate;
    private bool m_end;

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
        CameraManager.Instance.ChangeCamera(CameraView.Angle60View);
        m_camera = CameraManager.Instance.GetCamera();

        //인게임 text내용 설정 + 게임 승리조건
        m_clearCount = 6 + m_difficulty1*2;
        m_slimeSpawnRate = 0.4f;
        m_missionText.text = "튀어나오는 슬라임을 잡아라!";
        InvokeRepeating("MakeSlime", 2.0f, m_slimeSpawnRate);

    }

    private void Update()
    {

        #region   //게임 시간별 로직 + 성공실패 관리
        //시간과 카운트 반영되는 코드
        m_timeText.text = (12 - m_timer).ToString("0.00");
        m_countText.text = m_clearCount.ToString();

        //게임 시작 후 미션을 보여주고 나서 1초 후 지움
        if(!m_end)
            m_timer += Time.deltaTime;

        if (m_timer > 0.5 && m_missionPrefab.activeSelf == false)
            m_missionPrefab.SetActive(true);
        if (m_timer > 1.5 && m_missionPrefab.activeSelf == true)
            m_missionPrefab.SetActive(false);

        //2초 후 부터 실제 게임시작 - 시간제한과 클리어를 위한 카운트 ui를 출력
        if (m_timer > 2)
        {
            m_timePrefab.SetActive(true);
            m_countPrefab.SetActive(true);
        }

        //게임 승리조건
        if (m_clearCount <= 0)
        {
            if(!m_end)
            {
                m_end = true;
                m_clearPrefab.SetActive(true);
                EffectSoundManager.Instance.PlayEffect(21);
                CancelInvoke("MakeSlime");
                Invoke("GameClear", 1);
            }
        }

        //게임 패배조건
        if (m_timer >= 12)
        {
            if(!m_end)
            {
                m_end = true;
                m_timer = 12;
                m_failPrefab.SetActive(true);
                EffectSoundManager.Instance.PlayEffect(22);
                CancelInvoke("MakeSlime");
                Invoke("GameFail", 1);
            }
  
        }
        #endregion

        if (Input.GetMouseButtonDown(0) && m_timer > 2 && !m_end)
        {
            Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Target")
                {
                    EffectSoundManager.Instance.PlayEffect(27);
                    hit.collider.gameObject.SetActive(false);
                    if(m_clearCount > 0 && m_end == false)    
                      m_clearCount--;
                }
            }
        }
    }

    private void MakeSlime()
    {
        if (!m_end)
            Instantiate(m_slime, m_slimeSpawner[Random.Range(0, 25)].transform.position, Quaternion.identity, transform);
    }

}
