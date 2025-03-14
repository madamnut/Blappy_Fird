using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;
    public float rotationSpeed = 5f;
    private Rigidbody2D rb;

    private float maxUpAngle = 30f;    // 상승 시 최대 각도
    private float maxDownAngle = -80f; // 낙하 시 최대 각도

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionX; // x축 고정
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameRunning)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        RotateCharacter();
    }

    void RotateCharacter()
    {
        float angle;

        if (rb.velocity.y > 0)
        {
            angle = Mathf.LerpAngle(transform.eulerAngles.z, maxUpAngle, rotationSpeed * Time.deltaTime);
        }
        else
        {
            angle = Mathf.LerpAngle(transform.eulerAngles.z, maxDownAngle, rotationSpeed * Time.deltaTime);
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            GameManager.Instance.HandleCollision();
        }
    }
}
