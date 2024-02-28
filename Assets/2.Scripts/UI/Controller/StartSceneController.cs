using UnityEngine;

public class StartSceneController : MonoBehaviour
{
    [Header("Sun")]
    private Light m_sun;
    [SerializeField] private Gradient sunColor;
    [SerializeField] private AnimationCurve sunIntensity;

    [Header("Moon")]
    private Light m_moon;
    [SerializeField] private Gradient moonColor;
    [SerializeField] private AnimationCurve moonIntensity;

    [Header("Other Lighting")]
    [SerializeField] private AnimationCurve m_ambientIntensity;
    [SerializeField] private AnimationCurve m_reflectionIntensity;

    private GameObject m_light;//Direction Light Sun, Moon이 담겨져 있는 객체

    [Header("Character")]
    [SerializeField] private GameObject m_character;//오프닝 화면의 캐릭터
    private AnimatorUpdater m_aniUpdater;//오프닝 캐릭터의 애니메이션 관리자

    [Header("StartScene Ui")]
    [SerializeField] private CanvasGroup m_canvas;//시작하면 Ui의 전체 알파값을 줄이기 위함

    [SerializeField] TutorialUIController tutorialUIController;// 튜토리얼 관련
    private float m_timeRate = 0.0f;//총 진행 시간 %퍼센트로  
    private float m_sunsetTime = 3.0f;//밤에서 낮으로 바뀌는데 걸리는 시간
    private float m_alphaTime = 4.0f;//UI 투명화까지 걸리는 시간 (곱 연산)
    private float m_maxTimer = 10.0f;//초기 타이머
    private float m_timer;//전체 타이머
    private bool m_nextDay = true;//화면 터치하여 낮으로 변경 false로 변경 & 중복 호출 방지
    private bool m_wake = true;//캐릭터 애니메이션 실행 false로 변경 & 중복 호출 방지

    // Start is called before the first frame update
    private void Start()
    {
        m_timer = m_maxTimer;//타이머 세팅


        if (m_character != null)
        {
            PlayerCharacterController pcc = m_character.GetComponent<PlayerCharacterController>();
            m_aniUpdater = pcc.GetPlayerAnimator();
        }

        m_aniUpdater.SleepCharacter();//캐릭터의 처음 등장 모션을 잠자기로 변경

        m_light = GameSceneManager.Instance.GetLight();//현재 씬의 조명 오브젝트를 받아옴
        m_sun = m_light.transform.GetChild(0).GetComponent<Light>();//자식 오브젝트인 태양을 받아옴
        m_moon = m_light.transform.GetChild(1).GetComponent<Light>();//자식 오브젝트인 달을 받아옴
        m_canvas.alpha = 1.0f;//캔버스의 알파값 초기화

        Quaternion rotation = Quaternion.Euler(0.0f, 50.0f, 0.0f);//광원 회전 X를 0으로 초기화
        m_light.transform.rotation = rotation;
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_nextDay)
        {
            bool result = TouchManager.instance.IsBegan();//화면 터치 했을 경우

            if (result && m_nextDay)
            {
                m_nextDay = false;//중복 클릭 방지 - Invoke를 여러번 할 수 있기 때문
            }
        }else
        {
            bool result = TouchManager.instance.IsBegan();//화면 터치 했을 경우
            m_timer = m_timer < 0.0f ? 0.0f : m_timer - Time.deltaTime;

            float add = Time.deltaTime / m_sunsetTime;//진행 시간 퍼센트 계산
            m_timeRate = m_timeRate >= 1.0f ? 1.0f : m_timeRate + add;//시간 비율이 1.0f을 넘으면 1.0f으로 고정
            Quaternion rotation = Quaternion.Euler(m_timeRate * 120.0f, 50.0f, 0.0f);//광원을 x축 120도 방향으로 돌리기
            m_light.transform.rotation = rotation;

            if (m_timer <= m_maxTimer - m_sunsetTime && m_wake)
            {
                m_aniUpdater.WakeCharacter();//캐릭터 애니메이션 Trigger 발동 (한번만)
                m_wake = false;//트리거 발동 후 false로
            }

            if (m_canvas != null) //StartScene 프리펩에 Inspecter창에서 연결되어 있을 때
            {
                m_canvas.alpha = 1.0f - m_timeRate * m_alphaTime;//1.0f 에서 시간에 따라 점점 줄어들게
            }

            //클릭을 두번 했을 경우 바로 로비화면으로
            if (result && PlayerDataManager.instance.m_playerData.tutorial)
            {
                SkipAll();
            }
        }

