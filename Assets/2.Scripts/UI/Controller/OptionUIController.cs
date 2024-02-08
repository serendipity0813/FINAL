using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    }
    private void Start()
    {
        m_bgmSlider.value = 50;
        m_sfxSlider.value = 50;
    }

    public void CustomerSurviceButton()
    {
        Application.OpenURL("https://forms.gle/1b7589nJ6FvL3RFm8");
    }

    public void UpdateHistoryButton()
    {
        Application.OpenURL("https://www.notion.so/b69e76da62334ce1af9470b6dad29cf2");
    }

}
