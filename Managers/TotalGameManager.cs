using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalGameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerobject; // 플레이어 오브젝트를 연결하기 위한 변수
    [SerializeField] private GameObject playeruiobject; // 플레이어 UI 오브젝트를 연결하기 위한 변수
    [SerializeField] private StatusControllor thestatus; // 플레이어 상태를 제어하기 위한 StatusControllor 스크립트를 연결하기 위한 변수
    public int killcount = 0; // 플레이어의 킬 수를 기록하는 변수
    public static float survivaltimes; // 플레이어의 생존 시간을 기록하는 변수
    [SerializeField] public static float survivaltimesecond = 0; // 플레이어의 생존 시간(초)을 기록하는 변수 (정적 변수로 다른 스크립트에서 접근 가능)
    public static int survivaltimeminute = 0; // 플레이어의 생존 시간(분)을 기록하는 변수
    public static int survivaltimehour = 0; // 플레이어의 생존 시간(시간)을 기록하는 변수
    public static bool isPlayerDead = false; // 플레이어가 사망했는지 여부를 나타내는 변수
    private PlayerSpawnManager theplayerspawn; // 플레이어 스폰 매니저를 참조하기 위한 변수
    [SerializeField] private RealtimeRankingSystem rankingSystem; // 실시간 랭킹 시스템을 참조하기 위한 변수
    [SerializeField] private float survivalttime; // 플레이어의 생존 시간을 조절하기 위한 변수
    public float Survivalttime
    {
        get { return survivalttime; }
        set
        {
            survivalttime = value;
            survivaltimesecond = survivalttime;
        }
    }

    private void Awake()
    {
        rankingSystem = FindObjectOfType<RealtimeRankingSystem>(); // RealtimeRankingSystem 스크립트를 찾아 참조
    }

    void Start()
    {
        theplayerspawn = FindObjectOfType<PlayerSpawnManager>(); // PlayerSpawnManager 스크립트를 찾아 참조
        thestatus = FindObjectOfType<StatusControllor>(); // StatusControllor 스크립트를 찾아 참조
        theplayerspawn.RandomSelectSpawnPoint(); // 랜덤한 스폰 지점 선택
    }

    private void timerecord()
    {
        survivaltimesecond += Time.deltaTime; // 생존 시간 기록 (DeltaTime을 이용하여 초 단위로 증가)
    }

    void Update()
    {
        if (!isPlayerDead)
            timerecord(); // 플레이어가 사망하지 않았을 때 생존 시간 기록

        playerresqawn(); // 플레이어 부활 처리

        if (Input.GetKeyDown(KeyCode.F12))
            Application.Quit(); // F12 키를 누르면 게임 종료

        if (Input.GetKeyDown(KeyCode.F11))
        {
            rankingSystem.UpdateKillCount(killcount); // 랭킹 시스템에 킬 수 업데이트
            rankingSystem.UpdateSurvivalTime(survivaltimesecond); // 랭킹 시스템에 생존 시간 업데이트
            Debug.Log("기록완료");
        }
    }

    private void playerresqawn()
    {
        if (isPlayerDead) // 플레이어가 사망한 경우
        {
            if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바를 누르면 부활 처리
            {
                playerobject.gameObject.SetActive(true); // 플레이어 오브젝트 활성화
                playeruiobject.gameObject.SetActive(false); // 플레이어 UI 오브젝트 비활성화
                theplayerspawn.RandomSelectSpawnPoint(); // 랜덤한 스폰 지점 선택
                thestatus.currentHp = 100; // 플레이어 체력 초기화
                thestatus.currentHungry = 10000; // 플레이어 배고픔 초기화
                thestatus.currentThirsty = 10000; // 플레이어 목마름 초기화
                thestatus.sp = 4999; // 플레이어 SP 초기화
                isPlayerDead = false; // 사망 상태 해제
                LoadingSceneManager.LoadScene("gamescene"); // 게임 씬 다시 로드

                if (!isPlayerDead)
                {
                    survivaltimesecond = 0; // 생존 시간 초기화
                    survivaltimeminute = 0; // 생존 시간 초기화
                    survivaltimehour = 0; // 생존 시간 초기화
                    killcount = 0; // 킬 수 초기화
                    return;
                }
            }
        }
    }
}
