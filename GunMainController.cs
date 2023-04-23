using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class GunMainController : MonoBehaviour
{
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] protected Gun currentGun; // 현재 들고 있는 총의Gun.cs 가 할당 됨.
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
     
    protected abstract void ammoappear(); //자식 스크립트에 선언
    protected abstract void Ammotorifill();//자식 스크립트에 선언
    protected abstract void ammoReload(int _reloadammo);//자식 스크립트에 선언
    protected IEnumerator GunisfireFalse() //총을 쐈는지 않쏘았는지 반환함
    {
        isfire = true;
        yield return new WaitForSeconds(10f);
        isfire = false;
        yield return null;
    }
   
    protected void Moving() //각 무기에 맞은 애니메이션 재생
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
        if (isReload) //재정잔 중이라면
        {
            StopAllCoroutines(); //코루틴을 멈춰 강제로 재장전 캔슬
            isReload = false;
        }
    }
    public virtual void GunChange(Gun _gun)
    {
        if (WeaponManager.WeaponNow != null) //지금 장착중인 무기가 있으면
            WeaponManager.WeaponNow.gameObject.SetActive(false); //무기 교체를해야하니 현재 무기를 숨김

        currentGun = _gun;
        WeaponManager.WeaponNow = currentGun.GetComponent<Transform>();
        WeaponManager.WeaponNowAnim = currentGun.anim; //현재 들고있는 무기로 애니메이션을 바뀜

        currentGun.transform.localPosition = Vector3.zero; //현재 들고있는 무기 위치 수정
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

    protected void FindSight() //정조준
    {
        isFindSightMode = !isFindSightMode;
        currentGun.crosshairanim.SetBool("finesight", isFindSightMode);
        currentGun.anim.SetBool("FineSight", isFindSightMode);
    }
    protected void CancelFineSight() //정조준 취소
    {
        if (isFindSightMode)
            FindSight();
    }
    protected void GunFireRateCalc() //총 발사시간 계산
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
        if (isFindSightMode) //정조준일때
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
        else //정조준이 아니고 일반상태에서 총을 쏠때
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
    protected IEnumerator RetroActionCoroutine() //반동 주는 함수
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
    protected float GetAccuracy() //정확도 계산 함수
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
