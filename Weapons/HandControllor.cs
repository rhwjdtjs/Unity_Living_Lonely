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
        if (isActivate)
            TryAttack();
            Moving();
    }
    public void Moving()
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
    public override void MeleeChangeWeapon(MeleeWeapon _closeWeapon)
    {
        base.MeleeChangeWeapon(_closeWeapon);
        isActivate = true;
    }
    protected override IEnumerator HITCO()
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
                    hitInfo.transform.GetComponent<Creature>().Damage(currentMeleeWeapon.damage, transform.position);
                }
                Debug.Log(hitInfo.transform.name);
            }
            yield return null;
        }
    }
}


