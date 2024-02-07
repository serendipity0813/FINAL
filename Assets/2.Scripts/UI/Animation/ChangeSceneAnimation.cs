using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneAnimation : MonoBehaviour
{
    [SerializeField] private GameObject[] m_clearEffect;
    [SerializeField] private GameObject[] m_failEffect;
    [SerializeField] private GameObject[] m_normalEffect;

    public void ClearAnimation()
    {
        if(MiniGameManager.Instance.m_clearCheck == 1)
        {
            m_clearEffect[0].SetActive(true);
        }
        else if(MiniGameManager.Instance.m_clearCheck == -1)
        {
            m_failEffect[0].SetActive(true);
        }
        else
        {
            m_normalEffect[0].SetActive(true);
            m_normalEffect[1].SetActive(true);
        }
            
    }

}
