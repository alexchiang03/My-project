using UnityEngine;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    public static HUDController Instance;

    private Label scoreLabel;
    private Label startLabel;
    private Label finalScoreLabel;
    private VisualElement gameOverPanel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ShowStart();
    }

    private void OnEnable()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        VisualElement root = uiDocument.rootVisualElement;

        scoreLabel = root.Q<Label>("scoreLabel");
        startLabel = root.Q<Label>("startLabel");
        finalScoreLabel = root.Q<Label>("finalScoreLabel");
        gameOverPanel = root.Q<VisualElement>("gameOverPanel");
    }

    public void UpdateScore(int score)
    {
        scoreLabel.text = "Score: " + score;
    }

    public void ShowStart()
    {
        scoreLabel.text = "Score: 0";

        startLabel.style.display = DisplayStyle.Flex;
        gameOverPanel.style.display = DisplayStyle.None;
    }

    public void StartGame()
    {
        startLabel.style.display = DisplayStyle.None;
        gameOverPanel.style.display = DisplayStyle.None;
    }

    public void ShowGameOver(int finalScore)
    {
        startLabel.style.display = DisplayStyle.None;
        gameOverPanel.style.display = DisplayStyle.Flex;

        finalScoreLabel.text = "Final Score: " + finalScore;
    }
}