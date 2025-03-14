using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private bool scored = false;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (!scored && transform.position.x <= playerTransform.position.x)
        {
            ScoreManager.Instance.AddScore(1);
            scored = true;
        }
    }
}
