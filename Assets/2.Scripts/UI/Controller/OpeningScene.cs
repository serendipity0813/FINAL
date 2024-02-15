using UnityEngine;

public class StartSceneController : ButtonHandler
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
    
    private float m_timeRate = 0.0f;//총 진행 시간 %퍼센트로  
    private float m_timeTakes = 5.0f;//밤에서 낮으로 바뀌는데 걸리는 시간
    private bool m_once = true;

    // Start is called before the first frame update
    private void Start()
    {
        //Instantiate(Character, m_character.transform);  //캐릭터를 불러오는 부분
        m_aniUpdater = m_character.transform.GetChild(0).GetComponent<AnimatorUpdater>();
        m_aniUpdater.SleepCharacter();//캐릭터의 처음 등장 모션을 잠자기으로 변경

        m_light = GameSceneManager.Instance.GetLight();
        m_sun = m_light.transform.GetChild(0).GetComponent<Light>();
        m_moon = m_light.transform.GetChild(1).GetComponent<Light>();

        RenderSettings.ambientIntensity = m_ambientIntensity.Evaluate(m_timeRate);
        RenderSettings.reflectionIntensity = m_reflectionIntensity.Evaluate(m_timeRate);
    }

    // Update is called once per frame
    private void Update()
    {
        bool result = TouchManager.instance.IsBegan();

        if (result && m_once)
        {
            m_once = false;//중복 클릭 방지 - Invoke를 여러번 할 수 있기 때문

            Invoke("NextDay", m_timeTakes);//클릭하면 밤/낮 전환 이후 캐릭터 깨우기
        }

        if (!m_once)
        {
            float add = Time.deltaTime / m_timeTakes;
            m_timeRate = m_timeRate >= 1.0f ? 1.0f : m_timeRate + add;
            Quaternion rotation = Quaternion.Euler(m_timeRate * 120.0f, 50.0f, 0.0f);
            m_light.transform.rotation = rotation;
        }

        UpdateLighting(m_sun, sunColor, sunIntensity);
        UpdateLighting(m_moon, moonColor, moonIntensity);

        RenderSettings.ambientIntensity = m_ambientIntensity.Evaluate(m_timeRate);
        RenderSettings.reflectionIntensity = m_reflectionIntensity.Evaluate(m_timeRate);
    }

    private void NextDay()
    {
        m_aniUpdater.WakeCharacter();
        Invoke("LobbyClick", 8.0f);//캐릭터가 일어나는데 걸리는 시간을 기다리고 씬 변경
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

}
