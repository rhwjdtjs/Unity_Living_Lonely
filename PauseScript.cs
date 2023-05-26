using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseScript : MonoBehaviour //인게임에서 일시정지하는 스크립트
{
  
    public GameObject pauseMenuUI;
    public static bool isPaused = false;
    private void Awake()
    {
        
    }

    private void Start()
    {
        // 일시정지 메뉴 비활성화
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
        
        Debug.Log("메인화면으로 갑니다");
        
        SceneManager.LoadScene("mainmenu");
       
    }
    public void press_tutorial()
    {
        Debug.Log("튜토리얼이 뜹니다");
    }
    public void Resume()
    {
        
        // 일시정지 해제
        Time.timeScale = 1f;
        PlayerControllor.ispause = false;
        isPaused = false;

        // 일시정지 메뉴 비활성화
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    void Pause()
    {
       
        // 일시정지
        Time.timeScale = 0f;
        isPaused = true;
        PlayerControllor.ispause = true;
        // 일시정지 메뉴 활성화
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
