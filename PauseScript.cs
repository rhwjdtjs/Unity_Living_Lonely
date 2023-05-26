using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseScript : MonoBehaviour //�ΰ��ӿ��� �Ͻ������ϴ� ��ũ��Ʈ
{
  
    public GameObject pauseMenuUI;
    public static bool isPaused = false;
    private void Awake()
    {
        
    }

    private void Start()
    {
        // �Ͻ����� �޴� ��Ȱ��ȭ
        pauseMenuUI.SetActive(false);
    }

    private void Update()
    {
        if (!Inventory.invectoryActivated)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
               
                if (isPaused)
                {
                    
                    Resume();
                }
                else
                {
                    
                    Pause();
                }
            }
            Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isPaused;
        }
    }
    public void mainmenu_button()
    {
        Resume();
        
        Debug.Log("����ȭ������ ���ϴ�");
        
        SceneManager.LoadScene("mainmenu");
       
    }
    public void press_tutorial()
    {
        Debug.Log("Ʃ�丮���� ��ϴ�");
    }
    public void Resume()
    {
        
        // �Ͻ����� ����
        Time.timeScale = 1f;
        PlayerControllor.ispause = false;
        isPaused = false;

        // �Ͻ����� �޴� ��Ȱ��ȭ
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    void Pause()
    {
       
        // �Ͻ�����
        Time.timeScale = 0f;
        isPaused = true;
        PlayerControllor.ispause = true;
        // �Ͻ����� �޴� Ȱ��ȭ
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
