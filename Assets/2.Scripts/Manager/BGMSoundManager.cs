using System;
using System.Collections.Generic;
using UnityEngine;

public class BGMSoundManager : MonoBehaviour
{
    public static BGMSoundManager Instance;
    public AudioSource m_AudioSource;
    [SerializeField] private List<BGMs> bgm = new List<BGMs>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Init();
    }

    private void Init() // 초기화
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.volume = PlayerDataManager.instance.m_playerData.bgmVolume;
        PlayBGM(0); // 시작시 BGM 실행
    }

    // BGM 재생 메서드
    public void PlayBGM(int index)
    {
        m_AudioSource.Stop();
        m_AudioSource.clip = bgm[index].BGM;
        if (!m_AudioSource.loop)
            m_AudioSource.loop = true;
        m_AudioSource.Play();
    }

    // BGM 일시 정지
    public void PauseBGM()
    {
        m_AudioSource.Pause();
    }

    // BGM 일시 정지 해제
    public void UnPauseBGM()
    {
        m_AudioSource.UnPause();
    }

    // 사운드 클립 저장
    [Serializable]
    public struct BGMs
    {
        [Header("BGM")]
        public string name; // 사운드 이름 및 설명
        public AudioClip BGM; // 사운드 클립
    }
}
