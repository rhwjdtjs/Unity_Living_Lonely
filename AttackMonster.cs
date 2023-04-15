using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AttackMonster : Monster
{
    [SerializeField]protected int attackDamage; 
    [SerializeField]protected float attackDelay; 
    [SerializeField]protected float attackDistance; 
    [SerializeField]protected LayerMask targetMask; // 플레이어 레이어
    [SerializeField]protected float chaseTime;  // 총 추격 시간
    [SerializeField] protected float chaseDelayTime; // 추격 딜레이
    protected float currentChaseTime;
   // private StatusControllor thestatus;
    //[SerializeField] private Animator hitanim;

  //  protected new void Start()
  //  {
  //      thestatus = FindObjectOfType<StatusControllor>();
  //  }
    public void TargetChase(Vector3 _targetPos)
    {
        isChasing = true; //추격 상태 활성화
        destination = _targetPos; //목적지를 플레이어로 설정
        nav.speed = runSpeed; //추격상태라 이동속도를 달리는 속도로 지정
        isRunning = true;
        anim.SetBool("Run", isRunning); //애니메이션 활성화
        
        if (!isDead)
            nav.SetDestination(destination); //좀비가 죽지 않았다면 목적지로 이동
    }

    public override void Damage(int _dmg, Vector3 _targetPos)
    {
        base.Damage(_dmg, _targetPos);
        if (!isDead)
            TargetChase(_targetPos);
    }
    protected IEnumerator CHASETARGETCO()
    {
        if (!TotalGameManager.isPlayerDead)
        {
            currentChaseTime = 0; //초기화
            TargetChase(theFieldOfViewAngle.ReturnPlayerPos()); //플레이어 위치를 받아 추격

            while (currentChaseTime < chaseTime) //추격시간이라면
            {
                TargetChase(theFieldOfViewAngle.ReturnPlayerPos()); //플레이어 위치를 받아 추격
                // 플레이어와 충분히 가까이 있나 
                if (Vector3.Distance(transform.position, theFieldOfViewAngle.ReturnPlayerPos()) <= attackDistance) //몬스터와 플레이어 사이의 거리가 attackdistace 사이에 있다면
                {
                    if (theFieldOfViewAngle.Sight() && !isDead)  // 눈 앞에 있을 경우
                        StartCoroutine(ATTACKCO()); //공격시작
                }
                yield return new WaitForSeconds(chaseDelayTime); //대기
                currentChaseTime += chaseDelayTime;//다시 추격시작
            }
            isChasing = false;
            isRunning = false;
            anim.SetBool("Run", isRunning);
            nav.ResetPath();
        }
    }

    protected IEnumerator ATTACKCO()
    {
        PlaySE(sound_Normal);
        isAttacking = true; //함수가 불러와지면 공격 활성화
        nav.ResetPath(); //도중에 가던 목적지 초기화
        currentChaseTime = chaseTime;
        yield return new WaitForSeconds(0.5f); //잠깐 대기후
        transform.LookAt(theFieldOfViewAngle.ReturnPlayerPos()); //플레이어를 바라보고
        anim.SetTrigger("Attack"); //공격
        yield return new WaitForSeconds(0.5f);// 잠깐 대기후 대기를 안하면 공격이 연속으로 되어 피가 엄청나게 깎인다.
        RaycastHit _hit;
        if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out _hit, attackDistance, targetMask)) //전방으로 빔을 쏴서 맞췄다면
        {
            theStatus.DecreaseHP(attackDamage);//플레이어 체력 깎암
            StartCoroutine(theStatus.ImageCO()); //ui활성화
           // hitanim.Play("hitanim", -1, 1);
        }

        yield return new WaitForSeconds(attackDelay); //attackdelay만큼 대기
        isAttacking = false;
            StartCoroutine(CHASETARGETCO()); //다시 추격시작
    }
   
}
