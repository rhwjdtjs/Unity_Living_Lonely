using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene; // 다음에 로드할 씬의 이름을 저장하는 변수
    [SerializeField] Image progressBar; // 진행 상황을 표시할 이미지 프로그래스바

    private void Start()
    {
        StartCoroutine(LoadScene()); // 씬 로딩 코루틴을 시작한다.
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName; // 다음에 로드할 씬의 이름을 설정한다.
        SceneManager.LoadScene("LoadingScene"); // 로딩 씬을 로드한다.
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene); // 비동기로 씬을 로드하는 작업을 시작한다.
        op.allowSceneActivation = false; // 씬 로드가 완료되면 자동으로 씬을 활성화하지 않도록 설정한다.

        float timer = 0.0f; // 타이머 변수를 초기화한다.

        while (!op.isDone) // 씬 로드가 완료될 때까지 반복한다.
        {
            yield return null;
            timer += Time.deltaTime; // 타이머를 업데이트한다.

            if (op.progress < 0.9f) // 씬의 진행 상황이 0.9보다 작을 경우
            {
                // 프로그래스바를 씬의 진행 상황으로 서서히 채워나간다.
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);

                if (progressBar.fillAmount >= op.progress) // 프로그래스바가 씬의 진행 상황과 같거나 커졌을 경우
                {
                    timer = 0f; // 타이머를 초기화한다.
                }
            }
            else // 씬의 진행 상황이 0.9보다 크거나 같을 경우
            {
                // 프로그래스바를 서서히 1로 채워나간다.
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);

                if (progressBar.fillAmount == 1.0f) // 프로그래스바가 1로 채워졌을 경우
                {
                    op.allowSceneActivation = true; // 씬을 활성화하도록 설정한다.
                    yield break; // 코루틴을 종료한다.
                }
            }
        }
    }
}
