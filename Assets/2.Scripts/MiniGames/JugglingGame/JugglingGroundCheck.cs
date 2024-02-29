using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugglingGroundCheck : MonoBehaviour
{
    private JugglingGame m_jugglingGame;
    private bool m_endCheck = false;

    private void Awake()
    {
        m_jugglingGame = transform.GetComponentInParent<JugglingGame>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Target") && m_endCheck == false)
            {
                m_endCheck = true;
                m_jugglingGame.JugglingFail();
            }
        }
    }
}
