using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsGameRunning { get; private set; } = true;
    public float GameTime { get; private set; } = 0f;
    public GameObject player;
    public GameObject obstaclePrefab;

    public AudioClip bgmClip;  // 인게임 BGM 오디오 클립 추가
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
        // 기존 코드 유지
        PlayBGM(); // BGM 실행
    }

    private void Update()
    {
        if (IsGameRunning)
        {
            GameTime += Time.deltaTime;
            CheckCollision();
        }
    }

    public void StopGame()
    {
        IsGameRunning = false;
        Debug.Log("게임 멈춤!");
        StopBGM(); // 게임이 멈출 때 BGM도 정지
    }

    public void ResumeGame()
    {
        IsGameRunning = true;
        Debug.Log("게임 재개!");
        PlayBGM(); // 다시 게임 시작 시 BGM 재생
    }

    private void CheckCollision()
    {
        if (player == null || obstaclePrefab == null)
            return;

        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        Collider2D playerCollider = player.GetComponent<Collider2D>();

        if (playerCollider == null)
            return;

        foreach (GameObject obstacle in obstacles)
        {
            Collider2D[] obstacleColliders = obstacle.GetComponentsInChildren<Collider2D>();

            foreach (Collider2D obstacleCollider in obstacleColliders)
            {
                if (playerCollider.IsTouching(obstacleCollider)) 
                {
                    HandleCollision();
                    return;
                }
            }
        }
    }

    public void HandleCollision()
    {
        StopGame();
        GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOverScreen(ScoreManager.Instance.GetScore());
        }
    }

    // 🔹 BGM 실행
    private void PlayBGM()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.playOnAwake = false;
            audioSource.volume = 1f;
        }

        if (bgmClip != null && !audioSource.isPlaying)
        {
            audioSource.clip = bgmClip;
            audioSource.Play();
        }
    }

    // 🔹 BGM 정지
    private void StopBGM()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
