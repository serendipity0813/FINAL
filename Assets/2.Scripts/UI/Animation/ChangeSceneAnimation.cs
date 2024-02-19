using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneAnimation : MonoBehaviour
{
    //스테이지 클리어, 실패, 첫 시작 등의 애니메이션을 위한 이펙트 배열
    [SerializeField] private GameObject[] m_clearEffect;  
    [SerializeField] private GameObject[] m_failEffect;
    [SerializeField] private GameObject[] m_normalEffect;

    public void ClearAnimation()
    {
        //게임 클리어 : 1, 패배 : -1, 처음시작 : 0 으로 변수 되도록 설정하여 나와야 하는 애니메이션 구분
        if (MiniGameManager.Instance.m_clearCheck == 1)
        {
            m_clearEffect[0].SetActive(true);
            EffectSoundManager.Instance.PlayEffect(33);
        }
        else if (MiniGameManager.Instance.m_clearCheck == -1)
        {
            m_failEffect[0].SetActive(true);

            EffectSoundManager.Instance.PlayEffect(34);
        }
        else
        {

            EffectSoundManager.Instance.PlayEffect(33);
            m_normalEffect[0].SetActive(true);
            m_normalEffect[1].SetActive(true);
        }



    }

}
