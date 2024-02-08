using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSceneAnimation : MonoBehaviour
{
    [SerializeField] GameObject[] Stars;
    [SerializeField] GameObject[] StarsEffects;
    [SerializeField] GameObject[] StarsBGEffects;
    private int m_startNumber = 0;
    private int m_startEffectNumer = 0;

    public void StarEffect()
    {
        InvokeRepeating("EffectOn" , 0.1f, 0.1f);
        InvokeRepeating("StarOn", 0.3f, 0.1f);
    }

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
        if (m_startNumber == 5)
            CancelInvoke("StarOn");
    }

}
