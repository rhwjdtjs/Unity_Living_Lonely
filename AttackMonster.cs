using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AttackMonster : Monster
{
    [SerializeField]protected int attackDamage; 
    [SerializeField]protected float attackDelay; 
    [SerializeField]protected float attackDistance; 
    [SerializeField]protected LayerMask targetMask; // �÷��̾� ���̾�
    [SerializeField]protected float chaseTime;  // �� �߰� �ð�
    [SerializeField] protected float chaseDelayTime; // �߰� ������
    protected float currentChaseTime;
   // private StatusControllor thestatus;
    //[SerializeField] private Animator hitanim;

  //  protected new void Start()
  //  {
  //      thestatus = FindObjectOfType<StatusControllor>();
  //  }
    public void TargetChase(Vector3 _targetPos)
    {
        isChasing = true; //�߰� ���� Ȱ��ȭ
        destination = _targetPos; //�������� �÷��̾�� ����
        nav.speed = runSpeed; //�߰ݻ��¶� �̵��ӵ��� �޸��� �ӵ��� ����
        isRunning = true;
        anim.SetBool("Run", isRunning); //�ִϸ��̼� Ȱ��ȭ
        
        if (!isDead)
            nav.SetDestination(destination); //���� ���� �ʾҴٸ� �������� �̵�
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
                // �÷��̾�� ����� ������ �ֳ� 
                if (Vector3.Distance(transform.position, theFieldOfViewAngle.ReturnPlayerPos()) <= attackDistance)
                {
                    if (theFieldOfViewAngle.Sight() && !isDead)  // �� �տ� ���� ���
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
