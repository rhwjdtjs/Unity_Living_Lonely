using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusControllor : MonoBehaviour
{
    [SerializeField] private int hp;  // �ִ� ü��. ����Ƽ ������ ���Կ��� ������ ��.
    [SerializeField] private Image[] images_Gauge; // ü��, ���¹̳�, �����, �񸶸��� ǥ���ϴ� �̹��� ������ �迭
    [SerializeField] private int hungry;  // �ִ� �����. ����Ƽ ������ ���Կ��� ������ ��.
    [SerializeField] private int thirsty;  // �ִ� �񸶸�. ����Ƽ ������ ���Կ��� ������ ��.
    [SerializeField] private int hungryDecreaseTime; // ����� ���� �ð� ����
    [SerializeField] private int thirstyDecreaseTime; // �񸶸� ���� �ð� ����
    [SerializeField] public int sp;  // �ִ� ���¹̳�. ����Ƽ ������ ���Կ��� ������ ��.
    [SerializeField] private int spIncreaseSpeed; // ���¹̳� ȸ�� �ӵ�
    [SerializeField] private int spRechargeTime; // ���¹̳� ȸ�� ����
    [SerializeField] private GameObject DeadText; // �÷��̾� ��� �� Ȱ��ȭ�Ǵ� �ؽ�Ʈ ������Ʈ
    [SerializeField] private GameObject player; // �÷��̾� ������Ʈ
    [SerializeField] private Text recordtime; // ���� �ð��� ǥ���ϴ� �ؽ�Ʈ
    [SerializeField] private Text recordkill; // ų ���� ǥ���ϴ� �ؽ�Ʈ
    [SerializeField] private GameObject hitimage; // �ǰ� �� ǥ�õǴ� �̹���
    [SerializeField] private AudioSource hitSound; // �ǰ� ���� ����� ���� ����� �ҽ�
    [SerializeField] private AudioSource spSound; // ���¹̳� ���� ���� ����� ���� ����� �ҽ�
    [SerializeField] private GameObject hungrynormal; // ����� ���� ���¸� ǥ���ϴ� ���� ������Ʈ
    [SerializeField] private GameObject thirstynormal; // �񸶸� ���� ���¸� ǥ���ϴ� ���� ������Ʈ
    [SerializeField] private GameObject hungryhigh; // ����� ������ ���¸� ǥ���ϴ� ���� ������Ʈ
    [SerializeField] private GameObject thirstyhigh; // �񸶸� ������ ���¸� ǥ���ϴ� ���� ������Ʈ
    public Transform theplayer; // �÷��̾��� ��ġ�� �����ϴ� Transform ������Ʈ
    private RealtimeRankingSystem therankingsystem; // �ǽð� ��ŷ �ý����� �����ϴ� ������Ʈ
    private const int HP = 0, SP = 1, HUNGRY = 2, THIRSTY = 3; // �̹��� ������ �迭���� ü��, ���¹̳�, �����, �񸶸��� �ش��ϴ� �ε���
    private bool statusactivated; // ���� Ȱ��ȭ ���θ� �����ϴ� ����
    [SerializeField] public int currentHungry; // ���� ����� ��ġ
    [SerializeField] public int currentThirsty; // ���� �񸶸� ��ġ
    public int currentHp; // ���� ü��
    private int currentThirstyDecreaseTime; // ���� �񸶸� ���� �ð�
    private int currentHungryDecreaseTime; // ���� ����� ���� �ð�
    private TotalGameManager thekillcount; // ų ���� �����ϴ� ������Ʈ

    // ����� ���¿� ���� �ؽ�Ʈ ǥ��
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
    // �񸶸� ���¿� ���� �ؽ�Ʈ ǥ��
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
    // ü�� ����
    public void IncreaseHP(int _count)
    {
        if (currentHp + _count < hp)
            currentHp += _count;
        else
            currentHp = hp;
    }
    // �ǰ� �� ȿ�� ǥ��
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
    // ü�� ����
    public void DecreaseHP(int _count)
    {
        if (currentHp - _count < 0)
            currentHp = 0;
        currentHp -= _count;

        if (currentHp <= 0)
            Debug.Log("ĳ������ ü���� 0�� �Ǿ����ϴ�!!");
    }
    // ����� ����
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
            Debug.Log("����� ��ġ�� 0 �� �Ǿ����ϴ�.");
    }
    // ����� ����

    public void IncreaseHungry(int _count)
    {
        if (currentHungry + _count < hungry)
            currentHungry += _count;
        else
            currentHungry = hungry;
    }

    // ����� ����
    public void DecreaseHungry(int _count)
    {
        if (currentHungry - _count < 0)
            currentHungry = 0;
        else
            currentHungry -= _count;
    }

    // �񸶸� ����
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
            Debug.Log("�񸶸� ��ġ�� 0 �� �Ǿ����ϴ�.");
    }
    // �񸶸� ����
    public void IncreaseThirsty(int _count)
    {
        if (currentThirsty + _count < thirsty)
            currentThirsty += _count;
        else
            currentThirsty = thirsty;
    }
    // �񸶸� ����
    public void DecreaseThirsty(int _count)
    {
        if (currentThirsty - _count < 0)
            currentThirsty = 0;
        else
            currentThirsty -= _count;
    }
    

    [SerializeField]private int currentSp;
    private int currentSpRechargeTime;

    // ���¹̳� ���� ����
    private bool spUsed;

    // ���¹̳� ����
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
    // ���¹̳� ������ �ð�
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
    // ���¹̳� ȸ��
    private void SPRecover()
    {
        if (!spUsed && currentSp < sp)
        {
            currentSp += spIncreaseSpeed;
        }
    }
    // ���¹̳� �Ҹ� ȿ�� ��� ����
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
    // ���� ���¹̳� �� ��ȯ
    public int GetCurrentSP()
    {
        return currentSp;
    }
    // �ʱ�ȭ �� ���� �� ȣ��Ǵ� �Լ�
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
    // �÷��̾� ��� �� ȣ��Ǵ� �Լ�
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
    //Ư����ǥ ���Ϸ� ���� ȣ��Ǵ� �Լ�
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
    // ���� ������ �� ������Ʈ

    private void GaugeUpdate()
    {
        images_Gauge[HP].fillAmount = (float)currentHp / hp;
        images_Gauge[SP].fillAmount = (float)currentSp / sp;
        images_Gauge[HUNGRY].fillAmount = (float)currentHungry / hungry;
        images_Gauge[THIRSTY].fillAmount = (float)currentThirsty / thirsty;
    }
}
