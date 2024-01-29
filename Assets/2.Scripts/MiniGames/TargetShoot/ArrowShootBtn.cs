using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ArrowShootBtn : MonoBehaviour
{
    private ArrowShoot m_arrow;
    private Button m_button;
    private bool m_isShoot;

    private void Awake()
    {
        m_button = GetComponent<Button>();
        m_arrow = transform.parent.parent.GetComponentInChildren<ArrowShoot>();
        m_button.onClick.AddListener(Shoot);
        m_isShoot = false;
        Invoke(nameof(isShootCoolTime), 1f);
    }
    private void Shoot()
    {
        if (m_isShoot)
        {
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
            m_isShoot = false;
            m_arrow.ShootArrow();
            m_arrow.m_isfly = true;
        }
        yield return new WaitForSeconds(1.0f);
        if (!m_isShoot)
        {
            m_isShoot = true;
        }
    }
}
