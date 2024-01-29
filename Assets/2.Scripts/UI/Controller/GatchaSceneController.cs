using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GatchaSceneController : ButtonHandler
{
    [SerializeField] private TextMeshProUGUI m_playerTicketText;

    private void Start()
    {
        m_playerTicketText.text = PlayerDataManager.instance.m_playerData.ticket.ToString();
    }

    public void AdClick()
    {
        Debug.Log("광고보기");
    }


}
