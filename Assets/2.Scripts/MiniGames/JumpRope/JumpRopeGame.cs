
using UnityEngine;

public class JumpRopeGame : MiniGameSetting
{
    [SerializeField]
    private GameObject m_player;
    private DragToMoveController m_dragToMoveController;
    private int m_difficuty;
    private int m_count = 0;
    private bool m_collision = false;//플레이어가 밧줄과 닿았는지 체크하는 함수

    private float m_timer;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        CameraManager.Instance.ChangeCamera(CameraView.Angle60View);//90도 각도로 내려다 보는 카메라로 변경
        CameraManager.Instance.SetFollowTarget(m_player);
        CameraManager.Instance.m_followEnabled = true;

        m_dragToMoveController = m_player.GetComponent<DragToMoveController>();
        m_dragToMoveController.SetJumpPower(500.0f);
        m_difficuty = m_difficulty1 * 3 + m_difficulty2 - 3;
        //인게임 text내용 설정 + 게임 승리조건
        m_missionText.text = m_difficuty.ToString() + "회 줄넘기!";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_dragToMoveController.UpdateMoveWithJump();
    }


    private void Update()
    {
        #region   //게임 시간별 로직 + 성공실패 관리
        //시간과 카운트 반영되는 코드
        m_timeText.text = (12-m_timer).ToString("0.00");
        m_countText.text = m_count.ToString();

        //게임 시작 후 미션을 보여주고 나서 1초 후 지움
        m_timer = m_timer <= 0 ? 0 : m_timer - Time.deltaTime;
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

        //게임 패배조건 - 시간초과
        if (m_timer > 12)
        {
            m_failPrefab.SetActive(true);
            Invoke("GameFail", 1);
        }
        #endregion
    }

    public void CheckWin()
    {
        bool result;       
        switch (m_difficuty)
        {
            case 1:
                result = m_count > 5 ? true : false;
                break;
            case 2:
                result = m_count > 10 ? true : false;
                break;
            case 3:
                result = m_count > 15 ? true : false;
                break;
            default:
                //난이도가 필요없는 경우 무조건 지는 판정으로 
                result = false;
                break;
        }

        if (result)//개수 제한을 넘겼을 때 승리
        {
            m_clearPrefab.SetActive(true);
            Invoke("GameClear", 1);
            CameraManager.Instance.m_followEnabled = false;
        }
        else
        {
            if (m_collision)//개수 제한을 못넘기고 줄에 걸렸을 때 패배
            {
                m_failPrefab.SetActive(true);
                Invoke("GameFail", 1);
                CameraManager.Instance.m_followEnabled = false;
            }
        }
    }

    public void SetCollision()
    {
        m_collision = true;
    }

    public void AddCount()
    {
        m_count++;
    }




}
