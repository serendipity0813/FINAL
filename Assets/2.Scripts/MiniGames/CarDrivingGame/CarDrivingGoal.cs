using UnityEngine;

public class CarDrivingGoal : MonoBehaviour
{
    private CarDrivingGame m_game;

    private void Start()
    {
        m_game = transform.parent.GetComponent<CarDrivingGame>();
    }

    private void OnTriggerEnter(Collider other)
    {
        m_game.Win();//도로 끝에 도달하면 승리
    }
}
