using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRope : MonoBehaviour
{
    public GameObject rope;
    private float m_rotateSpeed = 90.0f;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        rope.transform.Rotate(new Vector3(0, 0, Time.deltaTime* m_rotateSpeed));
    }

    public void CollisionDetected()
    {
        Debug.Log("GameOver");
    }
}
