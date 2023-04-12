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
        if(Input.GetKeyDown(KeyCode.F1))
        {
            testzombiespqwner.gameObject.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.F2))
        {
            transform.Rotate(180, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            transform.Rotate(40, 0, 0);
        }
        if(Input.GetKeyDown(KeyCode.F4))
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
        // 계속 태양을 X 축 중심으로 회전. 현실시간 1초에  0.1f * secondPerRealTimeSecond 각도만큼 회전
        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecond * Time.deltaTime);
        if (transform.eulerAngles.x >= 175) // x 축 회전값 170 이상이면 밤이라고 하겠음
            isNight = true;
        else if (transform.eulerAngles.x <= 175 && transform.eulerAngles.x >= 10)  // x 축 회전값 10 이하면 낮이라고 하겠음
            isNight = false;
        testfunction();


    }
}
