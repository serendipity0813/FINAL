using UnityEngine;
using UnityEngine.UI;

public class OptionUIController : ButtonHandler
{
    [SerializeField] private GameObject m_BGM;
    [SerializeField] private GameObject m_SFX;
    protected Slider m_bgmSlider;
    protected Slider m_sfxSlider;

    private void Awake()
    {
        m_bgmSlider = m_BGM.GetComponent<Slider>();
        m_sfxSlider = m_SFX.GetComponent<Slider>();
        m_bgmSlider.value = PlayerDataManager.instance.m_playerData.bgmVolume;
        m_sfxSlider.value = PlayerDataManager.instance.m_playerData.sfxVolume;
    }
    private void Start()
    {
        m_bgmSlider.onValueChanged.AddListener(BGMVolumeChanged);
        m_sfxSlider.onValueChanged.AddListener(SFXVolumeChanged);
    }
    private void BGMVolumeChanged(float volume)
    {
        // BGM볼륨 값을 조절합니다.
        BGMSoundManager.Instance.m_AudioSource.volume = volume;
        PlayerDataManager.instance.m_playerData.bgmVolume = volume;
    }
    private void SFXVolumeChanged(float volume)
    {
        // SFX볼륨 값을 조절합니다.
        EffectSoundManager.Instance.m_AudioSource1.volume = volume;
        EffectSoundManager.Instance.m_AudioSource2.volume = volume;
        PlayerDataManager.instance.m_playerData.sfxVolume = volume;
    }
    //설정창 내부 버튼 클릭시 관련 링크로 이동하는 메소드
    public void CustomerSurviceButton()
    {
        Application.OpenURL("https://forms.gle/1b7589nJ6FvL3RFm8");
    }

    public void GameInformationButton()
    {
        Application.OpenURL("https://docs.google.com/spreadsheets/d/1hn1rmcynR9mKSCHu-gE9RO24Xt2r56avrzpsiET8xl4/edit?resourcekey#gid=1058894120");
    }

    public void UpdateHistoryButton()
    {
        Application.OpenURL("https://docs.google.com/spreadsheets/d/1hn1rmcynR9mKSCHu-gE9RO24Xt2r56avrzpsiET8xl4/edit?resourcekey#gid=0");
    }

    public void QuitBtn()
    {
        PlayerDataManager.instance.SaveJson();
        Application.Quit();
    }
}