        if (m_timer <= 0.0f)
        {
            LobbyClick();
        }

        UpdateLighting(m_sun, sunColor, sunIntensity);
        UpdateLighting(m_moon, moonColor, moonIntensity);

        RenderSettings.ambientIntensity = m_ambientIntensity.Evaluate(m_timeRate);//랜더 세팅에서 ambient를 Inspect창의 곡선 그래프대로 변경
        RenderSettings.reflectionIntensity = m_reflectionIntensity.Evaluate(m_timeRate);//랜더 세팅에서 reflection을 Inspect창의 곡선 그래프대로 변경
        RenderSettings.skybox.SetFloat("_Exposure", (m_timeRate * 2.0f + 1.0f) / 3.0f);//Skybox의 명암을 33퍼로 낮춰놨다가 아침이 되면서 100퍼로 바꾸게
    }

    //한번 더 클릭하면 바로 로비화면으로 넘어가지게
    private void SkipAll()
    {
        m_timeRate = 1.0f;

        m_canvas.alpha = 0.0f;//캔버스 투명도 바로 0으로

        {//광원 바로 낮으로 변경
            Quaternion rotation = Quaternion.Euler(120.0f, 50.0f, 0.0f);//광원을 x축 120도 방향으로 돌리기
            m_light.transform.rotation = rotation;

            UpdateLighting(m_sun, sunColor, sunIntensity);
            UpdateLighting(m_moon, moonColor, moonIntensity);

            RenderSettings.ambientIntensity = m_ambientIntensity.Evaluate(1.0f);//랜더 세팅에서 ambient를 Inspect창의 곡선 그래프대로 변경
            RenderSettings.reflectionIntensity = m_reflectionIntensity.Evaluate(1.0f);//랜더 세팅에서 reflection을 Inspect창의 곡선 그래프대로 변경
            RenderSettings.skybox.SetFloat("_Exposure", 1.0f);//Skybox의 명암을 33퍼로 낮춰놨다가 아침이 되면서 100퍼로 바꾸게
        }


        LobbyClick();
    }

    private void UpdateLighting(Light lightSource, Gradient colorGradiant, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(m_timeRate);

        lightSource.color = colorGradiant.Evaluate(m_timeRate);
        lightSource.intensity = intensity;

        GameObject go = lightSource.gameObject;
        if (lightSource.intensity == 0 && go.activeInHierarchy)
            go.SetActive(false);
        else if (lightSource.intensity > 0 && !go.activeInHierarchy)
            go.SetActive(true);
    }

    private void LobbyClick()
    {
        if (PlayerDataManager.instance.m_playerData.tutorial)
        {
            CameraManager.Instance.m_followEnabled = false;
            GameSceneManager.Instance.PopupClear();
            MiniGameManager.Instance.GameReset();
            Time.timeScale = 1.0f;
            GameSceneManager.Instance.SceneSelect(SCENES.LobbyScene);
            CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
        }
        else
        {
            Instantiate(tutorialUIController);
            CameraManager.Instance.m_followEnabled = false;
            GameSceneManager.Instance.PopupClear();
            MiniGameManager.Instance.GameReset();
            Time.timeScale = 1.0f;
            GameSceneManager.Instance.SceneSelect(SCENES.LobbyScene);
            CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
        }
    }
}
