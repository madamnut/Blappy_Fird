using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float scrollSpeed = 3f;
    public float spawnInterval = 1.5f;
    public float minY = -2.5f;
    public float maxY = 2.5f;

    private float timer;
    private float maxScrollSpeed = 9f;
    private float minSpawnInterval = 0.5f;
    private int maxScore = 200;

    void Update()
    {
        if (!GameManager.Instance.IsGameRunning)
            return;

        timer += Time.deltaTime;

        AdjustDifficulty();

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0;
        }
    }

    void AdjustDifficulty()
    {
        int score = ScoreManager.Instance.GetScore(); // 현재 점수 가져오기
        float t = Mathf.Clamp01((float)score / maxScore);
        scrollSpeed = Mathf.Lerp(3f, maxScrollSpeed, t);
        spawnInterval = Mathf.Lerp(1.5f, minSpawnInterval, t);
    }

    void SpawnObstacle()
    {
        float randomY = Random.Range(minY, maxY);
        float spawnX = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect + obstaclePrefab.GetComponent<SpriteRenderer>().bounds.size.x + 2;

        Vector2 spawnPosition = new Vector2(spawnX, randomY);
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

        var obstacleMovement = obstacle.AddComponent<ObstacleMovement>();
        obstacleMovement.scrollSpeed = scrollSpeed;
    }
}

public class ObstacleMovement : MonoBehaviour
{
    public float scrollSpeed;

    void Update()
    {
        if (!GameManager.Instance.IsGameRunning)
            return;

        transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        // 카메라 왼쪽 바깥으로 나가면 삭제
        float destroyX = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect - 1f;
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
