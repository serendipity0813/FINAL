using Unity.VisualScripting;
using UnityEngine;

public class MiniDefenceGame : MiniGameSetting
{
    [SerializeField] private GameObject[] m_shootingBalls;
    [SerializeField] private GameObject[] m_MonsterSpawner;
    [SerializeField] private GameObject m_BallSpawner;
    [SerializeField] private GameObject m_Monster;

    private Rigidbody m_shootingBallRigidBody;
    private Camera m_camera;
    private Vector3 m_shootingBallPosition;

    private float m_timer;
    private bool m_end;
    private int m_shootingBallIndex = 0;
    private float m_spawnRate;
    public float m_monsterSpeed;

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
        CameraManager.Instance.ChangeCamera(CameraView.Angle90View);
        m_camera = CameraManager.Instance.GetCamera();

        //인게임 text내용 설정 + 게임 승리조건
        m_missionText.text = "음식을 발사하여 바이킹을 막아라!";
        m_spawnRate = 1f - m_difficulty1 * 0.2f;
        m_monsterSpeed = 0.04f + m_difficulty2 * 0.03f;
        InvokeRepeating("MakeMonster", 2, m_spawnRate);

    }

    private void Update()
    {
        #region   //게임 시간별 로직 + 성공실패 관리
        //시간과 카운트 반영되는 코드
        m_timeText.text = (12 - m_timer).ToString("0.00");

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
        }

        if(!m_end)
        {
            //게임 승리조건
            if (m_timer >= 12)
            {
                m_end = true;
                m_clearPrefab.SetActive(true);
                EffectSoundManager.Instance.PlayEffect(21);
                CancelInvoke("MakeMonster");
                Invoke("GameClear", 1);
                m_timer = 12f;
            }
        }

        #endregion

        bool result = TouchManager.instance.IsBegan();

        if (result && m_timer > 2 && m_end == false && Time.timeScale > 0f)
        {
            Vector3 point = m_camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, Input.mousePosition.z));

            m_shootingBallPosition = m_BallSpawner.transform.position;
            m_shootingBallPosition.x = point.x;
            ShootReady();
            m_shootingBallRigidBody.AddForce(new Vector3(0, 0, 1) * 20.0f, ForceMode.Impulse);

        }


    }

    private void ShootReady()
    {

        if (m_shootingBallIndex < 10)
        {
            if (m_shootingBallIndex < 3)
            {
                m_shootingBalls[m_shootingBallIndex+7].SetActive(false);
                m_shootingBalls[m_shootingBallIndex].SetActive(true);
            }
            else
            {
                m_shootingBalls[m_shootingBallIndex - 3].SetActive(false);
                m_shootingBalls[m_shootingBallIndex].SetActive(true);
            }

        }
        else
        {
            m_shootingBalls[m_shootingBallIndex - 3].SetActive(false);
            m_shootingBallIndex -= 10;
            m_shootingBalls[m_shootingBallIndex].SetActive(true);
        }

        m_shootingBalls[m_shootingBallIndex].transform.position = m_shootingBallPosition;
        m_shootingBallRigidBody = m_shootingBalls[m_shootingBallIndex].GetComponent<Rigidbody>();
        m_shootingBallIndex++;

        EffectSoundManager.Instance.PlayEffect(17);
    }

    private void MakeMonster()
    {
        int randomNumber = Random.Range(0, 3);
        Instantiate(m_Monster, m_MonsterSpawner[randomNumber].transform.position, Quaternion.identity, transform);
    }

    public void DefenseFail()
    {
        if(!m_end)
        {
            m_end = true;
            m_failPrefab.SetActive(true);
            EffectSoundManager.Instance.PlayEffect(22);
            CancelInvoke("MakeMonster");
            Invoke("GameFail", 1);
        }
    }

}
