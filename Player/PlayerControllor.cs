using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private StatusController theStatusController; // ���� ��Ʈ�ѷ� ��ü
    [SerializeField] private float WalkSpeed; // �ȱ� �ӵ�
    [SerializeField] private float RunSpeed; // �޸��� �ӵ�
    [SerializeField] private float CrouchSpeed; // �ɱ� �ӵ�
    [SerializeField] private float ApplySpeed; // ����Ǵ� �ӵ�
    [SerializeField] private float JumpForce; // ���� ��
    [SerializeField] public bool isRun = false; // �޸����� ����
    [SerializeField] private float CrouchPosY; // ���� ������ ī�޶� Y ��ġ
    [SerializeField] private float LookSensitivity; // ���콺 ����
    [SerializeField] private float CamRotationLimit; // ī�޶� ȸ�� ����
    [SerializeField] private Camera TheCam; // ī�޶� ��ü
    [SerializeField] private Animator HandAnim; // �� �ִϸ�����
    [SerializeField] private Animator Pistol1Anim; // ����1 �ִϸ�����
    [SerializeField] private Animator Rifle1Anim; // ����1 �ִϸ�����
    [SerializeField] private AudioSource theWalkAudio; // �ȴ� �Ҹ� ����� �ҽ�
    [SerializeField] private AudioSource theRunningAudio; // �޸��� �Ҹ� ����� �ҽ�
    [SerializeField] private AudioClip theRunningClip; // �޸��� �Ҹ� Ŭ��
    [SerializeField] private SaveLoad theSave; // ���� �� �ҷ����� ��ü
    [SerializeField] private PlayfabSave thePlayfabSave; // �÷����� ���� �� �ҷ����� ��ü
    public bool isWalk = false; // �ȴ��� ����
    public bool isGround = true; // ���� ��� �ִ��� ����
    public bool isCrouch = false; // ���� �������� ����
    public float moveDirX; // X�� �̵� ����
    public float moveDirZ; // Z�� �̵� ����
    private float originalPosY; // ���� ī�޶� Y ��ġ
    private float applyCrouchPosY; // ����Ǵ� ���� ������ ī�޶� Y ��ġ
    private Vector3 lastPos; // ���� ��ġ
    private float currentCamRotationX; // ���� ī�޶� ȸ�� X ��
    private Rigidbody theRigid; // ������ٵ� ������Ʈ
    private CapsuleCollider theCapsuleCol; // ĸ�� �ݶ��̴� ������Ʈ
    private GunMainController theGunMain; // �ѱ� ���� ��Ʈ�ѷ� ��ü
    public static bool isPause = false; // �Ͻ� ���� ���� ����

    void Start()
    {
        theGunMain = FindObjectOfType<GunMainController>();
        theStatusController = FindObjectOfType<StatusController>();
        theCapsuleCol = GetComponent<CapsuleCollider>();
        theRigid = GetComponent<Rigidbody>();

        ApplySpeed = WalkSpeed;

        originalPosY = TheCam.transform.localPosition.y;
        applyCrouchPosY = originalPosY;
        //IswalkingSound();
    }

    void Update()
    {
        if (!isPause)
        {
            //   Debug.Log(GunMainController.isfire);
            if (!TotalGameManager.isPlayerDead)
            {
                IsWalkingSound();
                IsGround();
                //Jump();
                Running();
                RunningCancel();
                Move();
                if (!Inventory.invectoryActivated)
                {
                    CameraRotation();
                    CharacterRotation();
                }
                Crouch();
                Stamina();
            }
            if (Input.GetKeyDown(KeyCode.F5))
            {
                theSave.SaveData();
                thePlayfabSave.UploadSaveData();
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                LoadingSceneManager.LoadScene("mainmenu");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    // ���� ��� �ִ��� üũ�ϴ� �Լ�
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, TheCapsuleCol.bounds.extents.y + 0.1f);
    }

    // �ȴ� �Ҹ� ��� �Լ�
    private void IsWalkingSound()
    {
        if (isWalk && !isRun)
        {
            if (!theWalkAudio.isPlaying)
            {
                theWalkAudio.Play();
            }
        }

        if (isRun)
        {
            if (!theRunningAudio.isPlaying)
            {
                theRunningAudio.PlayOneShot(theRunningClip);
            }
        }
        else if (!isRun)
        {
            theRunningAudio.Stop();
        }

        if (!isWalk)
        {
            theRunningAudio.Stop();
        }
    }

    // ���� �Լ�
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            if (isCrouch)
                Crouch();

            theRigid.velocity = transform.up * JumpForce;
        }
    }

    // �޸��� �Լ�
    private void Running()
    {
        if (!theGunMain.isFindSightMode && !isCrouch && theStatusController.GetCurrentSP() > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (isCrouch)
                    Crouch();

                isRun = true;
                ApplySpeed = RunSpeed;
            }
        }
    }

    // ü�� �Ҹ� �Լ�
    private void Stamina()
    {
        if (isRun == true)
            theStatusController.DecreaseStamina(10);
    }

    // �޸��� ��� �Լ�
    private void RunningCancel()
    {
        if (!isCrouch)
        {
            if (Input.GetKey(KeyCode.S))
            {
                isRun = false;
                ApplySpeed = WalkSpeed;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) || theStatusController.GetCurrentSP() <= 0)
            {
                isRun = false;
                ApplySpeed = WalkSpeed;
            }
        }
    }

    // �ɱ� �Լ�
    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouch = !isCrouch;
            if (isCrouch)
            {
                ApplySpeed = CrouchSpeed;
                isWalk = false;
                applyCrouchPosY = CrouchPosY;
            }
            else
            {
                ApplySpeed = WalkSpeed;
                applyCrouchPosY = originalPosY;
            }

            StartCoroutine(CrouchCoroutine());
        }
    }

    // �ɱ� �ڷ�ƾ
    IEnumerator CrouchCoroutine()
    {
        float posY = TheCam.transform.localPosition.y;
        int count = 0;

        while (posY != applyCrouchPosY)
        {
            count++;
            posY = Mathf.Lerp(posY, applyCrouchPosY, 0.2f);
            TheCam.transform.localPosition = new Vector3(0, posY, 0);
            if (count > 15)
                break;
            yield return null;
        }
        TheCam.transform.localPosition = new Vector3(0, applyCrouchPosY, 0);
    }

    // �̵� �Լ�
    private void Move()
    {
        moveDirX = Input.GetAxisRaw("Horizontal");
        moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * moveDirX;
        Vector3 moveVertical = transform.forward * moveDirZ;
        Vector3 velocity = (moveHorizontal + moveVertical).normalized * ApplySpeed;

        theRigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    // ī�޶� ȸ�� �Լ�
    private void CameraRotation()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = xRotation * LookSensitivity;

        currentCamRotationX -= cameraRotationX;
        currentCamRotationX = Mathf.Clamp(currentCamRotationX, -CamRotationLimit, CamRotationLimit);

        TheCam.transform.localEulerAngles = new Vector3(currentCamRotationX, 0f, 0f);
    }

    // ĳ���� ȸ�� �Լ�
    private void CharacterRotation()
    {
        float yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 characterRotationY = new Vector3(0f, yRotation, 0f) * LookSensitivity;
        theRigid.MoveRotation(theRigid.rotation * Quaternion.Euler(characterRotationY));
    }

    // �޸��� ���� ��ȯ �Լ�
    public bool GetRun()
    {
        return isRun;
    }
}
