using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightControllor : MonoBehaviour
{
    Light Flash_Light;
    private bool isturnOn = false;
    void Start()
    {
        Flash_Light = GetComponent<Light>(); //Light ������Ʈ�� �����´�
    }
    private void TryTrunOnOFF()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) //���콺 ��Ŭ����
        {
            isturnOn = !isturnOn; //isturnon�� true �̸� false �� false �̸� true��
            if (isturnOn)
                TurnOn(); //true �϶�
            else
                TurnOff(); //false �϶�
        }
      
    }
    private void TurnOn()
    {
        Flash_Light.enabled = true; //true�϶� ����Ʈ on
    }
    private void TurnOff()
    {
        Flash_Light.enabled = false;
    }
    void Update()
    {
        TryTrunOnOFF();
    }
}
