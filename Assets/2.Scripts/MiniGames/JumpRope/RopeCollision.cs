using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Rope"))
        {
            JumpRopeGame rg = transform.parent.GetComponent<JumpRopeGame>();
            rg.SetCollision();
            rg.CheckWin();
        }
    }
}
