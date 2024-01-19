using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodEatPrefabs : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
            FoodEat foodEat = GetComponentInParent<FoodEat>();
            foodEat.m_winCount--;
            Destroy(gameObject);
        }
    }
}
