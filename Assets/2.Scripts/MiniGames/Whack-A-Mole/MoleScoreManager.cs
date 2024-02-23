using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton instance
    //public Text scoreText; // Assign this in the Inspector
    private int score = 0;

    void Awake()
    {
        // Initialize the singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        //Debug.Log(score);
        //UpdateScoreUI();
    }

    //private void UpdateScoreUI()
    //{
    //    scoreText.text = "Score: " + score;
    //}
}