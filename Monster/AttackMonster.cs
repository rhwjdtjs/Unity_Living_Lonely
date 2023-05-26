using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackMonster : Monster
{
    [SerializeField] protected int attackDamage;
    [SerializeField] protected float attackDelay;
    [SerializeField] protected float attackDistance;
    [SerializeField] protected LayerMask targetMask; // 플레이어 레이어
    [SerializeField] protected float chaseTime;  // 총 추격 시간
    [SerializeField] protected float chaseDelayTime; // 추격 딜레이
    protected float currentChaseTime;
    // private StatusControllor thestatus;
    // [SerializeField] private Animator hitanim;

    // protected new void Start()
    // {
    //     thestatus = FindObjectOfType<StatusControllor>();
    // }

    // 목표 위치를 추적합니다.
    public void TargetChase(Vector3 _targetPos)
    {
        isChasing = true;
        destination = _targetPos;
        nav.speed = runSpeed;
        isRunning = true;
        anim.SetBool("Run", isRunning);

        if (!isDead)
            nav.SetDestination(destination);
    }

    // 상위 클래스의 Damage 메서드를 오버라이드합니다.
    public override void Damage(int _dmg, Vector3 _targetPos)
    {
        base.Damage(_dmg, _targetPos);
        if (!isDead)
            TargetChase(_targetPos);
    }

    // 목표를 추격하는 코루틴입니다.
    protected IEnumerator CHASETARGETCO()
    {
        if (!TotalGameManager.isPlayerDead)
        {
            currentChaseTime = 0;
            TargetChase(theFieldOfViewAngle.ReturnPlayerPos());

            while (currentChaseTime < chaseTime)
            {
                TargetChase(theFieldOfViewAngle.ReturnPlayerPos());

                // 플레이어가 충분히 가까이 있는지 확인합니다.
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

    // 공격하는 코루틴입니다.
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
        }

        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
        StartCoroutine(CHASETARGETCO());
    }
}
