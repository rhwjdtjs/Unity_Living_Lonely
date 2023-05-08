using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeControllor : MeleeWeaponControllor
{
    public static bool isActivate = false;
    [SerializeField]private PlayerControllor thePlayer;
    [SerializeField]AudioSource hitsound;
    [SerializeField] Animator hitanim;
    void Update()
    {
        if (isActivate)
            TryAttack();
            Moving();
    }
    public void Moving() //걷기 애니메이션
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
    protected override IEnumerator HITCO() //나이프 히트 함수
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                isSwing = false;
                Debug.Log(hitInfo.transform.name);
                if (hitInfo.transform.tag == "theMonster")
                {
                    hitanim.SetTrigger("hit");
                    hitsound.PlayOneShot(currentMeleeWeapon.HitSound);
                    hitInfo.transform.GetComponent<Creature>().Damage(currentMeleeWeapon.damage, transform.position);
                }
            }
            yield return null;
        }
    }

    public override void MeleeChangeWeapon(MeleeWeapon _closeWeapon) //무기교체
    {
        base.MeleeChangeWeapon(_closeWeapon);
        isActivate = true;
    }
}
