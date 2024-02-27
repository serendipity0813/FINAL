using System;
using UnityEngine;

public class LoopParticle : MonoBehaviour
{
    private event Action Event;
    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }
    void Start()
    {
        StartParticle();
    }

    void StartParticle()
    {
        if (particle == null)
        {
            return;
        }
        // 루프가 아니라면 파티클 시스템의 재생이 끝날 때마다
        // OnParticleStop 메서드 호출 등록
        var mainModule = particle.main;
        if (!mainModule.loop)
        {
            mainModule.stopAction = ParticleSystemStopAction.Callback;
            particle.Stop(); // (시작 시 한 번 실행됨)
            particle.Play();

            // 파티클 시스템의 재생이 끝날 때마다 OnParticleStop 메서드 호출
            Event += OnParticleSystemStopped;
        }
        // 루프라면 그냥 플레이
        else
        {
            particle.Play();
        }
    }

    // 파티클 시스템 재생이 끝났을 때 호출되는 메서드
    private void OnParticleSystemStopped()
    {
        // 파티클 시스템 재생이 끝나면 다시 시작
        particle.Play();
    }

    // 활성화시 이벤트 등록
    private void OnEnable()
    {
        Event += OnParticleSystemStopped;
    }
    // 비활성화시 이벤트 해제
    private void OnDisable()
    {
        Event -= OnParticleSystemStopped;
    }
}
