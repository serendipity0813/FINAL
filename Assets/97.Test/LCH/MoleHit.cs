using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleHit : MonoBehaviour
{
    public int points = 10; // Points this mole gives when hit

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for mouse click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == this.transform)
                {
                    OnHit();
                }
            }
        }
    }

    void OnHit()
    {
        // Add logic for what happens when the mole is hit

        ScoreManager.Instance.AddScore(points);
        // You can also add logic here to hide the mole when hit
    }
}