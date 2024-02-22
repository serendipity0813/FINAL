using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSceneAnimation : MonoBehaviour
{
    //게임오버 씬 애니메이션 이펙트 배열
    [SerializeField] GameObject[] Stars;
    [SerializeField] GameObject[] StarsEffects;
    [SerializeField] GameObject[] StarsBGEffects;
    private int m_startNumber = 0;
    private int m_startEffectNumer = 0;

    //유니티 애니메이션 내부 이벤트 시스템과 연결
    public void StarEffect()
    {
        //시간 간격을 두고 별과 효과가 순차적으로 나오도록 하기 위함
        InvokeRepeating("EffectOn" , 0.1f, 0.1f);
        InvokeRepeating("StarOn", 0.3f, 0.1f);
    }

    //배열의 0번 ~ 4번 까지 0.1초 간격으로 생성 + 5번 나오고 나서 Invoke 취소
    private void EffectOn()
    {
        StarsEffects[m_startEffectNumer].SetActive(true);
        m_startEffectNumer++;
        if (m_startEffectNumer == 5)
            CancelInvoke("EffectOn");
    }

    private void StarOn()
    {
        Stars[m_startNumber].SetActive(true);
        StarsBGEffects[m_startNumber].SetActive(true);
        m_startNumber++;
        EffectSoundManager.Instance.PlayEffect(33);
        if (m_startNumber == 5)
            CancelInvoke("StarOn");
    }

}
