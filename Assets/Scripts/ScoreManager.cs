using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int score = 0;

    public int Score => score;

    private void Awake()
    {
        Instance = this;
    }

    public void AddScore()
    {
        score++;

        HUDController.Instance.UpdateScore(score);
    }

    public void ResetScore()
    {
        score = 0;

        HUDController.Instance.UpdateScore(score);
    }
}