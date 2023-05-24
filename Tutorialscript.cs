using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tutorialscript : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public GameObject panel4;
    public GameObject panel5;
    
    public void press_tutorial_button()
    {
        panel1.SetActive(true);
    }
    public void press_left_tutorial_button()
    {
        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(false);
        panel4.SetActive(false);
        panel5.SetActive(false);
    }
    public void press_next_panel1()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
    }
    public void press_next_panel2()
    {
        panel2.SetActive(false);
        panel3.SetActive(true);
    }
    public void press_next_panel3()
    {
        panel3.SetActive(false);
        panel4.SetActive(true);
    }
    public void press_next_panel4()
    {
        panel4.SetActive(false);
        panel5.SetActive(true);
    }
    public void press_prev_panel2()
    {
        panel2.SetActive(false);
        panel1.SetActive(true);
    }
    public void press_prev_panel3()
    {
        panel3.SetActive(false);
        panel2.SetActive(true);
    }
    public void press_prev_panel4()
    {
        panel4.SetActive(false);
        panel3.SetActive(true);
    }
    public void press_prev_panel5()
    {
        panel5.SetActive(false);
        panel4.SetActive(true);
    }
}
