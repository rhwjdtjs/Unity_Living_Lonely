using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle1Contollor : GunMainController
{
    public static bool isActivate = false;
    private Slot[] theSlot;
    private SaveLoad thesave;
    private void Start()
    {
        thesave = FindObjectOfType<SaveLoad>();
        WeaponManager.WeaponNow = currentGun.GetComponent<Transform>();
        WeaponManager.WeaponNowAnim = currentGun.anim;
        theSlot = go_SlotsParent.GetComponentsInChildren<Slot>();
    }
    protected override void ammoappear()
    {
        GunText.text = currentGun.currentBulletCount.ToString() + "/" + currentGun.carryBulletCount.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        thesave.savedata.bulletCountrifle1 = currentGun.currentBulletCount;
        
        if (isActivate)
        {
            Ammotorifill(); //�������Ӹ��� �κ��丮�� ź�� Ȯ��
            

            GunFireRateCalc();
            if (!Inventory.invectoryActivated)
            {
                TryFindSight();

                TryFire();
                TryReload();
                Moving();
            }
        }
        ammoappear();
    }
    protected override void Ammotorifill() //�κ��丮���� ���⿡ �˸��� ź��Ȯ���� ź�� ����
    {
        currentGun.carryBulletCount = 0;
        for (int i = 0; i < theSlot.Length; i++)
        {
            if (theSlot[i].item != null)
            {
                if (theSlot[i].item.itemName == "7.62mm ammo")
                {
                    currentGun.carryBulletCount = theSlot[i].itemCount;
                    return;
                }
            }
        }
    }

    protected override void ammoReload(int _reloadammo) //�κ��丮�� �ִ� ź�� ����Ͽ� ������
    {
        for (int i = 0; i < theSlot.Length; i++)
        {
            if (theSlot[i].item != null)
            {
                if (theSlot[i].item.itemName == "7.62mm ammo")
                {
                    theSlot[i].SetSlotCount(-_reloadammo);
                    Debug.Log(_reloadammo);
                    return;
                }
            }
        }
    }
    public override void GunChange(Gun _gun)
    {
        base.GunChange(_gun);
        isActivate = true;
    }

    protected override IEnumerator ReloadCoroutine()
    {
        // AmmoItemRifill();
        if (currentGun.carryBulletCount > 0)
        {

            isReload = true;
            currentGun.anim.ResetTrigger("Shot");
            currentGun.anim.ResetTrigger("FineSightShot");
            currentGun.anim.SetTrigger("Reload");

            PlaySE(currentGun.Reload_Sound);
            yield return new WaitForSeconds(currentGun.reloadTime);
            if (currentGun.currentBulletCount == 0) //���� �����Ǿ� �ִ� �Ѿ��� 0���϶�
            {
                if (currentGun.carryBulletCount >= currentGun.reloadBulletCount)  //�����Ǿ� �ִ� �Ѿ��� ���� �������� �Ѿ��� 60���̰� �� ���� �������ؾ��ϴ� ź���� 30���϶�
                {
                    currentGun.currentBulletCount = currentGun.reloadBulletCount; //�Ѿ��� 0�߿��� 30�߷� ����
                    currentGun.carryBulletCount -= currentGun.reloadBulletCount; //60���� ź���� 30���� ź������ ����
                    ammoReload(currentGun.reloadBulletCount); //�κ��丮���� 30�� ����
                }
                else //������ �Ǿ��ִ� ź���� ���� �������� ź���� 10���̰� �������ؾ��ϴ� �Ѿ��� ������ �� �̻��� 30���϶�
                {
                    currentGun.currentBulletCount += currentGun.carryBulletCount; //�������� ź���� ������ ���� �������� ź���� 10��ŭ�� ����
                    currentGun.carryBulletCount = 0; //ź���� ��� �Һ������� ���̳ʽ� ������ �Ǹ� �ȵǱ⿡ ���� ������ �ִ� �Ѿ��� 0���� ����
                    ammoReload(currentGun.reloadBulletCount); //���������� ������ ������ŭ ���� �κ��丮�� �������� �̶� �������.
                }

            }
            else if (currentGun.currentBulletCount > 0) //���� �����Ǿ� �ִ� ź���� 0���̻��϶�
            {
                if (currentGun.reloadBulletCount - currentGun.currentBulletCount > currentGun.carryBulletCount)
                // �����Ǿ� �ִ� �Ѿ��� 5���̰� ���� �� 20���� ź���� ������ �ִµ� 30���� ź���� �������ؾ��Ѵٸ�
                {
                    ammoReload(currentGun.carryBulletCount); //������ �ִ� �Ѿ��� �κ��丮���� ��� �Һ��ϰ�
                    currentGun.currentBulletCount += currentGun.carryBulletCount; //���� 5���� �����Ǿ� �ִٸ� 20���� �����Ͽ� 25�߷� ����
                    currentGun.carryBulletCount = 0; //20���� ź���� ��� ������ ������ 0���� ����
                }
                else //���� 5�� �����Ǿ� �ִµ� ���� �� 30���� ź���� ������ �ְ� 30���� ������ �ؾ��Ѵٸ�
                {
                    ammoReload(currentGun.reloadBulletCount - currentGun.currentBulletCount); //25���� �����ؾ��ϴ� 30���� 5�� �� 25���� ź�ุ �κ��丮���� �����Ѵ�.
                    currentGun.currentBulletCount += (currentGun.reloadBulletCount - currentGun.currentBulletCount);
                    //���� ������ �Ѿ��� ������ 5������ 30�� ���̳ʽ�5�� 25���� �������� 30�߷� �������Ѵ�.
                    currentGun.carryBulletCount -= (currentGun.reloadBulletCount - currentGun.currentBulletCount);
                    //���� ������ �ִ� �Ѿ� 30���� 30-5�� 25�� ��ŭ ���ҽ��� �������� �Ѿ��� 5���� ���´�. 
                }
            }
            else //�� �ܿ� 
            {
                currentGun.carryBulletCount = 0; 
                currentGun.currentBulletCount = 0;
                //��� �ʱ�ȭ
            }
            isReload = false;
        }
        else
        {
            Debug.Log("noammo");
        }
        if (currentGun.carryBulletCount < 0)
            currentGun.carryBulletCount = 0;
        //���׹����� ź���� 0���ϸ� �������� ź���� 0���� ����
    }
}
