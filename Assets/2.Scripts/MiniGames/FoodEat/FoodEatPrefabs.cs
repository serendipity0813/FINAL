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
            EffectSoundManager.Instance.PlayEffect(1);
            foodEat.m_clearCount--;
            Destroy(gameObject);
        }
    }
}
