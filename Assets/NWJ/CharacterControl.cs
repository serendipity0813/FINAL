using UnityEngine;

public class CharacterControl : MonoBehaviour
{

    private float m_speed = 3.0f;

    private void Update()
    {
        if (TouchManager.instance.IsDragging())//터치 누르고 있는 상태일때
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 hitpos;

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Terrain")
            {
                hitpos = hit.point;
            }
            else
            {
                hitpos = transform.position;
            }

            hitpos = hitpos - transform.position;
            hitpos.y = gameObject.GetComponent<Rigidbody>().velocity.y;
            gameObject.GetComponent<Rigidbody>().velocity = hitpos.normalized * m_speed;
        }

        
        if (TouchManager.instance.IsDragUp())
        {

            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 400, 0));
        }
        
    }

}
