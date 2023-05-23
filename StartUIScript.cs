using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUIScript : MonoBehaviour
{
    [SerializeField] private Text starttext;
    [SerializeField] private Animator anim;
    [SerializeField] private Text timesec;
    [SerializeField] private Text sec180_361_611text; //�̵��ӵ� ����,���ݷ� ����, ���� �߰�
   

    void Start()
    {
        StartCoroutine(starttextco());
    }

    void Update()
    {
        Timer();
        StartCoroutine(timetext());
    }
    private void Timer()
    {
        timesec.text = TotalGameManager.survivaltimesecond.ToString() + " sec";
    }
    IEnumerator timetext()
    {
        if(TotalGameManager.survivaltimesecond>=180 && TotalGameManager.survivaltimesecond<=190)
        {
            sec180_361_611text.gameObject.SetActive(true);
            sec180_361_611text.text = "���� �̵��ӵ��� �����մϴ�.";
            yield return new WaitForSeconds(4f);
            sec180_361_611text.gameObject.SetActive(false);
        }
        else if (TotalGameManager.survivaltimesecond >= 361 && TotalGameManager.survivaltimesecond <= 370)
        {
            sec180_361_611text.gameObject.SetActive(true);
            sec180_361_611text.text = "���� �̵��ӵ��� ���ݷ��� �����մϴ�.";
            yield return new WaitForSeconds(4f);
            sec180_361_611text.gameObject.SetActive(false);
        }
        else if (TotalGameManager.survivaltimesecond >= 611 && TotalGameManager.survivaltimesecond <= 620)
        {
            sec180_361_611text.gameObject.SetActive(true);
            sec180_361_611text.text = "���� ���� ����! ��Ƴ����ʽÿ�.";
            yield return new WaitForSeconds(4f);
            sec180_361_611text.gameObject.SetActive(false);
        }
    }
    IEnumerator starttextco()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("end", true);
        starttext.gameObject.SetActive(false);
    }
}
