using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsGameRunning { get; private set; } = true;
    public float GameTime { get; private set; } = 0f;
    public GameObject player;
    public GameObject obstaclePrefab; // 장애물 프리팹 (부모 객체)

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
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
    }

    public void ResumeGame()
    {
        IsGameRunning = true;
        Debug.Log("게임 재개!");
    }

    private void CheckCollision()
    {
        if (player == null || obstaclePrefab == null)
            return;

        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle"); // 현재 씬의 모든 장애물 가져오기
        Collider2D playerCollider = player.GetComponent<Collider2D>();

        if (playerCollider == null)
            return;

        foreach (GameObject obstacle in obstacles)
        {
            Collider2D[] obstacleColliders = obstacle.GetComponentsInChildren<Collider2D>(); // 자식 오브젝트들의 Collider2D 가져오기

            foreach (Collider2D obstacleCollider in obstacleColliders)
            {
                // 실제 콜라이더의 형태를 기준으로 충돌 체크
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

        // 게임 오버 UI 호출
        GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOverScreen(ScoreManager.Instance.GetScore());
        }
    }

}
