using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundManager : MonoBehaviour
{
    [Serializable]
    protected struct SoundEffects
    {
        [Header("SoundEffects")]
        public string name; // 사운드 이름 및 설명
        public AudioClip SoundEffect; // 사운드 클립
    }

    [SerializeField] private List<SoundEffects> soundEffects = new List<SoundEffects>();

    public static EffectSoundManager Instance;
    public AudioSource m_AudioSource1;
    public AudioSource m_AudioSource2;
    public AudioSource m_loopAudio;

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
        m_AudioSource1 = GetComponent<AudioSource>(); // 사운드 조절 관련 전체 설정을 위해
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

    public void PlayEffectPitch(int index, float pitch)
    {
        m_AudioSource2.pitch = pitch;
        m_AudioSource2.PlayOneShot(soundEffects[index].SoundEffect);
    }

    public void PlayAudioLoop(int index)
    {
        m_loopAudio.clip = soundEffects[index].SoundEffect;
    }

    public void StopEffect()
    {
        m_AudioSource1.Stop();
        m_AudioSource2.Stop();
        m_loopAudio.Stop();
    }

    //public void ToggleLoop()
    //{
    //    if (m_loopAudio.isPlaying)
    //    {
    //        m_loopAudio.Pause();
    //    }
    //    else
    //    {
    //        m_loopAudio.UnPause();
    //    }
    //}

    public void StopLoop()
    {
        if (m_loopAudio != null)
        {
            m_loopAudio.Stop();
        }
    }

    public void PlayLoop()
    {
        if (m_loopAudio != null)
        {
            m_loopAudio.Play();
        }
    }

}
