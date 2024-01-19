using UnityEngine;

public class FoodEatPrefabs : MonoBehaviour
{
    FoodEat foodEat;

    private void Awake()
    {
        foodEat = GetComponentInParent<FoodEat>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            foodEat.m_clearCount--;
            Destroy(gameObject);
        }
    }
}
