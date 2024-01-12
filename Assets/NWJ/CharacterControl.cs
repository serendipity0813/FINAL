using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class CharacterControl: MonoBehaviour
{



    private void Update()
    {
        /*
        if(TouchManager.instance.IsDragging())//터치 누르고 있는 상태일때
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 pos;

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Terrain") 
            {
                pos = hit.point;
                pos.y = transform.position.y;
            }
            else
            {
                pos = transform.position;
            }

            
            Debug.LogFormat("{0}, {1}", transform.position, pos);
            //transform.position = Vector3.Slerp(transform.position, pos == Vector3.zero ? transform.position : pos, Time.deltaTime/10000);
            
           transform.position = pos;
        }
        */

        if(TouchManager.instance.IsDragUp())
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 400, 0));
        }

    }

}
