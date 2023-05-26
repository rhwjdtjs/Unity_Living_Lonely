using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeControllor : MeleeWeaponControllor
{
    // 활성화 여부
    public static bool isActivate = false;

    [SerializeField] private PlayerControllor thePlayer;    // PlayerControllor 참조를 위한 변수
    [SerializeField] private AudioSource hitsound;          // 공격 효과음을 재생하기 위한 AudioSource
    [SerializeField] private Animator hitanim;              // 공격 애니메이션을 제어하기 위한 Animator

    private void Start()
    {
        // 시작 시 현재 무기 정보 설정
        WeaponManager.WeaponNow = currentMeleeWeapon.GetComponent<Transform>();
        WeaponManager.WeaponNowAnim = currentMeleeWeapon.anim;
    }

    public void Moving()
    {
        if (!thePlayer.isRun && thePlayer.isGround)
        {
            // 이동 입력에 따라 걷는 상태 설정
            if (thePlayer._moveDirZ >= 0.1f || thePlayer._moveDirZ <= -0.1f || thePlayer._moveDirX <= -0.1f || thePlayer._moveDirX >= 0.1f)
            {
                thePlayer.isWalk = true;
            }
            else
            {
                thePlayer.isWalk = false;
            }

            // 걷는 애니메이션 설정
            currentMeleeWeapon.anim.SetBool("Walk", thePlayer.isWalk);
        }

        // 달리기 상태 설정
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

                // 충돌한 객체가 "theMonster" 태그를 가지고 있다면 공격 처리
                if (hitInfo.transform.tag == "theMonster")
                {
                    hitanim.SetTrigger("hit");   // 공격 애니메이션 재생
                    hitsound.PlayOneShot(currentMeleeWeapon.HitSound);   // 공격 효과음 재생
                    hitInfo.transform.GetComponent<Creature>().Damage(currentMeleeWeapon.damage, transform.position);   // 공격 대상에게 데미지 입히기
                }
            }
            yield return null;
        }
    }

    public override void MeleeChangeWeapon(MeleeWeapon _closeWeapon)
    {
        base.MeleeChangeWeapon(_closeWeapon);
        isActivate = true;   // 무기 변경 시 활성화 상태로 설정
    }
}
