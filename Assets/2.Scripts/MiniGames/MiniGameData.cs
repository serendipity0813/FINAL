using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameData : MonoBehaviour
{
    public Dictionary<string, string> m_gameDiscription = new Dictionary<string, string>();
    private int m_gameLength;
    private string m_gameName;

    private void Awake()
    {
        m_gameLength = MiniGameManager.Instance.MiniGames.Length;
    }

    private void Start()
    {
        for (int i = 0; i < m_gameLength; i++)
        {
            m_gameName = MiniGameManager.Instance.MiniGames[i].name;
            m_gameDiscription.Add(m_gameName, m_gameName);
        }
    }

 
}
