using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text scoreText;
    public Button retryButton;
    public Button exitButton;

    public AudioClip gameOverSound;  // 게임 오버 사운드 추가
    private AudioSource audioSource;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        retryButton.onClick.AddListener(RestartGame);
        exitButton.onClick.AddListener(ExitGame);

        // 오디오 소스 설정
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void ShowGameOverScreen(int finalScore)
    {
        ScoreManager.Instance.HideScoreUI();
        gameOverPanel.SetActive(true);
        scoreText.text = "Score: " + finalScore.ToString();

        // 게임 오버 사운드 재생
        if (gameOverSound != null)
            audioSource.PlayOneShot(gameOverSound);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("게임 종료! (에디터에서는 작동하지 않을 수 있음)");
    }
}
