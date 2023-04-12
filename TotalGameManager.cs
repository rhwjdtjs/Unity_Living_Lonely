using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalGameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerobject;
    [SerializeField] private GameObject playeruiobject;
    [SerializeField] private StatusControllor thestatus;
    public int killcount = 0;
    public static float survivaltimesecond = 0;
    public static int survivaltimeminute = 0;
    public static int survivaltimehour = 0;
    public static bool isPlayerDead = false;
    private PlayerSpawnManager theplayerspawn;
    // Start is called before the first frame update
    void Start()
    {
        theplayerspawn = FindObjectOfType<PlayerSpawnManager>();
        thestatus = FindObjectOfType<StatusControllor>();
       theplayerspawn.RandomSelectSpawnPoint();
    }
    private void timerecord()
    {
        survivaltimesecond += Time.deltaTime;
        if (survivaltimesecond > 59)
        {
            survivaltimesecond = 0;
            survivaltimeminute++;

            if (survivaltimeminute > 59)
            {
                survivaltimeminute = 0;
                survivaltimehour++;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isPlayerDead)
            timerecord();
        playerresqawn();
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
    private void playerresqawn()
    {
        if(isPlayerDead)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                playerobject.gameObject.SetActive(true);
                playeruiobject.gameObject.SetActive(false);
                theplayerspawn.RandomSelectSpawnPoint();
                thestatus.currentHp = 100;
                thestatus.currentHungry = 10000;
                thestatus.currentThirsty = 10000;
                thestatus.sp = 4999;
                isPlayerDead = false;
                LoadingSceneManager.LoadScene("gamescene");
                if (!isPlayerDead)
                {
                    survivaltimesecond = 0;
                    survivaltimeminute = 0;
                    survivaltimehour = 0;
                    killcount = 0;
                    return;
                }
               
            }
        }
    }
}
