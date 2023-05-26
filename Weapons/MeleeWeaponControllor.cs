using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeWeaponControllor : MonoBehaviour
{
    [SerializeField]protected MeleeWeapon currentMeleeWeapon; // ���� ������ ���� ���� 
    [SerializeField] protected LayerMask layerMask;
    protected bool isAttack = false;  // ���� ���� ������ 
    protected bool isSwing = false;  // ���� �ֵθ��� ������. isSwing = True �� ���� �������� ������ ���̴�.

    protected RaycastHit hitInfo;  // ���� ����(Hand)�� ���� �͵��� ����.



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
    // �浹 üũ�� �����Ѵ�.
    //����� �浹�� ��ü�� �ִ��� ����
    protected bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, currentMeleeWeapon.range,layerMask))
        {
            return true;
        }

        return false;
    }
    // ������ ������ ���� HIT �ڷ�ƾ�� �����Ѵ�.
    protected abstract IEnumerator HITCO();
    //���� ���⸦ �����Ѵ�

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
