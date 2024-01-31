using UnityEngine;

public class MoleHit : MonoBehaviour
{
    public int points = 1; // Points for hitting the mole

    private void OnMouseDown()
    {
        MoleController.Instance.MoleHit(this.gameObject); // Notify MoleController that this mole was hit
        GetComponent<Collider>().enabled = false; // Disable collider to prevent double hits
    }
}
