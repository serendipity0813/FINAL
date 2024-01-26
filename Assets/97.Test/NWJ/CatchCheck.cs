using UnityEngine;

public class CatchCheck : MonoBehaviour
{
    private BasketGame m_game;

    private void Start()
    {
        m_game = transform.parent.parent.GetComponent<BasketGame>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        Destroy(other.gameObject);

        if (other.tag == "target")
        {
            m_game.Lose();//음식이외의 오브젝트가 담기면 게임 종료
        }
        else
        {
            m_game.AddCount();//음식이 바구니에 담겼을 때 갯수 증가
        }

        if(m_game.CheckClear())
        {
            m_game.Win();
        }
    }
}
