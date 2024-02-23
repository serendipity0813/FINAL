using UnityEngine;

//Player객체 안의 자식으로 발밑의 Collider가 Terrain과의 접촉 여부에 따라 땅에 닿아있는지 판별하는 클래스
public class GroundCheck : MonoBehaviour
{
    private DragToMoveController m_controller;//현재 플레이어의 이동 컨트롤러 클래스를 받아옴

    private void Start()
    {
        m_controller = transform.parent.GetComponent<DragToMoveController>();//부모 객체에서 이동 컨트롤러를 가져온다.
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Terrain")
        {
            m_controller.SetIsGround(true);//땅에 닿아있을 때 true로 변경
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Terrain")
        {
            m_controller.SetIsGround(false);//땅에서 떨어졌을 때 false로 변경
        }
    }
}
