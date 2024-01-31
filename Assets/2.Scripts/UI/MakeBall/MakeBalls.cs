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
        for (int i = 0; i < MiniGameManager.Instance.MiniGames.games.Count; i++)
        {
            yield return new WaitForSeconds(0.1f);
            float rndX = UnityEngine.Random.Range(-2f, 2);
            float rndZ = UnityEngine.Random.Range(-2f, 2);
            Vector3 position = new Vector3(rndX, transform.position.y, rndZ);
            GameObject newBall = Instantiate(gameBall, position, Quaternion.identity, transform);
            //newBall.name = MiniGameManager.Instance.MiniGames.games[i].gameName;
            newBall.name = i.ToString();
        }
    }
}
