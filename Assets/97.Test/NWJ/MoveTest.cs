using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    DragToMoveController dragToMoveController;

    // Start is called before the first frame update
    void Start()
    {
        CameraManager.Instance.ChangeCamera(CameraView.Angle90View);
        dragToMoveController = GetComponent<DragToMoveController>();
    }

    // Update is called once per frame
    void Update()
    {
        dragToMoveController.UpdateMoveWithJump();
    }
}
