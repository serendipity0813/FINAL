using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSceneAnimation : MonoBehaviour
{
    [SerializeField] private GameObject m_clearEffect;


    public void ClearAnimation()
    {
        m_clearEffect.SetActive(true);
    }

}
