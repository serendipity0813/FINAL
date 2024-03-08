using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundManager : MonoBehaviour
{
    public static EffectSoundManager Instance;
    public AudioSource m_AudioSource1;
    public AudioSource m_AudioSource2;
    public AudioSource m_loopAudio;
    [SerializeField] private List<SoundEffects> soundEffects = new List<SoundEffects>();

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
        m_AudioSource1 = GetComponent<AudioSource>();
        m_AudioSource1.volume = PlayerDataManager.instance.m_playerData.sfxVolume;
        m_AudioSource2 = gameObject.AddComponent<AudioSource>();
        m_AudioSource2.volume = PlayerDataManager.instance.m_playerData.sfxVolume;
        m_loopAudio = gameObject.AddComponent<AudioSource>();
        m_loopAudio.volume = PlayerDataManager.instance.m_playerData.sfxVolume;
        m_loopAudio.loop = true;
    }

    // 특정 효과음 재생
    // EffectSoundManager.Instance.PlayEffect();
    // ()안에 int 값 사운드 인덱스 기입
    public void PlayEffect(int index)
    {
        m_AudioSource1.PlayOneShot(soundEffects[index].SoundEffect);
    }

    // 이펙트 피치 조절 기능 추가
    public void PlayEffect(int index, float pitch)
    {
        m_AudioSource2.pitch = pitch;
        m_AudioSource2.PlayOneShot(soundEffects[index].SoundEffect);
    }

    // 이펙트 루프 클립 등록
    public void PlayAudioLoop(int index)
    {
        m_loopAudio.clip = soundEffects[index].SoundEffect;
    }

    // 이펙트 루프 클립 실행
    public void PlayLoop()
    {
        if (m_loopAudio != null)
        {
            m_loopAudio.Play();
        }
    }

    // 이펙트 루프 클립 종료
    public void StopLoop()
    {
        if (m_loopAudio != null)
        {
            m_loopAudio.Stop();
        }
    }

    // 재생중인 이펙트 모두 정지
    public void StopEffect()
    {
        m_AudioSource1.Stop();
        m_AudioSource2.Stop();
        m_loopAudio.Stop();
    }

    // 사운드 클립 저장
    [Serializable]
    protected struct SoundEffects
    {
        [Header("SoundEffects")]
        public string name; // 사운드 이름 및 설명
        public AudioClip SoundEffect; // 사운드 클립
    }
}
