using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControllor : MeleeWeaponControllor
{
    public static bool isActivate = true;
    [SerializeField]private PlayerControllor thePlayer;
    [SerializeField]AudioSource hitsound;
    [SerializeField] Animator hitanim;
    void Update()
    {
        if (isActivate) //해당스크립트가 활성화 중일때
            TryAttack(); //공격이 가능하게함
            Moving();
    }
    public void Moving() //무기에 따른 애니메이션
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
    public override void MeleeChangeWeapon(MeleeWeapon _closeWeapon) //무기변경 함수
    {
        base.MeleeChangeWeapon(_closeWeapon);
        isActivate = true;
    }
    protected override IEnumerator HITCO() //타격함수
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                isSwing = false;
                if (hitInfo.transform.tag == "theMonster")
                {
                    hitanim.SetTrigger("hit");
                    hitsound.PlayOneShot(currentMeleeWeapon.HitSound);
                    hitInfo.transform.GetComponent<Creature>().Damage(currentMeleeWeapon.damage, transform.position); //크리쳐 스크립트의 데미지 함수를 가져옴
                }
                Debug.Log(hitInfo.transform.name);
            }
            yield return null;
        }
    }
}


