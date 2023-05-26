using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeControllor : MeleeWeaponControllor
{
    // Ȱ��ȭ ����
    public static bool isActivate = false;

    [SerializeField] private PlayerControllor thePlayer;    // PlayerControllor ������ ���� ����
    [SerializeField] private AudioSource hitsound;          // ���� ȿ������ ����ϱ� ���� AudioSource
    [SerializeField] private Animator hitanim;              // ���� �ִϸ��̼��� �����ϱ� ���� Animator

    private void Start()
    {
        // ���� �� ���� ���� ���� ����
        WeaponManager.WeaponNow = currentMeleeWeapon.GetComponent<Transform>();
        WeaponManager.WeaponNowAnim = currentMeleeWeapon.anim;
    }

    public void Moving()
    {
        if (!thePlayer.isRun && thePlayer.isGround)
        {
            // �̵� �Է¿� ���� �ȴ� ���� ����
            if (thePlayer._moveDirZ >= 0.1f || thePlayer._moveDirZ <= -0.1f || thePlayer._moveDirX <= -0.1f || thePlayer._moveDirX >= 0.1f)
            {
                thePlayer.isWalk = true;
            }
            else
            {
                thePlayer.isWalk = false;
            }

            // �ȴ� �ִϸ��̼� ����
            currentMeleeWeapon.anim.SetBool("Walk", thePlayer.isWalk);
        }

        // �޸��� ���� ����
        if (thePlayer.isRun)
            currentMeleeWeapon.anim.SetBool("Run", thePlayer.isRun);
        else if (!thePlayer.isRun)
            currentMeleeWeapon.anim.SetBool("Run", thePlayer.isRun);
    }

    void Update()
    {
        if (isActivate)
        {
            TryAttack();
            Moving();
        }
    }

    protected override IEnumerator HITCO()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                isSwing = false;
                Debug.Log(hitInfo.transform.name);

                // �浹�� ��ü�� "theMonster" �±׸� ������ �ִٸ� ���� ó��
                if (hitInfo.transform.tag == "theMonster")
                {
                    hitanim.SetTrigger("hit");   // ���� �ִϸ��̼� ���
                    hitsound.PlayOneShot(currentMeleeWeapon.HitSound);   // ���� ȿ���� ���
                    hitInfo.transform.GetComponent<Creature>().Damage(currentMeleeWeapon.damage, transform.position);   // ���� ��󿡰� ������ ������
                }
            }
            yield return null;
        }
    }

    public override void MeleeChangeWeapon(MeleeWeapon _closeWeapon)
    {
        base.MeleeChangeWeapon(_closeWeapon);
        isActivate = true;   // ���� ���� �� Ȱ��ȭ ���·� ����
    }
}
