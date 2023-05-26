using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayContollor : MonoBehaviour
{
    [SerializeField] private float secondPerRealTimeSecond; // 게임 세계에서의 100초 = 현실 세계의 1초

    public bool isNight = false;

    //test code
    [SerializeField] GameObject testzombiespqwner;
    private StatusControllor thestatus;

    private void testfunction()
    {
        // F1 키를 누르면 testzombiespqwner 게임 오브젝트를 활성화한다.
        if (Input.GetKeyDown(KeyCode.F1))
        {
            testzombiespqwner.gameObject.SetActive(true);
        }

        // F2 키를 누르면 X 축을 기준으로 180도 회전한다.
        if (Input.GetKeyDown(KeyCode.F2))
        {
            transform.Rotate(180, 0, 0);
        }

        // F3 키를 누르면 X 축을 기준으로 40도 회전한다.
        if (Input.GetKeyDown(KeyCode.F3))
        {
            transform.Rotate(40, 0, 0);
        }

        // F4 키를 누르면 thestatus의 현재 체력, 배고픔, 목마름을 특정 값으로 설정한다.
        if (Input.GetKeyDown(KeyCode.F4))
        {
            thestatus.currentHp = 50;
            thestatus.currentHungry = 2000;
            thestatus.currentThirsty = 2000;
        }
    }

    void Start()
    {
        thestatus = FindObjectOfType<StatusControllor>();
    }

    void Update()
    {
        // 태양을 X 축 중심으로 일정한 속도로 회전시킨다.
        // 현실 시간 1초에 0.1f * secondPerRealTimeSecond 각도만큼 회전한다.
        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecond * Time.deltaTime);

        // X 축 회전값이 175 이상이면 밤이라고 판단한다.
        if (transform.eulerAngles.x >= 175)
            isNight = true;
        // X 축 회전값이 175 이하이고 10 이상이면 낮이라고 판단한다.
        else if (transform.eulerAngles.x <= 175 && transform.eulerAngles.x >= 10)
            isNight = false;

        // testfunction() 메서드를 호출한다.
        testfunction();
    }
}
