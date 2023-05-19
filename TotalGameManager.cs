using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalGameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerobject;
    [SerializeField] private GameObject playeruiobject;
    [SerializeField] private StatusControllor thestatus;
    public int killcount = 0;
    public static float survivaltimes;
    [SerializeField] public static float survivaltimesecond = 0;
    public static int survivaltimeminute = 0;
    public static int survivaltimehour = 0;
    public static bool isPlayerDead = false;
    private PlayerSpawnManager theplayerspawn;
    [SerializeField] private RealtimeRankingSystem rankingSystem;
    // Start is called before the first frame update
    private void Awake()
    {
        rankingSystem = FindObjectOfType<RealtimeRankingSystem>();
    }
    void Start()
    {
        
        theplayerspawn = FindObjectOfType<PlayerSpawnManager>();
        thestatus = FindObjectOfType<StatusControllor>();
       theplayerspawn.RandomSelectSpawnPoint();
    }
    private void timerecord()
    {
        survivaltimesecond += Time.deltaTime;
        //if (survivaltimesecond > 59)
       // {
       //     survivaltimesecond = 0;
       //     survivaltimeminute++;
//
       //     if (survivaltimeminute > 59)
      //      {
      //          survivaltimeminute = 0;
      //          survivaltimehour++;
     //       }
     //   }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isPlayerDead)
            timerecord();
        playerresqawn();
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        
        if (Input.GetKeyDown(KeyCode.F11))
        {
            rankingSystem.UpdateStatistics(killcount, survivaltimesecond);
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            rankingSystem.GetLeaderboard();
        }
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
