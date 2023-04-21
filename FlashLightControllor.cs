using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightControllor : MonoBehaviour
{
    Light Flash_Light;
    private bool isturnOn = false;
    void Start()
    {
        Flash_Light = GetComponent<Light>(); //Light 컴포넌트를 가져온다
    }
    private void TryTrunOnOFF()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) //마우스 우클릭시
        {
            isturnOn = !isturnOn; //isturnon이 true 이면 false 로 false 이면 true로
            if (isturnOn)
                TurnOn(); //true 일때
            else
                TurnOff(); //false 일때
        }
      
    }
    private void TurnOn()
    {
        Flash_Light.enabled = true; //true일때 라이트 on
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
