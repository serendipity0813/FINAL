using UnityEngine;

public class RopeCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            JumpRopeGame rg = transform.parent.parent.GetComponent<JumpRopeGame>();
            rg.SetCollision();
            rg.CheckWin();
        }
    }
}
