using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBallController : MonoBehaviour
{
    [SerializeField] private GameObject m_RealBallObject;
    [SerializeField] private GameObject m_FakeBallObject;

    private void Update()
    {
        if(this.transform.position.y < 0)
        {
            m_FakeBallObject.SetActive(false);
            m_RealBallObject.SetActive(true);
        }
        else if (this.transform.position.y < -10)
        {
            m_RealBallObject.SetActive(false);
        }
    }


}
