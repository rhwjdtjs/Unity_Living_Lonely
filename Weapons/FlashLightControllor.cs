using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightControllor : MonoBehaviour
{
    Light Flash_Light;
    private bool isturnOn = false;
    void Start()
    {
        Flash_Light = GetComponent<Light>();
    }
    private void TryTrunOnOFF()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            isturnOn = !isturnOn;
            if (isturnOn)
                TurnOn();
            else
                TurnOff();
        }
      
    }
    private void TurnOn()
    {
        Flash_Light.enabled = true;
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
