using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class FlyFood : MonoBehaviour
{
    float xValue;
    Rigidbody rb;
    [SerializeField] int power;
    [SerializeField] int jumpPower;

    void Awake()
    {
        xValue = transform.position.x; // 생성시 푸드 x 값 받아오기
        rb = GetComponent<Rigidbody>(); // AddForce 함수를 위한 rb
    }
    void Start()
    {
        FlyForce();
        Invoke("DestroyFood", 2.5f);
    }
    void FlyForce()
    {
        int powerRnd = Random.Range(power - 2, power + 2);
        int jumpPowerRnd = Random.Range(jumpPower - 2, jumpPower + 2);
        if (xValue > 0)
        {
            rb.AddForce(-powerRnd, jumpPowerRnd, 0, ForceMode.Impulse);
            // Torque
        }
        else if (xValue < 0)
        {
            rb.AddForce(powerRnd, jumpPowerRnd, 0, ForceMode.Impulse);
        }
    }
    void DestroyFood()
    {
        Destroy(gameObject);
    }
}
