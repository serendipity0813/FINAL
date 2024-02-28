using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ArrowShootBtn : MonoBehaviour
{
    private ArrowShoot m_arrow;     // 스크립트
    private Button m_button;        // 자신 버튼
    private bool m_isShoot = false; // 쿨타임 관련

    private void Awake()
    {
        m_button = GetComponent<Button>();
        // 자신,부모의,부모 안에서 ArrowShoot 스크립트 가져오기
        m_arrow = transform.parent.parent.GetComponentInChildren<ArrowShoot>();
        m_button.onClick.AddListener(Shoot);
    }
    private void Start()
    {
        Invoke(nameof(isShootCoolTime), 2f); // 2초 뒤에 누를 수 있게
    }
    private void Shoot()
    {
        // 화살을 날릴 수 있다면
        if (m_isShoot)
        {
            EffectSoundManager.Instance.PlayEffect(16);
            StartCoroutine(ShootCoolTime());
        }
    }
    private void isShootCoolTime()
    {
        m_isShoot = true;
    }
    IEnumerator ShootCoolTime()
    {
        if (m_arrow != null)
        {
            m_isShoot = false; // 화살 쿨타임
            m_arrow.ShootArrow(); // ArrowShoot 안의 ShootArrow() 메소드 실행
        }
        yield return new WaitForSeconds(1f); // 1초 쿨타임
        if (!m_isShoot)
        {
            m_isShoot = true; // 초기화
        }
    }
}
