using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundManager : MonoBehaviour
{
    private static EffectSoundManager Instance;
    private AudioSource m_AudioSource;
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

    [Serializable]
    public struct SoundEffects
    {
        [Header("SoundEffects")]
        public string name; // 사운드 이름 및 설명
        public AudioClip SoundEffect; // 사운드 클립
    }
    public List<SoundEffects> soundEffects = new List<SoundEffects>();

    // 특정 효과음 재생
    public AudioClip PlayEffect(int index)
    {
        return soundEffects[index].SoundEffect;
    }
}
