using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectSceneController : ButtonHandler
{
    [SerializeField] private GameObject[] m_shootingBalls;
    [SerializeField] private GameObject[] m_Walls;
    private Rigidbody m_shootingBallRigidBody;
    private Vector3 m_shootingBallPosition;
    private int m_shootingBallIndex = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 클릭시 RAY를 활용하여 타겟 찾기
            Camera camera = CameraManager.Instance.GetCamera();//카메라 매니저에서 현재 카메라를 받아옴
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);//카메라 기준 레이 생성
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Target")
                {
                    MiniGameManager.Instance.GameNumber = Int32.Parse(hit.collider.gameObject.name);
                    GameSceneManager.Instance.PopUpSelect(SCENES.GameChoiceScene);
                }
            }
        }
    }

    public  void UpShoot()
    {
        float x = m_Walls[3].transform.position.x - (float)0.1;
        float xrange = UnityEngine.Random.Range(-x, x);
        float z = m_Walls[1].transform.position.z + (float)0.1;
        m_shootingBallPosition = new Vector3(xrange, (float)-1.5, z);
        ShootReady();
        m_shootingBallRigidBody.AddForce(new Vector3(0, 0, 1) * 25.0f, ForceMode.Impulse);
    }

    public void DownShoot()
    {
        float x = m_Walls[3].transform.position.x - (float)0.1;
        float xrange = UnityEngine.Random.Range(-x, x);
        float z = m_Walls[0].transform.position.z - (float)0.1;
        m_shootingBallPosition = new Vector3(xrange, (float)-1.5, z);
        ShootReady();
        m_shootingBallRigidBody.AddForce(new Vector3(0,0,-1) * 25.0f, ForceMode.Impulse);
    }

    public void LeftShoot()
    {
        float z = m_Walls[0].transform.position.z - (float)0.1;
        float zrange = UnityEngine.Random.Range(-z, z);
        float x = m_Walls[3].transform.position.x - (float)0.1;
        m_shootingBallPosition = new Vector3(x, (float)-1.5, zrange);
        ShootReady();
        m_shootingBallRigidBody.AddForce(new Vector3(-1, 0, 0) * 25.0f, ForceMode.Impulse);

    }

    public void RightShoot()
    {
        float z = m_Walls[0].transform.position.z - (float)0.1;
        float zrange = UnityEngine.Random.Range(-z, z);
        float x = m_Walls[2].transform.position.x + (float)0.1;
        m_shootingBallPosition = new Vector3(x, (float)-1.5, zrange);
        ShootReady();
        m_shootingBallRigidBody.AddForce(new Vector3(1, 0, 0) * 25.0f, ForceMode.Impulse);
    }

    private void ShootReady()
    {

        if (m_shootingBallIndex < 10)
        {
            if(m_shootingBallIndex == 0)
            {
                m_shootingBalls[m_shootingBallIndex].SetActive(true);
            }
            else
            {
                m_shootingBalls[m_shootingBallIndex-1].SetActive(false);
                m_shootingBalls[m_shootingBallIndex].SetActive(true);
            }
           
        }        
        else
        {
            m_shootingBalls[m_shootingBallIndex - 1].SetActive(false);
            m_shootingBallIndex -= 10;
            m_shootingBalls[m_shootingBallIndex].SetActive(true);
        }

        m_shootingBalls[m_shootingBallIndex].transform.position = m_shootingBallPosition;
        m_shootingBallRigidBody = m_shootingBalls[m_shootingBallIndex].GetComponent<Rigidbody>();
        m_shootingBallIndex++;
    }

   

}
