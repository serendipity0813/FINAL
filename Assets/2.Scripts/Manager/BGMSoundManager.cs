using System;
using System.Collections.Generic;
using UnityEngine;

public class BGMSoundManager : MonoBehaviour
{
    public static BGMSoundManager Instance;
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
        m_AudioSource.Play(); // 임시 BGM 실행
    }

    [Serializable]
    public struct BGMs
    {
        [Header("BGM")]
        public string name; // 사운드 이름 및 설명
        public AudioClip BGM; // 사운드 클립
    }
    public List<BGMs> bgm = new List<BGMs>();

    // 배경음 재생
    public AudioClip PlayBGM(int index)
    {
        return bgm[index].BGM;
    }

    // 필요에 따라 다양한 사운드 관련 메서드 추가 가능
    // 필요한 메서드 = 볼륨 조절 관련 + 인스턴스로 불러올때 자동으로 매니저의 볼륨 값을 설정할지

    /* 
     * 다른 스크립트 내에서 사운드 활용 방법
     * 원하는 오브젝트에 Audio Source 컴포넌트 추가
     * 
     * private BGMSoundManager soundManager;
     * private AudioSource audioSource;
     * private void Start()
       {
           soundManager = BGMSoundManager.Instance;
           audioSource = GetComponent<AudioSource>();
       }
        audioSource.clip = soundManager.PlayEffect(n) // 클립 설정
        audioSource.loop = false; or true; // 사운드 1번 재생, 무한 재생 설정
        audioSource.Play(); // 사운드 실행
        audioSource.Stop(); // 사운드 중지
        audioSource.Pause(); // 사운드 일시 중지
        audioSource.UnPause(); // 사운드 일시 중지 되어있는것을 다시 재생
     */
}
