using UnityEngine;

public class CarHitChecker : MonoBehaviour
{
    private CarDrivingGame m_game;

    private void Start()
    {
        m_game = transform.parent.GetComponent<CarDrivingGame>();
    }
 
    private void OnCollisionEnter(Collision hit)
    {
        if (hit.transform.tag == "Target")
        {
            m_game.Lose();//차에 부딪쳤을 때 게임 종료
        }
    }

}
