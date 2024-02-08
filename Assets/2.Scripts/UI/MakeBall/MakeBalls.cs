using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBalls : MonoBehaviour
{
    public GameObject gameBall;
    void Start()
    {
        StartCoroutine(MakeBall());
    }

    IEnumerator MakeBall()
    {
        for (int i = 1; i < MiniGameManager.Instance.MiniGames.games.Count; i++)
        {
            if (PlayerDataManager.instance.m_playerData.haveGames[i])
            {
                yield return new WaitForSeconds(0.1f);
                float rndX = UnityEngine.Random.Range(-2f, 2);
                float rndZ = UnityEngine.Random.Range(-2f, 2);
                Vector3 position = new Vector3(rndX, transform.position.y, rndZ);
                if (MiniGameManager.Instance.MiniGames.games[i].gameIcon != null)
                {
                    GameObject newBall = Instantiate(MiniGameManager.Instance.MiniGames.games[i].gameIcon, position, Quaternion.identity, transform);
                    newBall.name = i.ToString();
                }
                else
                {
                    GameObject newBall = Instantiate(gameBall, position, Quaternion.identity, transform);
                    newBall.name = i.ToString();
                }
            }
        }
    }
}
