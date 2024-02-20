using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectSceneController : ButtonHandler
{

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 클릭시 RAY를 활용하여 타겟 찾기
            Camera camera = CameraManager.Instance.GetCamera();//카메라 매니저에서 현재 카메라를 받아옴
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);//카메라 기준 레이 생성
            RaycastHit hit;
            if (EventSystem.current.IsPointerOverGameObject(0))
            {
                return;
            }
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Target")
                {
                    MiniGameManager.Instance.GameNumber = Int32.Parse(hit.collider.gameObject.name);
                    GameSceneManager.Instance.PopUpSelect(SCENES.GameChoiceScene);
                }
            }
        }
    }
}
