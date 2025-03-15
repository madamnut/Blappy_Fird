using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private int score = 0;
    public Text scoreText;

    public AudioClip scoreSound; // 점수 획득 사운드 추가
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateScoreUI();

        // 오디오 소스 설정
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("현재 점수: " + score);
        UpdateScoreUI();

        // 점수 획득 효과음 재생
        if (scoreSound != null)
        {
            audioSource.PlayOneShot(scoreSound, 0.8f); // 볼륨 80%로 재생
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void HideScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(false);
        }
    }
}
