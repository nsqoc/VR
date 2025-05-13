using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Awake()
    {
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
        Debug.Log("🟢 AddScore() called. Adding: " + points + " points.");
        score += points;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText)
        {
            Debug.Log("📢 UI updated: " + score);
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogError("🚨 `scoreText` is not assigned in the Inspector!");
        }
    }
}
