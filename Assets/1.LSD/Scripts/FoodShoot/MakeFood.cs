using System.Collections;
using UnityEngine;

public class MakeFood : MonoBehaviour
{
    [SerializeField] private GameObject Banana;
    [SerializeField] private GameObject Cheese;
    [SerializeField] private GameObject Cherry;
    [SerializeField] private int repetition; // 몇번 반복할지
    [SerializeField] private Transform parentTrn;

    void Awake()
    {
        parentTrn = transform.parent;
    }

    void Start()
    {
        StartCoroutine(FoodMakeCoroutine()); // 코루틴 실행
    }

    IEnumerator FoodMakeCoroutine() // 코루틴 repetition 값 만큼 반복
    {
        while (repetition != 0)
        {
            if (gameObject == null) // 게임 오브젝트가 파괴되면 코루틴 종료
                yield break;

            FoodContainerPosition(); // 포지션을 바꾼다.
            repetition--; // 1번 동작할때마다 repetition 값을 -
            yield return new WaitForSeconds(.5f); // 0.5초에 한번씩
        }
    }

    void FoodContainerPosition() // 푸드 컨테이너 위치값 랜덤 설정
    {
        float rndY = Random.Range(1f, -6.5f); // Y값 위치를 만들기 위한 랜덤
        int rnd = Random.Range(0, 2); // 50%를 만들기 위한 랜덤
        Vector3 position; // 새로 생성된 포지션 값을 할당하기 위한 백터 선언
        if (rnd == 0)
        {
            position = new Vector3(-6 + parentTrn.position.x, rndY + parentTrn.position.y, parentTrn.position.z); // rnd 가 0일시 해당 좌표로 이동
        }
        else
        {
            position = new Vector3(6 + parentTrn.position.x, rndY + parentTrn.position.y, parentTrn.position.z);// rnd 가 1일시 해당 좌표로 이동
        }
        MakeFoods(position); // position 위치에 푸드를 만든다.
    }

    void MakeFoods(Vector3 position) // 랜덤 푸드 생성
    {
        int rnd = Random.Range(0, 3);
        // rnd의 값에 맞춰서 switch 문 동작
        switch (rnd)
        {
            // 푸드 생성
            case 0:
                Instantiate(Banana, position, Quaternion.identity, transform);
                break;
            case 1:
                Instantiate(Cheese, position, Quaternion.identity, transform);
                break;
            case 2:
                Instantiate(Cherry, position, Quaternion.identity, transform);
                break;
        }
    }
}

