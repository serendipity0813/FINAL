using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleHit : MonoBehaviour
{
    public float hitForce = 5f; // Force applied when hit

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
        Debug.Log("Mole Hit!");
        // Add logic for what happens when the mole is hit
        // For example, you could add a force to the mole
        // GetComponent<Rigidbody>().AddForce(Vector3.up * hitForce, ForceMode.Impulse);
    }
}