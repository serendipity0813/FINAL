using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    private CarDrivingGame carDrivingGame;
    private int m_carHitCount;
    private void Awake()
    {
        carDrivingGame = transform.parent.GetComponent<CarDrivingGame>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.CompareTag("Target") == true)
        {
            carDrivingGame.clearCount++;
        }

    }

}
