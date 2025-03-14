using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;  // 게임 오버 UI 패널
    public Text scoreText;            // 최종 점수 텍스트
    public Button retryButton;        // 다시 시작 버튼
    public Button exitButton;         // 종료 버튼

    private void Start()
    {
        gameOverPanel.SetActive(false); // 시작 시 비활성화
        retryButton.onClick.AddListener(RestartGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void Update()
    {
        if (gameOverPanel.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void ShowGameOverScreen(int finalScore)
    {
        ScoreManager.Instance.HideScoreUI();
        gameOverPanel.SetActive(true); // 게임 오버 화면 표시
        scoreText.text = "Score: " + finalScore.ToString(); // 최종 점수 표시
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 다시 로드
    }

    public void ExitGame()
    {
        Application.Quit(); // 게임 종료
        Debug.Log("게임 종료! (에디터에서는 작동하지 않을 수 있음)"); // 에디터에서는 종료가 안되므로 디버그 출력
    }
}