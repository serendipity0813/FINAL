using UnityEngine;

public class JumpJumpFall : MonoBehaviour
{
    private JumpJumpController jumpJumpController;
    

    private void Awake()
    {
        jumpJumpController = transform.parent.GetComponentInChildren<JumpJumpController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        jumpJumpController.PlayerSave();
        EffectSoundManager.Instance.PlayEffect(18);
    }
}
