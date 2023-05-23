using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Monster : MonoBehaviour
{
    [SerializeField] protected string animalName; // 동물의 이름
    [SerializeField] protected int hp;  // 동물의 체력
    [SerializeField] protected float walkSpeed;  // 걷기 속력
    [SerializeField] protected float runSpeed;  // 달리기 속력
    [SerializeField] protected float turningSpeed;  // 회전 속력
    [SerializeField] protected float walkTime;  // 걷기 시간
    [SerializeField] protected float waitTime;  // 대기 시간
    [SerializeField] protected float runTime;  // 뛰기 시간
    [SerializeField] protected Animator anim;
    [SerializeField] protected AudioClip sound_Normal;
    [SerializeField] protected AudioClip sound_Hurt;
    [SerializeField] protected AudioClip sound_Dead;
    protected bool isAttacking; // 공격중인지 판별
    protected Vector3 destination;  // 목적지
    protected bool isChasing; // 추격중인지 판별
    protected FieldofView theFieldOfViewAngle;
    protected NavMeshAgent nav;
    protected float applySpeed;
    protected Vector3 direction;  // 방향
    protected bool isAction;  // 행동 중인지 아닌지 판별
    protected bool isWalking; // 걷는지, 안 걷는지 판별
    protected bool isRunning; // 달리는지 판별
    protected bool isDead;   // 죽었는지 판별
    protected float currentTime;
    protected StatusControllor theStatus;
    protected AudioSource theAudio;
    protected TotalGameManager thekillcount;
    protected DayContollor theday;
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
      //  nav.enabled = false;
    }

    protected void Start()
    {
        theday = FindObjectOfType<DayContollor>();
        thekillcount = FindObjectOfType<TotalGameManager>();
        currentTime = waitTime;   // 대기 시작
        isAction = true;   // 대기도 행동
        theAudio = GetComponent<AudioSource>();
      //  nav.enabled = true;
        theFieldOfViewAngle = GetComponent<FieldofView>();
        theStatus = FindObjectOfType<StatusControllor>();

    }

    protected virtual void Update()
    {
        if (!isDead)
        {
            Move();
           // Rotation();
            ElapseTime();
        }
        if(theday.isNight)
        {
            walkSpeed = 4f;
            runSpeed = 7f;
        }    
        else
        {
            walkSpeed = 1.5f;
            runSpeed = 3f;
        }
        if(TotalGameManager.survivaltimesecond>=180 && TotalGameManager.survivaltimesecond<=360)
        {
            if(theday.isNight)
            {
                walkSpeed = 6f;
                runSpeed = 8f;
                
            }
            else
            {
                walkSpeed = 3f;
                runSpeed = 6f;
            }
        }
    }

    protected void Move()
    {
        if (isWalking || isRunning && !isDead)
        {
         //   nav.enabled = true;
            nav.SetDestination(transform.position + destination * 5f);
        }
    }
    protected void ElapseTime()
    {
        if (isAction && !isDead)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0 && !isChasing && !isAttacking)  // 랜덤하게 다음 행동을 개시
                RESETACTION();
        }
    }

    protected virtual void RESETACTION()  // 다음 행동 준비
    {
       // nav.enabled = true;
        isAction = true;
        
        isWalking = false;
        anim.SetBool("Walk", isWalking);
        isRunning = false;
        anim.SetBool("Run", isRunning);
        nav.speed = walkSpeed;
        nav.ResetPath();
        destination.Set(Random.Range(-0.2f, 0.2f), 0f, Random.Range(0.5f, 1f));
    }

    protected void TryWalk()  // 걷기
    {
       // nav.enabled = true;
        currentTime = walkTime;
        isWalking = true;
        anim.SetBool("Walk", isWalking);
        nav.speed = walkSpeed;
    }
    public virtual void Damage(int _dmg, Vector3 _targetPos)
    {
        if (!isDead)
        {
            hp -= _dmg;

            if (hp <= 0)
            {
                Dead();
                return;
            }
            if (_dmg >= 20)
            {
                PlaySE(sound_Hurt);
                anim.SetTrigger("Hit");
            }
            // Run(_targetPos);
        }
    }

    protected void Dead()
    {
        nav.ResetPath();
        StopAllCoroutines();
        PlaySE(sound_Dead);

        isWalking = false;
        isRunning = false;
        isChasing = false;
        isAttacking = false;
        isDead = true;
        thekillcount.killcount++;
        Debug.Log(thekillcount.killcount);
        anim.SetTrigger("Dead");
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        Destroy(this.gameObject, 100f);
        
        SpawnManager._reserveCount--;
    }

    protected void RandomSound()
    {
        int _random = Random.Range(0, 3);  // 돼지의 일상 사운드는 3 개
        PlaySE(sound_Normal);
    }

    protected void PlaySE(AudioClip _clip)
    {
        theAudio.clip = _clip;
        theAudio.Play();
    }
}
