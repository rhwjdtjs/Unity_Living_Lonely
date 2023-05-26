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
        // F1 Ű�� ������ testzombiespqwner ���� ������Ʈ�� Ȱ��ȭ�Ѵ�.
        if (Input.GetKeyDown(KeyCode.F1))
        {
            testzombiespqwner.gameObject.SetActive(true);
        }

        // F2 Ű�� ������ X ���� �������� 180�� ȸ���Ѵ�.
        if (Input.GetKeyDown(KeyCode.F2))
        {
            transform.Rotate(180, 0, 0);
        }

        // F3 Ű�� ������ X ���� �������� 40�� ȸ���Ѵ�.
        if (Input.GetKeyDown(KeyCode.F3))
        {
            transform.Rotate(40, 0, 0);
        }

        // F4 Ű�� ������ thestatus�� ���� ü��, �����, �񸶸��� Ư�� ������ �����Ѵ�.
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
        // �¾��� X �� �߽����� ������ �ӵ��� ȸ����Ų��.
        // ���� �ð� 1�ʿ� 0.1f * secondPerRealTimeSecond ������ŭ ȸ���Ѵ�.
        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecond * Time.deltaTime);

        // X �� ȸ������ 175 �̻��̸� ���̶�� �Ǵ��Ѵ�.
        if (transform.eulerAngles.x >= 175)
            isNight = true;
        // X �� ȸ������ 175 �����̰� 10 �̻��̸� ���̶�� �Ǵ��Ѵ�.
        else if (transform.eulerAngles.x <= 175 && transform.eulerAngles.x >= 10)
            isNight = false;

        // testfunction() �޼��带 ȣ���Ѵ�.
        testfunction();
    }
}
