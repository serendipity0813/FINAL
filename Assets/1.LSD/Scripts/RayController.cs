using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class RayController : MonoBehaviour
{
    [SerializeField] private int bulletCount;
    [SerializeField] private int winningCount;
    void Awake()
    {
        bulletCount = 5;
        winningCount = 0;
    }
    void Update()
    {
        if (bulletCount != 0)
        {
            if (Input.GetMouseButtonDown(0))  // 마우스 왼쪽 버튼 클릭 시
            {
                bulletCount--;
                HitFood();  // 레이 발사 함수 호출

                if (winningCount >= 3)
                {
                    // 승리시 로직
                    bulletCount = 0;
                    Debug.Log("이겼다!");
                }
                else if (winningCount < 3 && bulletCount == 0)
                {
                    // 패배시 로직
                    Debug.Log("졌다!");
                }
            }
        }
    }
    void HitFood()
    {
        // 마우스 위치에서 레이 생성
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // 레이캐스트 히트 정보 받아오기
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))  // 레이캐스트 실행
        {
            // 맞은 곳에 어떤 동작을 수행하도록 함
            GameObject hitObject = hit.collider.gameObject;
            winningCount++;
            Destroy(hitObject);
        }
    }
}
