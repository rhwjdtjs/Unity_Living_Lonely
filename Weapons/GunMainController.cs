using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class GunMainController : MonoBehaviour
{
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] public Gun currentGun; // 현재 들고 있는 총의Gun.cs 가 할당 됨.
    [SerializeField] protected Camera theCam;  // 카메라 시점에서 정 중앙에 발사할 거라서
    [SerializeField] protected Vector3 originPos;  // 원래 총의 위치(정조준 해제하면 나중에 돌아와야 하니까)
    [SerializeField] protected GameObject hitEffectPrefab;  // 피격 이펙트
    [SerializeField] protected PlayerControllor thePlayer;
    [SerializeField] protected GameObject go_SlotsParent;
    [SerializeField] protected Text GunText;
    [SerializeField] protected GameObject themonsterblood;
  
   
    
    public static bool isfire = false;
    //public static bool isActivate = true;
    protected float currentFireRate; // 이 값이 0 보다 큰 동안에는 총알이 발사 되지 않는다. 초기값은 연사 속도인 Gun.cs의 fireRate 
    public AudioSource audioSource;  // 발사 소리 재생기
    public AudioSource audioSourceHit;
    protected bool isReload = false;
    public bool isFindSightMode = false; // 정조준 중인지.
    protected float gunAccuracy;
    protected RaycastHit hitInfo;  // 총알의 충돌 정보
    [SerializeField] private Animator hitanim;
    [SerializeField] private Camera mainCamera;
                                   // protected Slot[] theSlot;
    /*
    public virtual void GunMainChange(MeleeWeapon _closeWeapon)
    {
        if (WeaponManager.WeaponNow != null)
            WeaponManager.WeaponNow.gameObject.SetActive(false);

        currentMeleeWeapon = _closeWeapon;
        WeaponManager.WeaponNow = currentMeleeWeapon.GetComponent<Transform>();
        WeaponManager.WeaponNowAnim = currentMeleeWeapon.anim;

        currentMeleeWeapon.transform.localPosition = Vector3.zero;
        currentMeleeWeapon.gameObject.SetActive(true);
    }
    */
    /*
    private void Start()
    {
        theSlot = go_SlotsParent.GetComponentsInChildren<Slot>();
        audioSource = GetComponent<AudioSource>();
        WeaponManager.WeaponNow = currentGun.GetComponent<Transform>();
        WeaponManager.WeaponNowAnim = currentGun.anim;
    }
    */
    /*
        void Update()
        {
            AmmoItemRifill();
            if (WeaponManager.isRifle)

            {
                GunFireRateCalc();
                if (!Inventory.invectoryActivated)
                {
                    TryFindSight();

                    TryFire();
                    TryReload();
                    Moving();
                }
            }
        }
    */
    /*
        public void AmmoItemRifill()
        {
            currentGun.carryBulletCount = 0;
            for (int i = 0; i < theSlot.Length; i++)
            {
                if (theSlot[i].item != null)
                {

                    currentGun.carryBulletCount = theSlot[i].itemCount;
                    return;

                }
            }
        }
    
        private void ammoReload(int _reloadammo)
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


                    if (theSlot[i].item.itemName == "5.56mm ammo")
                    {
                        theSlot[i].SetSlotCount(-_reloadammo);
                        Debug.Log(_reloadammo);
                        return;
                    }

                    if (theSlot[i].item.itemName == "9mm ammo")
                    {
                        theSlot[i].SetSlotCount(-_reloadammo);
                        Debug.Log(_reloadammo);
                        return;
                    }


                }
            }
        }
    */
    protected abstract void ammoappear();
    protected abstract void Ammotorifill();
    protected abstract void ammoReload(int _reloadammo);
    protected IEnumerator GunisfireFalse()
    {
        isfire = true;
        yield return new WaitForSeconds(10f);
        isfire = false;
        yield return null;
    }
   
    protected void Moving()
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


            currentGun.anim.SetBool("Walk", thePlayer.isWalk);


        }
        if (thePlayer.isRun)
            currentGun.anim.SetBool("Run", thePlayer.isRun);
        else if (!thePlayer.isRun)
            currentGun.anim.SetBool("Run", thePlayer.isRun);
    }

    public void CancelReload()
    {
        if (isReload)
        {
            StopAllCoroutines();
            isReload = false;
        }
    }
    public virtual void GunChange(Gun _gun)
    {
        if (WeaponManager.WeaponNow != null)
            WeaponManager.WeaponNow.gameObject.SetActive(false);

        currentGun = _gun;
        WeaponManager.WeaponNow = currentGun.GetComponent<Transform>();
        WeaponManager.WeaponNowAnim = currentGun.anim;

        currentGun.transform.localPosition = Vector3.zero;
        currentGun.gameObject.SetActive(true);

        //isActivate = true;
    }
    protected void TryFindSight()
    {
        if (Input.GetButtonDown("Fire2"))
        {
  
            FindSight();
            
        }
    }

    protected void FindSight()
    {
        isFindSightMode = !isFindSightMode;
        currentGun.crosshairanim.SetBool("finesight", isFindSightMode);
        currentGun.anim.SetBool("FineSight", isFindSightMode);
    }
    protected void CancelFineSight()
    {
        if (isFindSightMode)
            FindSight();
    }
    protected void GunFireRateCalc()
    {
        if (currentFireRate > 0)
            currentFireRate -= Time.deltaTime;  // 즉, 1 초에 1 씩 감소시킨다.
    }

    protected void TryFire()  // 발사 입력을 받음
    {
        if (Input.GetKey(KeyCode.Mouse0) && currentFireRate <= 0 && !isReload)
        {
           
            Fire();
        }
    }

    protected void Fire()  // 발사를 위한 과정
    {
        
        if (!isReload)
        {
            if (currentGun.currentBulletCount > 0)
                Shoot();
            else
            {
                CancelFineSight();
                StartCoroutine(ReloadCoroutine());
            }
        }
    }

    protected void Shoot()
    {
        if (isFindSightMode)
        {
           
            currentGun.anim.SetTrigger("FineSightShot");
            currentGun.currentBulletCount--;
            currentFireRate = currentGun.fireRate;  // 연사 속도 재계산
            audioSource.PlayOneShot(currentGun.fire_Sound, 0.9f);
            // SoundManager.instance.PlaySE("Shot");
            currentGun.finesightmuzzle.Play();
            Hit();
            StopAllCoroutines();
            StartCoroutine(RetroActionCoroutine());
            StartCoroutine(GunisfireFalse());
        }
        else
        {
            
            currentGun.anim.SetTrigger("Shot");
            currentGun.currentBulletCount--;

            currentFireRate = currentGun.fireRate;  // 연사 속도 재계산
            audioSource.PlayOneShot(currentGun.fire_Sound);
            //SoundManager.instance.PlaySE("Shot");

            currentGun.muzzleFlash.Play();
            Hit();
            StopAllCoroutines();
            StartCoroutine(RetroActionCoroutine());
            StartCoroutine(GunisfireFalse());
        }
    }
    
    protected IEnumerator RetroActionCoroutine()
    {
        Vector3 recoilBack = new Vector3(originPos.x, originPos.x, currentGun.retroActionForce);     // 정조준 안 했을 때의 최대 반동
        Vector3 retroActionRecoilBack = new Vector3(currentGun.fineSightOriginPos.x, currentGun.fineSightOriginPos.y, currentGun.retroActionFineSightForce);  // 정조준 했을 때의 최대 반동

        if (!isFindSightMode)  // 정조준이 아닌 상태
        {
            currentGun.transform.localPosition = originPos;

            // 반동 시작
            while (currentGun.transform.localPosition.z <= currentGun.retroActionForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, recoilBack, 0.4f);
                yield return null;
            }

            // 원위치
            while (currentGun.transform.localPosition != originPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.1f);
                yield return null;
            }
        }
        else  // 정조준 상태
        {
            currentGun.transform.localPosition = currentGun.fineSightOriginPos;

            // 반동 시작
            while (currentGun.transform.localPosition.z <= currentGun.retroActionFineSightForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, retroActionRecoilBack, 0.4f);
                yield return null;
            }

            // 원위치
            while (currentGun.transform.localPosition != currentGun.fineSightOriginPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.1f);
                yield return null;
            }
        }
    }
    
   

    protected void TryReload()
    {
        
        if (Input.GetKeyDown(KeyCode.R) && !isReload && currentGun.currentBulletCount < currentGun.reloadBulletCount)
        {
            CancelFineSight();
            StartCoroutine(ReloadCoroutine());

        }
    }
    protected abstract IEnumerator ReloadCoroutine();
   
    protected void PlaySE(AudioClip _clip)  // 발사 소리 재생
    {
        audioSource.clip = _clip;  // 오디오 클립 할당
        audioSource.Play();       // 오디오 재생
    }
    public Gun GetGun()
    {
        return currentGun;
    }
    protected float GetAccuracy()
    {
        if (thePlayer.isWalk)
            gunAccuracy = 0.06f;
        else if (thePlayer.isCrouch)
            gunAccuracy = 0.015f;
        else if (isFindSightMode)
            gunAccuracy = 0.001f;
        else
            gunAccuracy = 0.035f;
        return gunAccuracy;
    }
   
    protected void Hit()
    {
        // 카메라 월드 좌표!! (localPosition이 아님)
        if (Physics.Raycast(theCam.transform.position,
            theCam.transform.forward +
                new Vector3(Random.Range(-GetAccuracy() - currentGun.accuracy, -GetAccuracy() + currentGun.accuracy),
                            Random.Range(-GetAccuracy() - currentGun.accuracy, -GetAccuracy() + currentGun.accuracy),
                            0),
            out hitInfo,
            currentGun.range, layerMask))
        {
            if (hitInfo.transform.tag == "theMonster")
            {
                hitanim.SetTrigger("hit");
                // SoundManager.instance.PlaySE("Hit_Body");
                audioSourceHit.PlayOneShot(currentGun.HitHead_Sound);
                //파티클 시스템 추가
                GameObject bloodeffect= Instantiate(themonsterblood, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(bloodeffect, 2f);
                //StartCoroutine(hitimage());
               
                hitInfo.transform.GetComponent<Creature>().Damage(currentGun.damage, transform.position);
                
            }
           
           
            Debug.Log(hitInfo.transform.name);
            GameObject clone = Instantiate(hitEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(clone, 2f);
        }
    }
}
