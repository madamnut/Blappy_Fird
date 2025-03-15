using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LobbyManager : MonoBehaviour
{
    public GameObject pressAnyKey;  // 밝아졌다가 어두워지는 객체
    public GameObject character;    // 회전하는 캐릭터
    public GameObject title;        // 커졌다 돌아오는 타이틀
    public string gameSceneName;    // 이동할 게임 씬 이름 (유니티 인스펙터에서 설정)

    public AudioClip bgmClip;       // 배경 음악
    public AudioClip pressKeySound; // Press Any Key 효과음

    private AudioSource bgmSource;  // 내부에서 생성하는 BGM 오디오 소스
    private AudioSource sfxSource;  // 내부에서 생성하는 SFX 오디오 소스

    private void Start()
    {
        // 🔹 오디오 소스 자동 생성
        bgmSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        // 🔹 배경 음악 설정 및 재생
        if (bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.loop = true;
            bgmSource.Play();
        }

        if (pressAnyKey != null)
            StartCoroutine(FadeInOut(pressAnyKey.GetComponent<Text>()));

        if (character != null)
            StartCoroutine(RotateCharacter(character.transform));

        if (title != null)
            StartCoroutine(ScaleTitle(title.transform));
    }

    private void Update()
    {
        if (Input.anyKeyDown) // 아무 키나 눌렀을 때
        {
            if (sfxSource != null && pressKeySound != null)
            {
                sfxSource.PlayOneShot(pressKeySound, 1.5f);
                sfxSource.PlayOneShot(pressKeySound); // 효과음 재생
            }
            StartCoroutine(LoadGameSceneWithDelay());
        }
    }

    // 씬 전환 전 잠시 대기 (효과음이 끝날 시간을 고려)
    private IEnumerator LoadGameSceneWithDelay()
    {
        yield return new WaitForSeconds(0.5f); // 효과음이 끝날 때까지 대기
        LoadGameScene();
    }

    private void LoadGameScene()
    {
        if (!string.IsNullOrEmpty(gameSceneName)) // 씬 이름이 설정되었는지 확인
        {
            SceneManager.LoadScene(gameSceneName);
        }
        else
        {
            Debug.LogWarning("게임 씬 이름이 설정되지 않았습니다! Unity 인스펙터에서 설정하세요.");
        }
    }

    // 🔹 Press Any Key: 천천히 밝아졌다가 어두워지는 애니메이션
    private IEnumerator FadeInOut(Text text)
    {
        float duration = 1.5f; // 페이드 인/아웃 시간
        while (true)
        {
            for (float t = 0; t <= duration; t += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(0f, 1f, t / duration);
                text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
                yield return null;
            }

            for (float t = 0; t <= duration; t += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(1f, 0f, t / duration);
                text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
                yield return null;
            }
        }
    }

    // 🔹 Character: 양쪽으로 30도씩 천천히 회전하는 애니메이션
    private IEnumerator RotateCharacter(Transform characterTransform)
    {
        float rotationAngle = 15f;
        float duration = 2f; // 한쪽으로 회전하는 데 걸리는 시간
        while (true)
        {
            for (float t = 0; t <= duration; t += Time.deltaTime)
            {
                float angle = Mathf.Lerp(-rotationAngle, rotationAngle, t / duration);
                characterTransform.rotation = Quaternion.Euler(0, 0, angle);
                yield return null;
            }

            for (float t = 0; t <= duration; t += Time.deltaTime)
            {
                float angle = Mathf.Lerp(rotationAngle, -rotationAngle, t / duration);
                characterTransform.rotation = Quaternion.Euler(0, 0, angle);
                yield return null;
            }
        }
    }

    // 🔹 Title: 커졌다가 원래 크기로 돌아오는 애니메이션
    private IEnumerator ScaleTitle(Transform titleTransform)
    {
        Vector3 originalScale = titleTransform.localScale;
        Vector3 targetScale = originalScale * 1.2f;
        float duration = 1.5f; // 확대/축소 시간

        while (true)
        {
            for (float t = 0; t <= duration; t += Time.deltaTime)
            {
                titleTransform.localScale = Vector3.Lerp(originalScale, targetScale, t / duration);
                yield return null;
            }

            for (float t = 0; t <= duration; t += Time.deltaTime)
            {
                titleTransform.localScale = Vector3.Lerp(targetScale, originalScale, t / duration);
                yield return null;
            }
        }
    }
}
