using UnityEngine;

public class BasketGameGround: MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Terrain")//음식이 지면에 닿았을 때 제거
        {
            Destroy(gameObject);
        }
    }
}
