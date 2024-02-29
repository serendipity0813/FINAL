using System.Collections;
using UnityEngine;

public class UpDownSlimeController : MonoBehaviour
{
    private Vector3 m_startPosition;
    private Vector3 m_finishPosition;
    private float m_upSpeed;
    private float m_downSpeed;
    private float m_waittingTime;
    private bool m_downFlag = false;
    private bool m_coroutinFlag = false;

    private void Start()
    {
        m_startPosition = transform.position;
        m_startPosition.y += 1;
        m_startPosition.z += 1;
        m_finishPosition = transform.position;
        m_finishPosition.y -= 1;
        m_finishPosition.z -= 1;
        m_waittingTime = Random.Range(0.3f, 0.8f);
        float upRandom = Random.Range(0.1f, 0.4f);
        float downRandom = Random.Range(0.1f, 0.4f);



        m_upSpeed = (PlayerDataManager.instance.m_playerData.stage % 3 + 1) * 0.01f + upRandom;
        m_downSpeed = (PlayerDataManager.instance.m_playerData.stage % 3 + 1) * 0.01f + downRandom;


        StartCoroutine(SlimeUpDownCoroutine());
    }

    private void FixedUpdate()
    {
        if(m_downFlag)
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_finishPosition, m_downSpeed);
        else
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_startPosition, m_upSpeed);

        if (gameObject.transform.position == m_startPosition && m_coroutinFlag == false)
        {
            StartCoroutine(SlimeUpDownCoroutine());
        }

        if (gameObject.transform.position == m_finishPosition)
            gameObject.SetActive(false);
    }

    IEnumerator SlimeUpDownCoroutine()
    {
        m_coroutinFlag = true;
        EffectSoundManager.Instance.PlayEffect(19);
        yield return new WaitForSeconds(m_waittingTime);
        m_downFlag = true;
    }
}
