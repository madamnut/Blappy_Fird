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

    private void Start()
    {
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
            LoadGameScene();
        }
    }

    // 게임 씬으로 이동
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
