using UnityEngine;

public class MiniGameBaseCode : MiniGameSetting
{
    private Camera m_camera;
    private int m_clearCount;
    private float m_timer;
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
        CameraManager.Instance.ChangeCamera(CameraView.Angle90View);
        m_camera = CameraManager.Instance.GetCamera();

        //인게임 text내용 설정 + 게임 승리조건
        m_clearCount = 10;
        m_missionText.text = "mission text";
    }

    private void Update()
    {
        #region   //게임 시간별 로직 + 성공실패 관리
        //시간과 카운트 반영되는 코드
        m_timeText.text = (12-m_timer).ToString("0.00");
        m_countText.text = m_clearCount.ToString();

        //게임 시작 후 미션을 보여주고 나서 1초 후 지움
        if (!m_end)
            m_timer += Time.deltaTime;

        if (m_timer > 0.5 && m_missionPrefab.activeSelf == false)
            m_missionPrefab.SetActive(true);
        if (m_timer > 1.5 && m_missionPrefab.activeSelf == true )
            m_missionPrefab.SetActive(false);

        //2초 후 부터 실제 게임시작 - 시간제한과 클리어를 위한 카운트 ui를 출력
        if(m_timer > 2)
        {
            m_timePrefab.SetActive(true);
            m_countPrefab.SetActive(true);
        }

        if (!m_end)
        {
            //게임 승리조건
            if (m_clearCount == 0)
            {
                m_end = true;
                m_timer = 12;
                m_clearPrefab.SetActive(true);
                EffectSoundManager.Instance.PlayEffect(21);
                Invoke("GameClear", 1);

            }

            //게임 패배조건
            if (m_timer > 12 && m_clearCount > 0)
            {
                m_end = true;
                m_timer = 12;
                m_failPrefab.SetActive(true);
                EffectSoundManager.Instance.PlayEffect(22);
                Invoke("GameFail", 1);
            }
        }
        #endregion


    }

}
