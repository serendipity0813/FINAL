using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIController : MonoBehaviour
{
    [SerializeField] private GameObject m_inGameOption;

    public void InGameOptionToggle()
    {
        //인게임 옵션버튼 토글화
        if(m_inGameOption.activeSelf == true)
            m_inGameOption.SetActive(false);
        else
            m_inGameOption.SetActive(true);
    }

}
