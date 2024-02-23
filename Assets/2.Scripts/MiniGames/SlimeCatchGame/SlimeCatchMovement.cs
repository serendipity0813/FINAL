using UnityEngine;

public class SlimeCatchMovement : MonoBehaviour
{
    private Vector3 m_targetPosition;   //다음으로 이동할 위치
    private float m_slimeSpeed;      //모기의 이동속도
    float m_xPosition;
    float m_yPosition;
    private bool m_soundFlag = false;

    private void Start()
    {
      
        m_targetPosition = new Vector3(0, 0, 0);
        m_slimeSpeed = (PlayerDataManager.instance.m_playerData.stage / 3 + 1) * 0.03f + 0.07f;
    }

    private void FixedUpdate()
    {
        if (true)
        {
            //현재 오브젝트의 위치가 다음 포지션과 다를 경우 해당 포지션으로 이동하도록 함
            if (gameObject.transform.position != m_targetPosition)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_targetPosition, m_slimeSpeed);
            }
            //만약 같을 경우 다음 포지션을 찾아야하는지 체크 후 랜덤한 다음 포지션을 설정
            else
            {
                m_soundFlag = true;
                m_xPosition = Random.Range(-3, 4);
                m_yPosition = Random.Range(-4, 7);
                m_targetPosition = new Vector3(m_xPosition, m_yPosition, 0);
            }
        }

        if (m_soundFlag == true)
        {
            EffectSoundManager.Instance.PlayEffect(28);
            m_soundFlag = false;
        }
    }
}
