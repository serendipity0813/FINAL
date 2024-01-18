using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodEatMakeFood : MonoBehaviour
{
    [SerializeField] private GameObject m_banana;
    [SerializeField] private GameObject m_cheese;
    [SerializeField] private GameObject m_cherry;
    [SerializeField] private GameObject m_hamburger;
    [SerializeField] private GameObject m_hotdog;
    [SerializeField] private GameObject m_olive;
    [SerializeField] private GameObject m_watermelon;
    [SerializeField] private Transform m_parentTrn;
    [SerializeField] private int m_repetition;


    private void Awake()
    {
        m_parentTrn = transform.parent;
        FoodEat foodEat = GetComponentInParent<FoodEat>();
        m_repetition = foodEat.m_repetition;
    }
    void Start()
    {
        StartCoroutine(FoodMakeCoroutine()); // 코루틴 실행
    }
    IEnumerator FoodMakeCoroutine() // 코루틴 repetition 값 만큼 반복
    {
        yield return new WaitForSeconds(1f);
        while (m_repetition != 0)
        {
            if (gameObject == null) // 게임 오브젝트가 파괴되면 코루틴 종료
                yield break;

            FoodContainerPosition(); // 포지션을 바꾼다.
            m_repetition--; // 1번 동작할때마다 repetition 값을 -
        }
    }
    void FoodContainerPosition() // 푸드 컨테이너 위치값 랜덤 설정
    {
        // 화면 크기에 따라 포지션값 받아오기
        float rndX = Random.Range(6.5f, -6.6f);
        float rndZ = Random.Range(6.5f, -6.6f);
        Vector3 position; // 새로 생성된 포지션 값을 할당하기 위한 백터 선언
        position = new Vector3(rndX + m_parentTrn.position.x,.5f + m_parentTrn.position.y, rndZ + m_parentTrn.position.z);
        MakeFoods(position); // position 위치에 푸드를 만든다.
    }
    void MakeFoods(Vector3 position) // 랜덤 푸드 생성
    {
        int rnd = Random.Range(0, 7);
        // rnd의 값에 맞춰서 switch 문 동작
        switch (rnd)
        {
            // 푸드 생성
            case 0:
                Instantiate(m_banana, position, Quaternion.identity, transform);
                break;
            case 1:
                Instantiate(m_cheese, position, Quaternion.identity, transform);
                break;
            case 2:
                Instantiate(m_cherry, position, Quaternion.identity, transform);
                break;
            case 3:
                Instantiate(m_hamburger, position, Quaternion.identity, transform);
                break;
            case 4:
                Instantiate(m_hotdog, position, Quaternion.identity, transform);
                break;
            case 5:
                Instantiate(m_olive, position, Quaternion.identity, transform);
                break;
            case 6:
                Instantiate(m_watermelon, position, Quaternion.identity, transform);
                break;
        }
    }
}
