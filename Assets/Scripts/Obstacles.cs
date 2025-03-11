using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float scrollSpeed = 3f;
    public float spawnInterval = 3f;
    public float minY = -2.5f;
    public float maxY = 2.5f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0;
        }
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
        transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        // 카메라 왼쪽 바깥으로 나가면 삭제
        float destroyX = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect - 1f;
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
