using UnityEngine;

public class MoleHit : MonoBehaviour
{
    public int points = 10; // Points awarded for hitting this mole
    public delegate void HitAction(GameObject mole);
    public event HitAction OnHit; // Event to notify when the mole is hit

    private void OnMouseDown()
    {
        // This method is called when the mole is clicked
        OnHit?.Invoke(gameObject); // Invoke the OnHit event
        // Additional logic for when the mole is hit can be added here
    }
}