using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSceneController : ButtonHandler
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 클릭시 RAY를 활용하여 타겟 찾기
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Target")
                {
                    MiniGameManager.Instance.GameName = hit.collider.gameObject.name;
                    GameSceneManager.Instance.PopUpSelect(SCENES.GameChoiceScene);
                }

            }
        }
    }


}
