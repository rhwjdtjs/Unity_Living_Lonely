using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class GunMainController : MonoBehaviour
{
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] protected Gun currentGun; // ���� ��� �ִ� ����Gun.cs �� �Ҵ� ��.
    [SerializeField] protected Camera theCam;  // ī�޶� �������� �� �߾ӿ� �߻��� �Ŷ�
    [SerializeField] protected Vector3 originPos;  // ���� ���� ��ġ(������ �����ϸ� ���߿� ���ƿ;� �ϴϱ�)
    [SerializeField] protected GameObject hitEffectPrefab;  // �ǰ� ����Ʈ
    [SerializeField] protected PlayerControllor thePlayer;
    [SerializeField] protected GameObject go_SlotsParent;
    [SerializeField] protected Text GunText;
    [SerializeField] protected GameObject themonsterblood;
 
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
     
    protected abstract void ammoappear(); //�ڽ� ��ũ��Ʈ�� ����
    protected abstract void Ammotorifill();//�ڽ� ��ũ��Ʈ�� ����
    protected abstract void ammoReload(int _reloadammo);//�ڽ� ��ũ��Ʈ�� ����
    protected IEnumerator GunisfireFalse() //���� ������ �ʽ�Ҵ��� ��ȯ��
    {
        isfire = true;
        yield return new WaitForSeconds(10f);
        isfire = false;
        yield return null;
    }
   
    protected void Moving() //�� ���⿡ ���� �ִϸ��̼� ���
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
        if (isReload) //������ ���̶��
        {
            StopAllCoroutines(); //�ڷ�ƾ�� ���� ������ ������ ĵ��
            isReload = false;
        }
    }
    public virtual void GunChange(Gun _gun)
    {
        if (WeaponManager.WeaponNow != null) //���� �������� ���Ⱑ ������
            WeaponManager.WeaponNow.gameObject.SetActive(false); //���� ��ü���ؾ��ϴ� ���� ���⸦ ����

        currentGun = _gun;
        WeaponManager.WeaponNow = currentGun.GetComponent<Transform>();
        WeaponManager.WeaponNowAnim = currentGun.anim; //���� ����ִ� ����� �ִϸ��̼��� �ٲ�

        currentGun.transform.localPosition = Vector3.zero; //���� ����ִ� ���� ��ġ ����
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

    protected void FindSight() //������
    {
        isFindSightMode = !isFindSightMode;
        currentGun.crosshairanim.SetBool("finesight", isFindSightMode);
        currentGun.anim.SetBool("FineSight", isFindSightMode);
    }
    protected void CancelFineSight() //������ ���
    {
        if (isFindSightMode)
            FindSight();
    }
    protected void GunFireRateCalc() //�� �߻�ð� ���
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
        if (isFindSightMode) //�������϶�
        {
           
            currentGun.anim.SetTrigger("FineSightShot");
            currentGun.currentBulletCount--;
            currentFireRate = currentGun.fireRate;  // ���� �ӵ� ����
            audioSource.PlayOneShot(currentGun.fire_Sound, 0.9f);
            // SoundManager.instance.PlaySE("Shot");
            currentGun.finesightmuzzle.Play();
            Hit();
            StopAllCoroutines();
            StartCoroutine(RetroActionCoroutine());
            StartCoroutine(GunisfireFalse());
        }
        else //�������� �ƴϰ� �Ϲݻ��¿��� ���� ��
        {
            
            currentGun.anim.SetTrigger("Shot");
            currentGun.currentBulletCount--;

            currentFireRate = currentGun.fireRate;  // ���� �ӵ� ����
            audioSource.PlayOneShot(currentGun.fire_Sound);
            //SoundManager.instance.PlaySE("Shot");

            currentGun.muzzleFlash.Play();
            Hit();
            StopAllCoroutines();
            StartCoroutine(RetroActionCoroutine());
            StartCoroutine(GunisfireFalse());
        }
    }
    protected IEnumerator RetroActionCoroutine() //�ݵ� �ִ� �Լ�
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
    protected float GetAccuracy() //��Ȯ�� ��� �Լ�
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
                //��ƼŬ �ý��� �߰�
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
