using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeWeaponControllor : MonoBehaviour
{
    [SerializeField]protected MeleeWeapon currentMeleeWeapon; // 현재 장착된 근접 무기 
    [SerializeField] protected LayerMask layerMask;
    protected bool isAttack = false;  // 현재 공격 중인지 
    protected bool isSwing = false;  // 팔을 휘두르는 중인지. isSwing = True 일 때만 데미지를 적용할 것이다.

    protected RaycastHit hitInfo;  // 현재 무기(Hand)에 닿은 것들의 정보.



    protected void TryAttack()
    {
        if (!Inventory.invectoryActivated)
        {
            if (Input.GetButton("Fire1"))
            {
                if (!isAttack)
                {
                    StartCoroutine(ATTACKCO());
                }
            }
        }
    }
   
    protected IEnumerator ATTACKCO()
    {
        isAttack = true;
        currentMeleeWeapon.anim.SetTrigger("Attack");

        yield return new WaitForSeconds(currentMeleeWeapon.attackDelayA);
        isSwing = true;

        StartCoroutine(HITCO());

        yield return new WaitForSeconds(currentMeleeWeapon.attackDelayB);
        isSwing = false;

        yield return new WaitForSeconds(currentMeleeWeapon.attackDelay - currentMeleeWeapon.attackDelayA - currentMeleeWeapon.attackDelayB);
        isAttack = false;
    }
    // 충돌 체크를 수행한다.
    //무기로 충돌한 객체가 있는지 여부
    protected bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, currentMeleeWeapon.range,layerMask))
        {
            return true;
        }

        return false;
    }
    // 데미지 적용을 위한 HIT 코루틴을 실행한다.
    protected abstract IEnumerator HITCO();
    //근접 무기를 변경한다

    public virtual void MeleeChangeWeapon(MeleeWeapon _closeWeapon)
    {
        if (WeaponManager.WeaponNow != null)
            WeaponManager.WeaponNow.gameObject.SetActive(false);

        currentMeleeWeapon = _closeWeapon;
        WeaponManager.WeaponNow = currentMeleeWeapon.GetComponent<Transform>();
        WeaponManager.WeaponNowAnim = currentMeleeWeapon.anim;

        currentMeleeWeapon.transform.localPosition = Vector3.zero;
        currentMeleeWeapon.gameObject.SetActive(true);
    }
}
