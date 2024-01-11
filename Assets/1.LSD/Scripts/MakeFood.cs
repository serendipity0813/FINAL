using System.Collections;
using UnityEngine;

public class MakeFood : MonoBehaviour
{
    [SerializeField] private GameObject Banana;
    [SerializeField] private GameObject Cheese;
    [SerializeField] private GameObject Cherry;
    [SerializeField] private int repetition; // 몇번 반복할지

    void Start()
    {
        StartCoroutine(FoodMakeCoroutine()); // 코루틴 실행
    }

    IEnumerator FoodMakeCoroutine() // 코루틴 repetition 값 만큼 반복
    {
        while (repetition != 0)
        {
            FoodContainerPosition(); // 포지션을 바꾸고
            MakeFoods(); // 푸드를 만든다.
            repetition--; // 1번 동작할때마다 repetition 값을 -
            yield return new WaitForSeconds(.5f); // 0.5초에 한번씩
        }
    }

    void FoodContainerPosition() // 푸드 컨테이너 위치값 랜덤 설정
    {
        float rndY = Random.Range(1f, -6.5f); // Y값 위치를 만들기 위한 랜덤
        int rnd = Random.Range(0, 2); // 50%를 만들기 위한 랜덤
        if (rnd == 0)
        {
            transform.position = new Vector3(-6, rndY, 0); // rnd 가 0일시 해당 좌표로 이동
        }
        else
        {
            transform.position = new Vector3(6, rndY, 0);// rnd 가 1일시 해당 좌표로 이동
        }
    }

    void MakeFoods() // 랜덤 푸드 생성
    {
        int rnd = Random.Range(0, 3);
        switch (rnd)
        {
            case 0:
                Instantiate(Banana, transform.position, Quaternion.identity);
                break;
            case 1:
                Instantiate(Cheese, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(Cherry, transform.position, Quaternion.identity);
                break;
        }
    }
}

