using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBall : MonoBehaviour
{
    float m_timer = 0;
    private void Update()
    {
        m_timer += Time.deltaTime;

        if (m_timer > 2)
            Destroy(this);
    }
}
