using UnityEngine;

public class JumpJumpFall : MonoBehaviour
{
    private JumpJumpBtn jumpJumpBtn;

    private void Awake()
    {
        jumpJumpBtn = transform.parent.GetComponentInChildren<JumpJumpBtn>();
    }
    private void OnTriggerEnter(Collider other)
    {
        jumpJumpBtn.PlayerSave();
        EffectSoundManager.Instance.PlayEffect(18);
    }
}
