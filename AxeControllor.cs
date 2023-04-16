using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeControllor : MeleeWeaponControllor
{
    // 활성화 여부
    public static bool isActivate = false;
    [SerializeField]private PlayerControllor thePlayer;
    [SerializeField]AudioSource hitsound;
    [SerializeField] Animator hitanim;
   
    private void Start()
    {
        WeaponManager.WeaponNow = currentMeleeWeapon.GetComponent<Transform>();
        WeaponManager.WeaponNowAnim = currentMeleeWeapon.anim;
    }
    public void Moving() //이동 애니메이션 함수
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
        if (isActivate) //무기를 들고있을때 함수 실행
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
                if (hitInfo.transform.tag == "theMonster") //때린상대가 몬스터 태그라면
                {
                    hitanim.SetTrigger("hit"); //애니메이션 재생
                    hitsound.PlayOneShot(currentMeleeWeapon.HitSound); //사운드 재생
                    hitInfo.transform.GetComponent<Creature>().Damage(currentMeleeWeapon.damage, transform.position); //데미지를 입힘
                }
            }
            yield return null;
        }
    }

    public override void MeleeChangeWeapon(MeleeWeapon _closeWeapon) //무기를 활성화 시킴
    {
        base.MeleeChangeWeapon(_closeWeapon);
        isActivate = true;
    }
}
