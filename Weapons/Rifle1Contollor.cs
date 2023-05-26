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
            Ammotorifill(); //매프레임마다 인벤토리에 탄약 확인
            

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
    protected override void Ammotorifill() //인벤토리에서 무기에 알맞은 탄약확인후 탄약 충전
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

    protected override void ammoReload(int _reloadammo) //인벤토리에 있는 탄약 사용하여 재장전
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
            if (currentGun.currentBulletCount == 0) //현재 장전되어 있는 총알이 0발일때
            {
                if (currentGun.carryBulletCount >= currentGun.reloadBulletCount)  //장전되어 있는 총알은 없고 소지중인 총알이 60발이고 그 총의 재장전해야하는 탄약이 30발일때
                {
                    currentGun.currentBulletCount = currentGun.reloadBulletCount; //총알이 0발에서 30발로 변경
                    currentGun.carryBulletCount -= currentGun.reloadBulletCount; //60개의 탄약이 30개의 탄약으로 감소
                    ammoReload(currentGun.reloadBulletCount); //인벤토리에서 30발 차감
                }
                else //재장전 되어있는 탄약은 없고 소지중인 탄약이 10개이고 재장전해야하는 총알의 개수가 그 이상인 30발일때
                {
                    currentGun.currentBulletCount += currentGun.carryBulletCount; //장전중인 탄약의 개수를 현재 소지중인 탄약인 10만큼만 더함
                    currentGun.carryBulletCount = 0; //탄약을 모두 소비했으니 마이너스 개수가 되면 안되기에 현재 가지고 있는 총알을 0으로 변경
                    ammoReload(currentGun.reloadBulletCount); //마찬가지로 재장전 개수만큼 차감 인벤토리의 아이템은 이때 사라진다.
                }

            }
            else if (currentGun.currentBulletCount > 0) //지금 장전되어 있는 탄약이 0발이상일때
            {
                if (currentGun.reloadBulletCount - currentGun.currentBulletCount > currentGun.carryBulletCount)
                // 장전되어 있는 총알은 5개이고 내가 총 20개의 탄약을 가지고 있는데 30개의 탄약을 재장전해야한다면
                {
                    ammoReload(currentGun.carryBulletCount); //가지고 있는 총알을 인벤토리에서 모두 소비하고
                    currentGun.currentBulletCount += currentGun.carryBulletCount; //만약 5발이 장전되어 있다면 20발을 장전하여 25발로 만듬
                    currentGun.carryBulletCount = 0; //20개의 탄약을 모두 재장전 했으니 0으로 변경
                }
                else //현재 5발 장전되어 있는데 내가 총 30개의 탄약을 가지고 있고 30발을 재장전 해야한다면
                {
                    ammoReload(currentGun.reloadBulletCount - currentGun.currentBulletCount); //25발을 장전해야하니 30에서 5를 뺀 25개의 탄약만 인벤토리에서 제거한다.
                    currentGun.currentBulletCount += (currentGun.reloadBulletCount - currentGun.currentBulletCount);
                    //현재 장전된 총알의 개수를 5개에서 30발 마이너스5인 25발을 증가시켜 30발로 재장전한다.
                    currentGun.carryBulletCount -= (currentGun.reloadBulletCount - currentGun.currentBulletCount);
                    //현재 가지고 있는 총알 30개를 30-5인 25개 만큼 감소시켜 소지중인 총알은 5개가 남는다. 
                }
            }
            else //그 외에 
            {
                currentGun.carryBulletCount = 0; 
                currentGun.currentBulletCount = 0;
                //모두 초기화
            }
            isReload = false;
        }
        else
        {
            Debug.Log("noammo");
        }
        if (currentGun.carryBulletCount < 0)
            currentGun.carryBulletCount = 0;
        //버그방지로 탄약이 0이하면 소지중인 탄약을 0으로 변경
    }
}
