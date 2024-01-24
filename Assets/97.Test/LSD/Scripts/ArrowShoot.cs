using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    private bool isHit = false; // 화살이 맞았는지 여부
    private ConstantForce constant;
    private void Awake()
    {
        constant = GetComponent<ConstantForce>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target") && !isHit)
        {
            isHit = true;
            constant.force = new Vector3(0f, 0f, 0f);
            StickToTarget(collision.transform);
        }
    }
    void StickToTarget(Transform target)
    {
        transform.SetParent(target);
    }
}
