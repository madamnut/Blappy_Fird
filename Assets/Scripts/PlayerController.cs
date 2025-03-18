using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;
    public float rotationSpeed = 5f;
    private Rigidbody2D rb;

    private float maxUpAngle = 30f;
    private float maxDownAngle = -80f;

    public AudioClip jumpSound; // 점프 사운드 추가
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;

        // 오디오 소스 설정
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameRunning)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
            
            // 점프 사운드 재생
            if (jumpSound != null)
                audioSource.volume = 0.5f;
                audioSource.PlayOneShot(jumpSound);
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
