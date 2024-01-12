using UnityEngine;

public class FlyFood : MonoBehaviour
{
    private float xValue;
    private Rigidbody rb;
    [SerializeField] private int power;
    [SerializeField] private int jumpPower;
    private Transform trn;

    void Awake()
    {
        rb = GetComponent<Rigidbody>(); // AddForce, AddTorque 함수를 위한 rb
        trn = transform.parent.transform.parent; // 인스턴스로 생성된 부모(FoodContainer) 의 부모 (MiniGame - FoodShoot)
        xValue = transform.position.x; // 처음 생성된 위치 x 값 받아오기
    }
    void Start()
    {
        FlyForce(); // 한번만 실행
        Invoke(nameof(DestroyFood), 2.5f);
    }
    void FlyForce()
    {
        // powerRnd = power의 -2 ~ +2 값으로 변경
        int powerRnd = Random.Range(power - 2, power + 2);

        // jumpPowerRnd = jumpPower의 -2 ~ +2 값으로 변경
        int jumpPowerRnd = Random.Range(jumpPower - 2, jumpPower + 2);
        // xValue 값이 MiniGame - FoodShoot 포지션 값보다 클 경우 오른쪽에서 왼쪽으로 발사
        if (xValue > trn.position.x)
        {
            rb.AddForce(-powerRnd, jumpPowerRnd, 0, ForceMode.Impulse);
            rb.AddTorque(RotationPowerRnd(), RotationPowerRnd(), RotationPowerRnd());
        }
        // xValue 값이 MiniGame - FoodShoot 포지션 값보다 작을 경우 왼쪽에서 오른쪽으로 발사
        else if (xValue < trn.position.x) 
        {
            rb.AddForce(powerRnd, jumpPowerRnd, 0, ForceMode.Impulse);
            rb.AddTorque(RotationPowerRnd(), RotationPowerRnd(), RotationPowerRnd());
        }
    }

    int RotationPowerRnd() // Food -1000부터 1000까지 회전값 랜덤
    {
        int rotationPowerRnd = Random.Range(-1000, 1001);
        return rotationPowerRnd;
    }

    // Invoke("DestroyFood", 2.5f); 함수에 필요한 함수, 2.5초 이후 오브젝트 삭제
    void DestroyFood()
    {
        Destroy(gameObject);
    }
}
