using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusControllor : MonoBehaviour
{
    [SerializeField] private int hp;  // 최대 체력. 유니티 에디터 슬롯에서 지정할 것.
    [SerializeField] private Image[] images_Gauge;
    [SerializeField] private int hungry;  // 최대 배고픔. 유니티 에디터 슬롯에서 지정할 것.
    [SerializeField] private int thirsty;  // 최대 목마름. 유니티 에디터 슬롯에서 지정할 것.
    [SerializeField] private int hungryDecreaseTime;
    [SerializeField] private int thirstyDecreaseTime;
    [SerializeField] public int sp;  // 최대 스태미나. 유니티 에디터 슬롯에서 지정할 것.
    [SerializeField] private int spIncreaseSpeed;
    [SerializeField] private int spRechargeTime;
    [SerializeField] private GameObject DeadText;
    [SerializeField] private GameObject player;
    [SerializeField] private Text recordtime;
    [SerializeField] private Text recordkill;
    [SerializeField] private GameObject hitimage;
    [SerializeField] private AudioSource hitSound;
    [SerializeField] private AudioSource spSound;
    [SerializeField] private GameObject hungrynormal;
    [SerializeField] private GameObject thirstynormal;
    [SerializeField] private GameObject hungryhigh;
    [SerializeField] private GameObject thirstyhigh;
    private RealtimeRankingSystem therankingsystem;
    private const int HP = 0, SP = 1, HUNGRY = 2, THIRSTY = 3;
    private bool statusactivated;
    [SerializeField]
    public int currentHungry;
    [SerializeField]
    public int currentThirsty;
    public int currentHp;
    private int currentThirstyDecreaseTime;
    private int currentHungryDecreaseTime;
    private TotalGameManager thekillcount;
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
    public void IncreaseHP(int _count)
    {
        if (currentHp + _count < hp)
            currentHp += _count;
        else
            currentHp = hp;
    }
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
    public void DecreaseHP(int _count)
    {
        if (currentHp - _count < 0)
            currentHp = 0;
        currentHp -= _count;

        if (currentHp <= 0)
            Debug.Log("캐릭터의 체력이 0이 되었습니다!!");
    }
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

    public void IncreaseHungry(int _count)
    {
        if (currentHungry + _count < hungry)
            currentHungry += _count;
        else
            currentHungry = hungry;
    }

    public void DecreaseHungry(int _count)
    {
        if (currentHungry - _count < 0)
            currentHungry = 0;
        else
            currentHungry -= _count;
    }


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

    public void IncreaseThirsty(int _count)
    {
        if (currentThirsty + _count < thirsty)
            currentThirsty += _count;
        else
            currentThirsty = thirsty;
    }

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

    private void SPRecover()
    {
        if (!spUsed && currentSp < sp)
        {
            currentSp += spIncreaseSpeed;
        }
    }
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
    public int GetCurrentSP()
    {
        return currentSp;
    }

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
    public void IsPlayerDead()
    {
        if (currentHp <= 0)
        {
            TotalGameManager.isPlayerDead = true;
            DeadText.gameObject.SetActive(true);
            player.gameObject.SetActive(false);
            therankingsystem.UpdateStatistics(thekillcount.killcount, TotalGameManager.survivaltimesecond);
            recordtime.text = "Survival Time: " + TotalGameManager.survivaltimehour.ToString() + "H " + TotalGameManager.survivaltimeminute.ToString() +
                "M " + TotalGameManager.survivaltimesecond.ToString("N1") + "S";
            recordkill.text = "Kill Count: " + thekillcount.killcount.ToString();
        }
    }
   

    private void GaugeUpdate()
    {
        images_Gauge[HP].fillAmount = (float)currentHp / hp;
        images_Gauge[SP].fillAmount = (float)currentSp / sp;
        images_Gauge[HUNGRY].fillAmount = (float)currentHungry / hungry;
        images_Gauge[THIRSTY].fillAmount = (float)currentThirsty / thirsty;
    }
}
