using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUIScript : MonoBehaviour
{
    [SerializeField] private Text starttext; // 시작 텍스트
    [SerializeField] private Animator anim; // 애니메이터 컴포넌트
    [SerializeField] private Text timesec; // 생존 시간 텍스트
    [SerializeField] private Text sec180_361_611text; // 이동속도 증가, 공격력 증가, 몬스터 추가 안내 텍스트

    void Start()
    {
        StartCoroutine(StartTextCoroutine()); // 시작 텍스트 애니메이션 및 비활성화 코루틴 실행
    }

    void Update()
    {
        UpdateTimer(); // 생존 시간 업데이트
        StartCoroutine(UpdateTimeText()); // 시간별 안내 텍스트 업데이트
    }

    private void UpdateTimer()
    {
        timesec.text = TotalGameManager.survivaltimesecond.ToString("N1") + " sec"; // 생존 시간 텍스트 업데이트
    }

    IEnumerator UpdateTimeText()
    {
        if (TotalGameManager.survivaltimesecond >= 180 && TotalGameManager.survivaltimesecond <= 190)
        {
            sec180_361_611text.gameObject.SetActive(true); // 텍스트 활성화
            sec180_361_611text.text = "적의 이동속도가 증가합니다.";
            yield return new WaitForSeconds(4f);
            sec180_361_611text.gameObject.SetActive(false); // 텍스트 비활성화
        }
        else if (TotalGameManager.survivaltimesecond >= 361 && TotalGameManager.survivaltimesecond <= 370)
        {
            sec180_361_611text.gameObject.SetActive(true); // 텍스트 활성화
            sec180_361_611text.text = "적의 이동속도와 공격력이 증가합니다.";
            yield return new WaitForSeconds(4f);
            sec180_361_611text.gameObject.SetActive(false); // 텍스트 비활성화
        }
        else if (TotalGameManager.survivaltimesecond >= 611 && TotalGameManager.survivaltimesecond <= 620)
        {
            sec180_361_611text.gameObject.SetActive(true); // 텍스트 활성화
            sec180_361_611text.text = "강한 몬스터 출현! 살아남으십시오.";
            yield return new WaitForSeconds(4f);
            sec180_361_611text.gameObject.SetActive(false); // 텍스트 비활성화
        }
    }

    IEnumerator StartTextCoroutine()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("end", true); // 애니메이션 재생
        starttext.gameObject.SetActive(false); // 시작 텍스트 비활성화
    }
}
