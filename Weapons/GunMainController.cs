using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class GunMainController : MonoBehaviour
{
    [SerializeField] protected LayerMask layerMask;               // 충돌을 검출할 레이어 마스크
    [SerializeField] public Gun currentGun;                       // 현재 사용 중인 총의 정보가 할당될 Gun.cs
    [SerializeField] protected Camera theCam;                      // 정중앙에 발사할 카메라 시점
    [SerializeField] protected Vector3 originPos;                  // 정조준 해제 시 원래 총의 위치로 돌아갈 때 사용되는 위치 값
    [SerializeField] protected GameObject hitEffectPrefab;         // 피격 이펙트 프리팹
    [SerializeField] protected PlayerControllor thePlayer;        // 플레이어 컨트롤러 참조를 위한 변수
    [SerializeField] protected GameObject go_SlotsParent;          // 슬롯 UI의 부모 오브젝트
    [SerializeField] protected Text GunText;                      // 총의 정보를 표시할 텍스트 UI
    [SerializeField] protected GameObject themonsterblood;         // 몬스터 체력을 표시할 게임 오브젝트



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

    protected abstract void ammoappear();
    protected abstract void Ammotorifill();
    protected abstract void ammoReload(int _reloadammo);
    protected IEnumerator GunisfireFalse()
    {
        isfire = true;                             // 총을 발사함
        yield return new WaitForSeconds(10f);      // 10초간 대기
        isfire = false;                            // 총을 발사하지않음
        //좀비 사운드 체크때문에 해당 함수 사용
        yield return null;
    }

    protected void Moving()
    {
        if (!thePlayer.isRun && thePlayer.isGround)  // 플레이어가 달리지 않고 땅에 있을 때
        {
            if (thePlayer._moveDirZ >= 0.1f)
            {
                thePlayer.isWalk = true;           // 앞으로 이동 중인 경우 걷는 상태로 설정
            }
            else if (thePlayer._moveDirZ <= -0.1f)
            {
                thePlayer.isWalk = true;           // 뒤로 이동 중인 경우 걷는 상태로 설정
            }
            else if (thePlayer._moveDirX <= -0.1f)
            {
                thePlayer.isWalk = true;           // 왼쪽으로 이동 중인 경우 걷는 상태로 설정
            }
            else if (thePlayer._moveDirX >= 0.1f)
            {
                thePlayer.isWalk = true;           // 오른쪽으로 이동 중인 경우 걷는 상태로 설정
            }
            else
            {
                thePlayer.isWalk = false;          // 이동하지 않는 경우 걷지 않는 상태로 설정
            }

            currentGun.anim.SetBool("Walk", thePlayer.isWalk);   // 걷는 애니메이션 활성화
        }

        if (thePlayer.isRun)
            currentGun.anim.SetBool("Run", thePlayer.isRun);     // 달리는 애니메이션 활성화
        else if (!thePlayer.isRun)
            currentGun.anim.SetBool("Run", thePlayer.isRun);     // 달리지 않는 애니메이션 활성화
    }

    public void CancelReload()
    {
        if (isReload)
        {
            StopAllCoroutines();                  // 모든 코루틴 정지
            isReload = false;                      // 재장전 취소 상태 설정
        }
    }

    public virtual void GunChange(Gun _gun)
    {
        if (WeaponManager.WeaponNow != null)
            WeaponManager.WeaponNow.gameObject.SetActive(false);   // 현재 총 비활성화

        currentGun = _gun;                           // 새로운 총 할당
        WeaponManager.WeaponNow = currentGun.GetComponent<Transform>();    // 총의 위치 정보 업데이트
        WeaponManager.WeaponNowAnim = currentGun.anim;        // 총의 애니메이션 정보 업데이트

        currentGun.transform.localPosition = Vector3.zero;    // 총의 위치 초기화
        currentGun.gameObject.SetActive(true);                // 새로운 총 활성화

        //isActivate = true;   // 활성화 상태로 설정
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
        if (isFindSightMode)  // 조준 모드일 때
        {
            currentGun.anim.SetTrigger("FineSightShot");      // 정조준 발사 애니메이션 재생
            currentGun.currentBulletCount--;                  // 현재 탄약 수 감소
            currentFireRate = currentGun.fireRate;            // 연사 속도 재계산
            audioSource.PlayOneShot(currentGun.fire_Sound, 0.9f);  // 발사 사운드 재생
            currentGun.finesightmuzzle.Play();                // 정조준 시 발사 이펙트 재생
            Hit();                                            // 탄약이 맞은 대상 처리
            StopAllCoroutines();                              // 모든 코루틴 정지
            StartCoroutine(RetroActionCoroutine());           // 반동 코루틴 시작
            StartCoroutine(GunisfireFalse());                 // 일정 시간 후 총 발사 가능 상태로 변경
        }
        else  // 조준 모드가 아닐 때
        {
            currentGun.anim.SetTrigger("Shot");               // 발사 애니메이션 재생
            currentGun.currentBulletCount--;                  // 현재 탄약 수 감소
            currentFireRate = currentGun.fireRate;            // 연사 속도 재계산
            audioSource.PlayOneShot(currentGun.fire_Sound);   // 발사 사운드 재생
            currentGun.muzzleFlash.Play();                    // 발사 이펙트 재생
            Hit();                                            // 탄약이 맞은 대상 처리
            StopAllCoroutines();                              // 모든 코루틴 정지
            StartCoroutine(RetroActionCoroutine());           // 반동 코루틴 시작
            StartCoroutine(GunisfireFalse());                 // 일정 시간 후 총 발사 가능 상태로 변경
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

                // 파티클 시스템 추가
                GameObject bloodeffect = Instantiate(themonsterblood, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(bloodeffect, 2f);

                hitInfo.transform.GetComponent<Creature>().Damage(currentGun.damage, transform.position);
            }

            Debug.Log(hitInfo.transform.name);

            // 피격 이펙트 생성
            GameObject clone = Instantiate(hitEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(clone, 2f);
        }
    }
}
