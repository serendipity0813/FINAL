using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniDefenceMonsterController : MonoBehaviour
{
    private MiniDefenceGame m_miniDefenceGame;
    private Vector3 m_targetPosition;
    private float m_monsterSpeed;
    private bool m_endCheck = false;

    private void Awake()
    {
        m_miniDefenceGame = transform.GetComponentInParent<MiniDefenceGame>();
    }

    // Update is called once per frame
    private void Start()
    {
        m_monsterSpeed = m_miniDefenceGame.m_monsterSpeed;
        m_targetPosition = gameObject.transform.position;
        m_targetPosition.z = -6;
    }

    private void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_targetPosition, m_monsterSpeed);
        if(gameObject.transform.position == m_targetPosition && m_endCheck == false)
        {
            m_endCheck = true;
            m_miniDefenceGame.DefenseFail();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            if(other.gameObject.CompareTag("Target"))
            {
                EffectSoundManager.Instance.PlayEffect(27);
                other.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
            }
        }
    }

}
