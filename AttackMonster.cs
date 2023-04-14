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
            currentChaseTime = 0;
            TargetChase(theFieldOfViewAngle.ReturnPlayerPos());

            while (currentChaseTime < chaseTime)
            {
                TargetChase(theFieldOfViewAngle.ReturnPlayerPos());
                // 플레이어와 충분히 가까이 있나 
                if (Vector3.Distance(transform.position, theFieldOfViewAngle.ReturnPlayerPos()) <= attackDistance)
                {
                    if (theFieldOfViewAngle.Sight() && !isDead)  // 눈 앞에 있을 경우
                        StartCoroutine(ATTACKCO());
                }
                yield return new WaitForSeconds(chaseDelayTime);
                currentChaseTime += chaseDelayTime;
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
        isAttacking = true;
        nav.ResetPath();
        currentChaseTime = chaseTime;
        yield return new WaitForSeconds(0.5f);
        transform.LookAt(theFieldOfViewAngle.ReturnPlayerPos());
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        RaycastHit _hit;
        if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out _hit, attackDistance, targetMask))
        {
            theStatus.DecreaseHP(attackDamage);
            StartCoroutine(theStatus.ImageCO());
           // hitanim.Play("hitanim", -1, 1);
        }

        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
            StartCoroutine(CHASETARGETCO());
    }
   
}
