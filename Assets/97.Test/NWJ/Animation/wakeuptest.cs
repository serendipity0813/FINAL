using UnityEngine;

public class wakeuptest : MonoBehaviour
{
    [Header("Time")]
    private float m_timeRate = 0.0f;//총 진행 시간 %퍼센트로  
    private float m_timeTakes = 5.0f;//밤에서 낮으로 바뀌는데 걸리는 시간

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionIntensityMultiplier;

    [SerializeField] private GameObject m_light;//Direction Light Sun, Moon이 담겨져 있는 객체

    [Header("AnimatorUpdater")]
    [SerializeField] private AnimatorUpdater m_aniUpdater;
    private bool m_once = true;

    // Start is called before the first frame update
    void Start()
    {
        m_aniUpdater.SleepCharacter();//캐릭터의 처음 등장 모션을 잠자기으로 변경

        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(m_timeRate);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(m_timeRate);
    }

    // Update is called once per frame
    void Update()
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

        UpdateLighting(sun, sunColor, sunIntensity);
        UpdateLighting(moon, moonColor, moonIntensity);

        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(m_timeRate);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(m_timeRate);
    }

    private void NextDay()
    {
        m_aniUpdater.WakeCharacter();
    }

    void UpdateLighting(Light lightSource, Gradient colorGradiant, AnimationCurve intensityCurve)
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
