using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle2Controller : GunMainController
{
    public static bool isActivate = false;
    //public static bool isActivate = true;
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
        thesave.savedata.bulletCountrifle2 = currentGun.currentBulletCount;
        if (isActivate)
        {
            Ammotorifill();

           
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
    protected override void Ammotorifill()
    {
        currentGun.carryBulletCount = 0;
        for (int i = 0; i < theSlot.Length; i++)
        {
            if (theSlot[i].item != null)
            {
                if (theSlot[i].item.itemName == "5.56mm ammo")
                {
                    currentGun.carryBulletCount = theSlot[i].itemCount;
                    return;
                }
            }
        }
    }

    protected override void ammoReload(int _reloadammo)
    {
        for (int i = 0; i < theSlot.Length; i++)
        {
            if (theSlot[i].item != null)
            {
                if (theSlot[i].item.itemName == "5.56mm ammo")
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
            if (currentGun.currentBulletCount == 0)
            {
                if (currentGun.carryBulletCount >= currentGun.reloadBulletCount)
                {
                    currentGun.currentBulletCount = currentGun.reloadBulletCount;
                    currentGun.carryBulletCount -= currentGun.reloadBulletCount;
                    ammoReload(currentGun.reloadBulletCount);
                }
                else
                {
                    currentGun.currentBulletCount += currentGun.carryBulletCount;
                    currentGun.carryBulletCount = 0;
                    ammoReload(currentGun.reloadBulletCount);
                }

            }
            else if (currentGun.currentBulletCount > 0)
            {
                if (currentGun.reloadBulletCount - currentGun.currentBulletCount > currentGun.carryBulletCount)
                {
                    ammoReload(currentGun.carryBulletCount);
                    currentGun.currentBulletCount += currentGun.carryBulletCount;
                    currentGun.carryBulletCount = 0;
                }
                else
                {
                    ammoReload(currentGun.reloadBulletCount - currentGun.currentBulletCount);
                    currentGun.currentBulletCount += (currentGun.reloadBulletCount - currentGun.currentBulletCount);
                    currentGun.carryBulletCount -= (currentGun.reloadBulletCount - currentGun.currentBulletCount);
                }
            }
            else
            {
                currentGun.carryBulletCount = 0;
                currentGun.currentBulletCount = 0;
            }
            isReload = false;
        }
        else
        {
            Debug.Log("noammo");
        }
        if (currentGun.carryBulletCount < 0)
            currentGun.carryBulletCount = 0;
    }
}

