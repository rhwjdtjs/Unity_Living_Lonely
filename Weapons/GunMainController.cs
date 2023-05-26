using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class GunMainController : MonoBehaviour
{
    [SerializeField] protected LayerMask layerMask;               // �浹�� ������ ���̾� ����ũ
    [SerializeField] public Gun currentGun;                       // ���� ��� ���� ���� ������ �Ҵ�� Gun.cs
    [SerializeField] protected Camera theCam;                      // ���߾ӿ� �߻��� ī�޶� ����
    [SerializeField] protected Vector3 originPos;                  // ������ ���� �� ���� ���� ��ġ�� ���ư� �� ���Ǵ� ��ġ ��
    [SerializeField] protected GameObject hitEffectPrefab;         // �ǰ� ����Ʈ ������
    [SerializeField] protected PlayerControllor thePlayer;        // �÷��̾� ��Ʈ�ѷ� ������ ���� ����
    [SerializeField] protected GameObject go_SlotsParent;          // ���� UI�� �θ� ������Ʈ
    [SerializeField] protected Text GunText;                      // ���� ������ ǥ���� �ؽ�Ʈ UI
    [SerializeField] protected GameObject themonsterblood;         // ���� ü���� ǥ���� ���� ������Ʈ



    public static bool isfire = false;
    //public static bool isActivate = true;
    protected float currentFireRate; // �� ���� 0 ���� ū ���ȿ��� �Ѿ��� �߻� ���� �ʴ´�. �ʱⰪ�� ���� �ӵ��� Gun.cs�� fireRate 
    public AudioSource audioSource;  // �߻� �Ҹ� �����
    public AudioSource audioSourceHit;
    protected bool isReload = false;
    public bool isFindSightMode = false; // ������ ������.
    protected float gunAccuracy;
    protected RaycastHit hitInfo;  // �Ѿ��� �浹 ����
    [SerializeField] private Animator hitanim;
    [SerializeField] private Camera mainCamera;

    protected abstract void ammoappear();
    protected abstract void Ammotorifill();
    protected abstract void ammoReload(int _reloadammo);
    protected IEnumerator GunisfireFalse()
    {
        isfire = true;                             // ���� �߻���
        yield return new WaitForSeconds(10f);      // 10�ʰ� ���
        isfire = false;                            // ���� �߻���������
        //���� ���� üũ������ �ش� �Լ� ���
        yield return null;
    }

    protected void Moving()
    {
        if (!thePlayer.isRun && thePlayer.isGround)  // �÷��̾ �޸��� �ʰ� ���� ���� ��
        {
            if (thePlayer._moveDirZ >= 0.1f)
            {
                thePlayer.isWalk = true;           // ������ �̵� ���� ��� �ȴ� ���·� ����
            }
            else if (thePlayer._moveDirZ <= -0.1f)
            {
                thePlayer.isWalk = true;           // �ڷ� �̵� ���� ��� �ȴ� ���·� ����
            }
            else if (thePlayer._moveDirX <= -0.1f)
            {
                thePlayer.isWalk = true;           // �������� �̵� ���� ��� �ȴ� ���·� ����
            }
            else if (thePlayer._moveDirX >= 0.1f)
            {
                thePlayer.isWalk = true;           // ���������� �̵� ���� ��� �ȴ� ���·� ����
            }
            else
            {
                thePlayer.isWalk = false;          // �̵����� �ʴ� ��� ���� �ʴ� ���·� ����
            }

            currentGun.anim.SetBool("Walk", thePlayer.isWalk);   // �ȴ� �ִϸ��̼� Ȱ��ȭ
        }

        if (thePlayer.isRun)
            currentGun.anim.SetBool("Run", thePlayer.isRun);     // �޸��� �ִϸ��̼� Ȱ��ȭ
        else if (!thePlayer.isRun)
            currentGun.anim.SetBool("Run", thePlayer.isRun);     // �޸��� �ʴ� �ִϸ��̼� Ȱ��ȭ
    }

    public void CancelReload()
    {
        if (isReload)
        {
            StopAllCoroutines();                  // ��� �ڷ�ƾ ����
            isReload = false;                      // ������ ��� ���� ����
        }
    }

    public virtual void GunChange(Gun _gun)
    {
        if (WeaponManager.WeaponNow != null)
            WeaponManager.WeaponNow.gameObject.SetActive(false);   // ���� �� ��Ȱ��ȭ

        currentGun = _gun;                           // ���ο� �� �Ҵ�
        WeaponManager.WeaponNow = currentGun.GetComponent<Transform>();    // ���� ��ġ ���� ������Ʈ
        WeaponManager.WeaponNowAnim = currentGun.anim;        // ���� �ִϸ��̼� ���� ������Ʈ

        currentGun.transform.localPosition = Vector3.zero;    // ���� ��ġ �ʱ�ȭ
        currentGun.gameObject.SetActive(true);                // ���ο� �� Ȱ��ȭ

        //isActivate = true;   // Ȱ��ȭ ���·� ����
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
            currentFireRate -= Time.deltaTime;  // ��, 1 �ʿ� 1 �� ���ҽ�Ų��.
    }

    protected void TryFire()  // �߻� �Է��� ����
    {
        if (Input.GetKey(KeyCode.Mouse0) && currentFireRate <= 0 && !isReload)
        {
           
            Fire();
        }
    }

    protected void Fire()  // �߻縦 ���� ����
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
        if (isFindSightMode)  // ���� ����� ��
        {
            currentGun.anim.SetTrigger("FineSightShot");      // ������ �߻� �ִϸ��̼� ���
            currentGun.currentBulletCount--;                  // ���� ź�� �� ����
            currentFireRate = currentGun.fireRate;            // ���� �ӵ� ����
            audioSource.PlayOneShot(currentGun.fire_Sound, 0.9f);  // �߻� ���� ���
            currentGun.finesightmuzzle.Play();                // ������ �� �߻� ����Ʈ ���
            Hit();                                            // ź���� ���� ��� ó��
            StopAllCoroutines();                              // ��� �ڷ�ƾ ����
            StartCoroutine(RetroActionCoroutine());           // �ݵ� �ڷ�ƾ ����
            StartCoroutine(GunisfireFalse());                 // ���� �ð� �� �� �߻� ���� ���·� ����
        }
        else  // ���� ��尡 �ƴ� ��
        {
            currentGun.anim.SetTrigger("Shot");               // �߻� �ִϸ��̼� ���
            currentGun.currentBulletCount--;                  // ���� ź�� �� ����
            currentFireRate = currentGun.fireRate;            // ���� �ӵ� ����
            audioSource.PlayOneShot(currentGun.fire_Sound);   // �߻� ���� ���
            currentGun.muzzleFlash.Play();                    // �߻� ����Ʈ ���
            Hit();                                            // ź���� ���� ��� ó��
            StopAllCoroutines();                              // ��� �ڷ�ƾ ����
            StartCoroutine(RetroActionCoroutine());           // �ݵ� �ڷ�ƾ ����
            StartCoroutine(GunisfireFalse());                 // ���� �ð� �� �� �߻� ���� ���·� ����
        }
    }

    protected IEnumerator RetroActionCoroutine()
    {
        Vector3 recoilBack = new Vector3(originPos.x, originPos.x, currentGun.retroActionForce);     // ������ �� ���� ���� �ִ� �ݵ�
        Vector3 retroActionRecoilBack = new Vector3(currentGun.fineSightOriginPos.x, currentGun.fineSightOriginPos.y, currentGun.retroActionFineSightForce);  // ������ ���� ���� �ִ� �ݵ�

        if (!isFindSightMode)  // �������� �ƴ� ����
        {
            currentGun.transform.localPosition = originPos;

            // �ݵ� ����
            while (currentGun.transform.localPosition.z <= currentGun.retroActionForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, recoilBack, 0.4f);
                yield return null;
            }

            // ����ġ
            while (currentGun.transform.localPosition != originPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.1f);
                yield return null;
            }
        }
        else  // ������ ����
        {
            currentGun.transform.localPosition = currentGun.fineSightOriginPos;

            // �ݵ� ����
            while (currentGun.transform.localPosition.z <= currentGun.retroActionFineSightForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, retroActionRecoilBack, 0.4f);
                yield return null;
            }

            // ����ġ
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
   
    protected void PlaySE(AudioClip _clip)  // �߻� �Ҹ� ���
    {
        audioSource.clip = _clip;  // ����� Ŭ�� �Ҵ�
        audioSource.Play();       // ����� ���
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
        // ī�޶� ���� ��ǥ!! (localPosition�� �ƴ�)
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

                // ��ƼŬ �ý��� �߰�
                GameObject bloodeffect = Instantiate(themonsterblood, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(bloodeffect, 2f);

                hitInfo.transform.GetComponent<Creature>().Damage(currentGun.damage, transform.position);
            }

            Debug.Log(hitInfo.transform.name);

            // �ǰ� ����Ʈ ����
            GameObject clone = Instantiate(hitEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(clone, 2f);
        }
    }
}
