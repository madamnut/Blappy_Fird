using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public GameObject backgroundPrefab;

    private GameObject bg1;
    private GameObject bg2;
    private float backgroundWidth;

    void Start()
    {
        backgroundWidth = backgroundPrefab.GetComponent<SpriteRenderer>().bounds.size.x;

        bg1 = Instantiate(backgroundPrefab, new Vector3(0, 0, 1), Quaternion.identity, transform);
        bg2 = Instantiate(backgroundPrefab, new Vector3(backgroundWidth, 0, 1), Quaternion.identity, transform);
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameRunning)
            return;

        bg1.transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
        bg2.transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        if (bg1.transform.position.x <= -backgroundWidth)
        {
            Destroy(bg1);
            bg1 = bg2;
            bg2 = Instantiate(backgroundPrefab, new Vector3(bg1.transform.position.x + backgroundWidth, 0, 1), Quaternion.identity, transform);
        }
    }
}
