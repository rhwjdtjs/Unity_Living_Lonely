using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUIScript : MonoBehaviour
{
    [SerializeField] private Text starttext; // ���� �ؽ�Ʈ
    [SerializeField] private Animator anim; // �ִϸ����� ������Ʈ
    [SerializeField] private Text timesec; // ���� �ð� �ؽ�Ʈ
    [SerializeField] private Text sec180_361_611text; // �̵��ӵ� ����, ���ݷ� ����, ���� �߰� �ȳ� �ؽ�Ʈ

    void Start()
    {
        StartCoroutine(StartTextCoroutine()); // ���� �ؽ�Ʈ �ִϸ��̼� �� ��Ȱ��ȭ �ڷ�ƾ ����
    }

    void Update()
    {
        UpdateTimer(); // ���� �ð� ������Ʈ
        StartCoroutine(UpdateTimeText()); // �ð��� �ȳ� �ؽ�Ʈ ������Ʈ
    }

    private void UpdateTimer()
    {
        timesec.text = TotalGameManager.survivaltimesecond.ToString("N1") + " sec"; // ���� �ð� �ؽ�Ʈ ������Ʈ
    }

    IEnumerator UpdateTimeText()
    {
        if (TotalGameManager.survivaltimesecond >= 180 && TotalGameManager.survivaltimesecond <= 190)
        {
            sec180_361_611text.gameObject.SetActive(true); // �ؽ�Ʈ Ȱ��ȭ
            sec180_361_611text.text = "���� �̵��ӵ��� �����մϴ�.";
            yield return new WaitForSeconds(4f);
            sec180_361_611text.gameObject.SetActive(false); // �ؽ�Ʈ ��Ȱ��ȭ
        }
        else if (TotalGameManager.survivaltimesecond >= 361 && TotalGameManager.survivaltimesecond <= 370)
        {
            sec180_361_611text.gameObject.SetActive(true); // �ؽ�Ʈ Ȱ��ȭ
            sec180_361_611text.text = "���� �̵��ӵ��� ���ݷ��� �����մϴ�.";
            yield return new WaitForSeconds(4f);
            sec180_361_611text.gameObject.SetActive(false); // �ؽ�Ʈ ��Ȱ��ȭ
        }
        else if (TotalGameManager.survivaltimesecond >= 611 && TotalGameManager.survivaltimesecond <= 620)
        {
            sec180_361_611text.gameObject.SetActive(true); // �ؽ�Ʈ Ȱ��ȭ
            sec180_361_611text.text = "���� ���� ����! ��Ƴ����ʽÿ�.";
            yield return new WaitForSeconds(4f);
            sec180_361_611text.gameObject.SetActive(false); // �ؽ�Ʈ ��Ȱ��ȭ
        }
    }

    IEnumerator StartTextCoroutine()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("end", true); // �ִϸ��̼� ���
        starttext.gameObject.SetActive(false); // ���� �ؽ�Ʈ ��Ȱ��ȭ
    }
}
