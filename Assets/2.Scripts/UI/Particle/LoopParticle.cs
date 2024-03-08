using UnityEngine;

public class LoopParticle : MonoBehaviour
{
    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        StartParticle();
    }

    private void StartParticle() // 파티클 시작
    {
        if (particle == null)
        {
            return;
        }

        // 루프가 아니라면 파티클 시스템의 재생이 끝날 때마다
        var mainModule = particle.main;
        if (!mainModule.loop)
        {
            mainModule.stopAction = ParticleSystemStopAction.Callback;
            particle.Stop(); // (시작 시 한 번 실행됨)
            particle.Play();
        }
        // 루프라면 그냥 플레이
        else
        {
            particle.Play();
        }
    }

    // 파티클 재생이 끝나면 다시 시작
    private void OnParticleSystemStopped()
    {
        particle.Play();
    }
}