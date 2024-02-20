using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RunningGoal : MonoBehaviour
{
    private RunningGame m_game;

    // Start is called before the first frame update
    void Start()
    {
        m_game = transform.parent.GetComponent<RunningGame>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_game.Win();
        }
    }
}
