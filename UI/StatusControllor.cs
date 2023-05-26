using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusControllor : MonoBehaviour
{
    [SerializeField] private int hp;  // 최대 체력. 유니티 에디터 슬롯에서 지정할 것.
    [SerializeField] private Image[] images_Gauge; // 체력, 스태미나, 배고픔, 목마름을 표시하는 이미지 게이지 배열
    [SerializeField] private int hungry;  // 최대 배고픔. 유니티 에디터 슬롯에서 지정할 것.
    [SerializeField] private int thirsty;  // 최대 목마름. 유니티 에디터 슬롯에서 지정할 것.
    [SerializeField] private int hungryDecreaseTime; // 배고픔 감소 시간 간격
    [SerializeField] private int thirstyDecreaseTime; // 목마름 감소 시간 간격
    [SerializeField] public int sp;  // 최대 스태미나. 유니티 에디터 슬롯에서 지정할 것.
    [SerializeField] private int spIncreaseSpeed; // 스태미나 회복 속도
    [SerializeField] private int spRechargeTime; // 스태미나 회복 간격
    [SerializeField] private GameObject DeadText; // 플레이어 사망 시 활성화되는 텍스트 오브젝트
    [SerializeField] private GameObject player; // 플레이어 오브젝트
    [SerializeField] private Text recordtime; // 생존 시간을 표시하는 텍스트
    [SerializeField] private Text recordkill; // 킬 수를 표시하는 텍스트
    [SerializeField] private GameObject hitimage; // 피격 시 표시되는 이미지
    [SerializeField] private AudioSource hitSound; // 피격 사운드 재생을 위한 오디오 소스
    [SerializeField] private AudioSource spSound; // 스태미나 부족 사운드 재생을 위한 오디오 소스
    [SerializeField] private GameObject hungrynormal; // 배고픔 정상 상태를 표시하는 게임 오브젝트
    [SerializeField] private GameObject thirstynormal; // 목마름 정상 상태를 표시하는 게임 오브젝트
    [SerializeField] private GameObject hungryhigh; // 배고픔 고위험 상태를 표시하는 게임 오브젝트
    [SerializeField] private GameObject thirstyhigh; // 목마름 고위험 상태를 표시하는 게임 오브젝트
    public Transform theplayer; // 플레이어의 위치를 저장하는 Transform 컴포넌트
    private RealtimeRankingSystem therankingsystem; // 실시간 랭킹 시스템을 관리하는 컴포넌트
    private const int HP = 0, SP = 1, HUNGRY = 2, THIRSTY = 3; // 이미지 게이지 배열에서 체력, 스태미나, 배고픔, 목마름에 해당하는 인덱스
    private bool statusactivated; // 상태 활성화 여부를 저장하는 변수
    [SerializeField] public int currentHungry; // 현재 배고픔 수치
    [SerializeField] public int currentThirsty; // 현재 목마름 수치
    public int currentHp; // 현재 체력
    private int currentThirstyDecreaseTime; // 현재 목마름 감소 시간
    private int currentHungryDecreaseTime; // 현재 배고픔 감소 시간
    private TotalGameManager thekillcount; // 킬 수를 관리하는 컴포넌트

    // 배고픔 상태에 따른 텍스트 표시
    private void HungryText()
    {
        if (currentHungry <= 5000 && currentHungry >= 2000)
        {
            hungryhigh.SetActive(false);
            hungrynormal.SetActive(true);
        }
        if(currentHungry<=2000)
        {
            hungrynormal.SetActive(false);
            hungryhigh.SetActive(true);
        }
        if(currentHungry>=5001)
        {
            hungrynormal.SetActive(false);
            hungryhigh.SetActive(false);
        }
    }
    // 목마름 상태에 따른 텍스트 표시
    private void ThirstyText()
    {
        if (currentThirsty <= 5000 && currentThirsty >= 2000)
        {
            thirstynormal.SetActive(true);
            thirstyhigh.SetActive(false);
        }
        if (currentThirsty <= 2000)
        {
            thirstynormal.SetActive(false);
            thirstyhigh.SetActive(true);
        }
        if(currentThirsty>=5001)
        {
            thirstynormal.SetActive(false);
            thirstyhigh.SetActive(false);
        }
    }
    // 체력 증가
    public void IncreaseHP(int _count)
    {
        if (currentHp + _count < hp)
            currentHp += _count;
        else
            currentHp = hp;
    }
    // 피격 시 효과 표시
    public IEnumerator ImageCO()
    {
        if (!hitSound.isPlaying)
        {
            hitSound.Play();
        }
        hitimage.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        hitimage.gameObject.SetActive(false);
    }
    // 체력 감소
    public void DecreaseHP(int _count)
    {
        if (currentHp - _count < 0)
            currentHp = 0;
        currentHp -= _count;

        if (currentHp <= 0)
            Debug.Log("캐릭터의 체력이 0이 되었습니다!!");
    }
    // 배고픔 감소
    private void Hungry()
    {
        if (currentHungry > 0)
        {
            if (currentHungryDecreaseTime <= hungryDecreaseTime)
                currentHungryDecreaseTime++;
            else
            {
                currentHungry--;
                currentHungryDecreaseTime = 0;
            }
        }
        else
            Debug.Log("배고픔 수치가 0 이 되었습니다.");
    }
    // 배고픔 증가

    public void IncreaseHungry(int _count)
    {
        if (currentHungry + _count < hungry)
            currentHungry += _count;
        else
            currentHungry = hungry;
    }

    // 배고픔 감소
    public void DecreaseHungry(int _count)
    {
        if (currentHungry - _count < 0)
            currentHungry = 0;
        else
            currentHungry -= _count;
    }

    // 목마름 감소
    private void Thirsty()
    {
        if (currentThirsty > 0)
        {
            if (currentThirstyDecreaseTime <= thirstyDecreaseTime)
                currentThirstyDecreaseTime++;
            else
            {
                currentThirsty--;
                currentThirstyDecreaseTime = 0;
            }
        }
        else
            Debug.Log("목마름 수치가 0 이 되었습니다.");
    }
    // 목마름 증가
    public void IncreaseThirsty(int _count)
    {
        if (currentThirsty + _count < thirsty)
            currentThirsty += _count;
        else
            currentThirsty = thirsty;
    }
    // 목마름 감소
    public void DecreaseThirsty(int _count)
    {
        if (currentThirsty - _count < 0)
            currentThirsty = 0;
        else
            currentThirsty -= _count;
    }
    

    [SerializeField]private int currentSp;
    private int currentSpRechargeTime;

    // 스태미나 감소 여부
    private bool spUsed;

    // 스태미나 감소
    public void DecreaseStamina(int _count)
    {
        spUsed = true;
        currentSpRechargeTime = 0;

        if (currentSp - _count > 0)
        {
            currentSp -= _count;
        }
        else
            currentSp = 0;
    }
    // 스태미나 재충전 시간
    private void SPRechargeTime()
    {
        if (spUsed)
        {
            if (currentSpRechargeTime < spRechargeTime)
                currentSpRechargeTime++;
            else
                spUsed = false;
        }
    }
    // 스태미나 회복
    private void SPRecover()
    {
        if (!spUsed && currentSp < sp)
        {
            currentSp += spIncreaseSpeed;
        }
    }
    // 스태미나 소리 효과 재생 여부
    private void currentspsound()
    {
        if (currentSp <= 500)
        {
            if (!spSound.isPlaying)
                spSound.Play();
        }
        if(currentSp>=600)
        {
            spSound.Stop();
        }
    }
    // 현재 스태미나 값 반환
    public int GetCurrentSP()
    {
        return currentSp;
    }
    // 초기화 및 시작 시 호출되는 함수
    void Start()
    {
        therankingsystem = FindObjectOfType<RealtimeRankingSystem>();
        hitimage.gameObject.SetActive(false);
        thekillcount = FindObjectOfType<TotalGameManager>();
        currentHp = hp;
        currentSp = sp;
        currentHungry = hungry;
        currentThirsty = thirsty;
    }

    void Update()
    {
        if (!PauseScript.isPaused)
        {
            HungryText();
            ThirstyText();
            currentspsound();
            HungryThiTimeDamage();
            Hungry();
            Thirsty();
            SPRechargeTime();
            SPRecover();
            GaugeUpdate();
            IsPlayerDead();
            IsyPlayerDead();
        }
    }
    [SerializeField] private float damageTime; 
    [SerializeField] private float currentDamageTime;

    private void HungryThiTimeDamage()
    {
        if (currentHungry <= 0 || currentThirsty <= 0)
        {
            if (currentDamageTime > 0)
                currentDamageTime -= Time.deltaTime;
            if (currentDamageTime <= 0)
            {
                DecreaseHP(5);
                currentDamageTime = damageTime;
            }
        }
    }
    // 플레이어 사망 시 호출되는 함수
    public void IsPlayerDead()
    {
        if (currentHp <= 0)
        {
            TotalGameManager.isPlayerDead = true;
            DeadText.gameObject.SetActive(true);
            player.gameObject.SetActive(false);
            therankingsystem.UpdateKillCount(thekillcount.killcount);
            therankingsystem.UpdateSurvivalTime(TotalGameManager.survivaltimesecond);
            recordtime.text = "Survival Time: " + TotalGameManager.survivaltimehour.ToString() + "H " + TotalGameManager.survivaltimeminute.ToString() +
                "M " + TotalGameManager.survivaltimesecond.ToString("N1") + "S";
            recordkill.text = "Kill Count: " + thekillcount.killcount.ToString();
        }
    }
    //특정좌표 이하로 갈시 호출되는 함수
    public void IsyPlayerDead()
    {
        if (theplayer.position.y<-50)
        {
            TotalGameManager.isPlayerDead = true;
            DeadText.gameObject.SetActive(true);
            player.gameObject.SetActive(false);
            therankingsystem.UpdateKillCount(thekillcount.killcount);
            therankingsystem.UpdateSurvivalTime(TotalGameManager.survivaltimesecond);
            recordtime.text = "Survival Time: " + TotalGameManager.survivaltimehour.ToString() + "H " + TotalGameManager.survivaltimeminute.ToString() +
                "M " + TotalGameManager.survivaltimesecond.ToString("N1") + "S";
            recordkill.text = "Kill Count: " + thekillcount.killcount.ToString();
        }
    }
    // 현재 게이지 값 업데이트

    private void GaugeUpdate()
    {
        images_Gauge[HP].fillAmount = (float)currentHp / hp;
        images_Gauge[SP].fillAmount = (float)currentSp / sp;
        images_Gauge[HUNGRY].fillAmount = (float)currentHungry / hungry;
        images_Gauge[THIRSTY].fillAmount = (float)currentThirsty / thirsty;
    }
}
