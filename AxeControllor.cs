using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeControllor : MeleeWeaponControllor
{
    // Ȱ��ȭ ����
    public static bool isActivate = false;
    [SerializeField]private PlayerControllor thePlayer;
    [SerializeField]AudioSource hitsound;
    [SerializeField] Animator hitanim;
   
    private void Start()
    {
        WeaponManager.WeaponNow = currentMeleeWeapon.GetComponent<Transform>();
        WeaponManager.WeaponNowAnim = currentMeleeWeapon.anim;
    }
    public void Moving() //�̵� �ִϸ��̼� �Լ�
    {
        if (!thePlayer.isRun && thePlayer.isGround)
        {
            if (thePlayer._moveDirZ >= 0.1f)
            {
                thePlayer.isWalk = true;
            }
            else if (thePlayer._moveDirZ <= -0.1f)
            {
                thePlayer.isWalk = true;
            }
            else if (thePlayer._moveDirX <= -0.1f)
            {
                thePlayer.isWalk = true;

            }
            else if (thePlayer._moveDirX >= 0.1f)
            {
                thePlayer.isWalk = true;

            }
            else
            {
                thePlayer.isWalk = false;
            }

          
                currentMeleeWeapon.anim.SetBool("Walk", thePlayer.isWalk); 


        }
        if (thePlayer.isRun)
            currentMeleeWeapon.anim.SetBool("Run", thePlayer.isRun);
        else if (!thePlayer.isRun)
            currentMeleeWeapon.anim.SetBool("Run", thePlayer.isRun);
    }
    void Update()
    {
        if (isActivate) //���⸦ ��������� �Լ� ����
            TryAttack();
            Moving();
    }

    protected override IEnumerator HITCO()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                isSwing = false;
                Debug.Log(hitInfo.transform.name);
                if (hitInfo.transform.tag == "theMonster") //������밡 ���� �±׶��
                {
                    hitanim.SetTrigger("hit"); //�ִϸ��̼� ���
                    hitsound.PlayOneShot(currentMeleeWeapon.HitSound); //���� ���
                    hitInfo.transform.GetComponent<Creature>().Damage(currentMeleeWeapon.damage, transform.position); //�������� ����
                }
            }
            yield return null;
        }
    }

    public override void MeleeChangeWeapon(MeleeWeapon _closeWeapon) //���⸦ Ȱ��ȭ ��Ŵ
    {
        base.MeleeChangeWeapon(_closeWeapon);
        isActivate = true;
    }
}
