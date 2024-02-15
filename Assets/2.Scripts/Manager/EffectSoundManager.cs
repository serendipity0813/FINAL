using System;
using System.Collections;
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
    public AudioSource m_AudioSource;

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
        m_AudioSource = GetComponent<AudioSource>(); // 사운드 조절 관련 전체 설정을 위해
    }

    // 특정 효과음 재생
    // EffectSoundManager.Instance.PlayEffect();
    // ()안에 int 값 사운드 인덱스 기입
    public void PlayEffect(int index)
    {
        m_AudioSource.PlayOneShot(soundEffects[index].SoundEffect);
    }
}
