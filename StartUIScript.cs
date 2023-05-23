using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUIScript : MonoBehaviour
{
    [SerializeField] private Text starttext;
    [SerializeField] private Animator anim;
    [SerializeField] private Text timesec;
    [SerializeField] private Text sec180_361_611text; //이동속도 증가,공격력 증가, 몬스터 추가
   

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
            sec180_361_611text.text = "적의 이동속도가 증가합니다.";
            yield return new WaitForSeconds(4f);
            sec180_361_611text.gameObject.SetActive(false);
        }
        else if (TotalGameManager.survivaltimesecond >= 361 && TotalGameManager.survivaltimesecond <= 370)
        {
            sec180_361_611text.gameObject.SetActive(true);
            sec180_361_611text.text = "적의 이동속도와 공격력이 증가합니다.";
            yield return new WaitForSeconds(4f);
            sec180_361_611text.gameObject.SetActive(false);
        }
        else if (TotalGameManager.survivaltimesecond >= 611 && TotalGameManager.survivaltimesecond <= 620)
        {
            sec180_361_611text.gameObject.SetActive(true);
            sec180_361_611text.text = "강한 몬스터 출현! 살아남으십시오.";
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
