using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayContollor : MonoBehaviour
{
    [SerializeField] private float secondPerRealTimeSecond; // ���� ���迡���� 100�� = ���� ������ 1��

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
        // ��� �¾��� X �� �߽����� ȸ��. ���ǽð� 1�ʿ�  0.1f * secondPerRealTimeSecond ������ŭ ȸ��
        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecond * Time.deltaTime);
        if (transform.eulerAngles.x >= 175) // x �� ȸ���� 170 �̻��̸� ���̶�� �ϰ���
            isNight = true;
        else if (transform.eulerAngles.x <= 175 && transform.eulerAngles.x >= 10)  // x �� ȸ���� 10 ���ϸ� ���̶�� �ϰ���
            isNight = false;
        testfunction();


    }
}
