using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private TMP_Text scoreText;
    private int score = 0;

    public int Score => score;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void AddPoints(int points = 1)
    {
        score += points;
        UpdateScoreUI();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
        else
            Debug.LogWarning("Score Text is not assigned in the Inspector!");
    }
}
