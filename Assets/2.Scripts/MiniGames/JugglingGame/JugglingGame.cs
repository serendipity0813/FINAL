using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugglingGame : MiniGameSetting
{
    [SerializeField] private GameObject m_jugglingBall;
    [SerializeField] private GameObject m_BallSpawner;
    private GameObject m_jugglingBallPrefab;
    private Rigidbody m_jugglingBallRigidBody;
    private Camera m_camera;
    private Vector3 m_jugglingBallSpawnPosition;

    private float m_timer;
    private bool m_end = false;
    private float m_Difficultyforce;

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
        CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
        m_camera = CameraManager.Instance.GetCamera();

        //인게임 text내용 설정 + 게임 승리조건
        m_missionText.text = "공을 떨어트리지 않고 버텨라!";
        m_jugglingBallSpawnPosition = m_BallSpawner.transform.position;
        m_Difficultyforce = m_difficulty2;
        Invoke("MakeJugglingBall", 2);

    }

    private void Update()
    {
        #region   //게임 시간별 로직 + 성공실패 관리
        //시간과 카운트 반영되는 코드
        m_timeText.text = (12 - m_timer).ToString("0.00");

        //게임 시작 후 미션을 보여주고 나서 1초 후 지움
        if (!m_end)
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

        if (!m_end)
        {
            //게임 승리조건
            if (m_timer >= 12)
            {
                m_end = true;
                m_timer = 12;
                m_clearPrefab.SetActive(true);
                EffectSoundManager.Instance.PlayEffect(21);
                Invoke("GameClear", 1);

            }

        }

        #endregion

        if (Input.GetMouseButtonDown(0) && m_timer > 2 && m_end == false)
        {
            Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Target")
                {

                    EffectSoundManager.Instance.PlayEffect(27);
                    m_jugglingBallPrefab = hit.collider.gameObject;
                    m_jugglingBallRigidBody = m_jugglingBallPrefab.GetComponent<Rigidbody>();

                    float xPush = 0;
                    if (m_jugglingBallPrefab.transform.position.x > 0)
                        xPush = -0.1f * m_Difficultyforce;
                    else if (m_jugglingBallPrefab.transform.position.x < 0)
                        xPush = 0.1f * m_Difficultyforce;
                    else
                        xPush = Random.Range(-0.1f, 0.1f);

                    m_jugglingBallRigidBody.AddForce(new Vector3(xPush, 1, 0) * (2.5f + 0.5f* m_Difficultyforce), ForceMode.Impulse);
                }
            }

        }


    }

    private void MakeJugglingBall()
    {
        for (int i = 0; i < m_difficulty1; i++)
        {
            int randomx = Random.Range(0, 3);
            m_jugglingBallSpawnPosition.x = -4 + i*3 + randomx;
            m_jugglingBallSpawnPosition.y = Random.Range(9f, 11f);
            Instantiate(m_jugglingBall, m_jugglingBallSpawnPosition, Quaternion.identity, transform);
        }
    }

    public void JugglingFail()
    {
        if (!m_end)
        {
            m_end = true;
            m_timer = 12;
            m_failPrefab.SetActive(true);
            EffectSoundManager.Instance.PlayEffect(22);
            Invoke("GameFail", 1);
        }
    }


}
