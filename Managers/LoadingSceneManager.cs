using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene; // ������ �ε��� ���� �̸��� �����ϴ� ����
    [SerializeField] Image progressBar; // ���� ��Ȳ�� ǥ���� �̹��� ���α׷�����

    private void Start()
    {
        StartCoroutine(LoadScene()); // �� �ε� �ڷ�ƾ�� �����Ѵ�.
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName; // ������ �ε��� ���� �̸��� �����Ѵ�.
        SceneManager.LoadScene("LoadingScene"); // �ε� ���� �ε��Ѵ�.
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene); // �񵿱�� ���� �ε��ϴ� �۾��� �����Ѵ�.
        op.allowSceneActivation = false; // �� �ε尡 �Ϸ�Ǹ� �ڵ����� ���� Ȱ��ȭ���� �ʵ��� �����Ѵ�.

        float timer = 0.0f; // Ÿ�̸� ������ �ʱ�ȭ�Ѵ�.

        while (!op.isDone) // �� �ε尡 �Ϸ�� ������ �ݺ��Ѵ�.
        {
            yield return null;
            timer += Time.deltaTime; // Ÿ�̸Ӹ� ������Ʈ�Ѵ�.

            if (op.progress < 0.9f) // ���� ���� ��Ȳ�� 0.9���� ���� ���
            {
                // ���α׷����ٸ� ���� ���� ��Ȳ���� ������ ä��������.
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);

                if (progressBar.fillAmount >= op.progress) // ���α׷����ٰ� ���� ���� ��Ȳ�� ���ų� Ŀ���� ���
                {
                    timer = 0f; // Ÿ�̸Ӹ� �ʱ�ȭ�Ѵ�.
                }
            }
            else // ���� ���� ��Ȳ�� 0.9���� ũ�ų� ���� ���
            {
                // ���α׷����ٸ� ������ 1�� ä��������.
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);

                if (progressBar.fillAmount == 1.0f) // ���α׷����ٰ� 1�� ä������ ���
                {
                    op.allowSceneActivation = true; // ���� Ȱ��ȭ�ϵ��� �����Ѵ�.
                    yield break; // �ڷ�ƾ�� �����Ѵ�.
                }
            }
        }
    }
}
