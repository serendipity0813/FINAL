using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            if (PlayerDataManager.instance.m_playerData.equipSkin[i] == true)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
                transform.GetChild(i).gameObject.SetActive(false);
        }

        //transform.GetChild(1).gameObject.SetActive(true);
    }

    //프리펩 내에서 여러 모델링들을 SetActive를 켯다 껏다 해주므로
    //현재 활성화 되어있는 오브젝트의 Animator를 리턴하는 함수가 필요함
    public AnimatorUpdater GetPlayerAnimator()
    {
        AnimatorUpdater animator = null;

        for (int i = 0; i < 10; i++)
        {
            if (PlayerDataManager.instance.m_playerData.equipSkin[i] == true)
            {
                animator = transform.GetChild(i).GetComponent<AnimatorUpdater>();
            }
        }

        //장착한 스킨을 알 수 없을 때
        if(animator == null)
        {
            animator = transform.GetChild(0).GetComponent<AnimatorUpdater>();//가장 첫번째 스킨을 디폴트로 지정
        }

        return animator;
    }

}
