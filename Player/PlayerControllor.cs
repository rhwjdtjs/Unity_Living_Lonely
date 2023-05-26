using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private StatusController theStatusController; // 상태 컨트롤러 객체
    [SerializeField] private float WalkSpeed; // 걷기 속도
    [SerializeField] private float RunSpeed; // 달리기 속도
    [SerializeField] private float CrouchSpeed; // 앉기 속도
    [SerializeField] private float ApplySpeed; // 적용되는 속도
    [SerializeField] private float JumpForce; // 점프 힘
    [SerializeField] public bool isRun = false; // 달리는지 여부
    [SerializeField] private float CrouchPosY; // 앉은 상태의 카메라 Y 위치
    [SerializeField] private float LookSensitivity; // 마우스 감도
    [SerializeField] private float CamRotationLimit; // 카메라 회전 제한
    [SerializeField] private Camera TheCam; // 카메라 객체
    [SerializeField] private Animator HandAnim; // 손 애니메이터
    [SerializeField] private Animator Pistol1Anim; // 권총1 애니메이터
    [SerializeField] private Animator Rifle1Anim; // 소총1 애니메이터
    [SerializeField] private AudioSource theWalkAudio; // 걷는 소리 오디오 소스
    [SerializeField] private AudioSource theRunningAudio; // 달리는 소리 오디오 소스
    [SerializeField] private AudioClip theRunningClip; // 달리는 소리 클립
    [SerializeField] private SaveLoad theSave; // 저장 및 불러오기 객체
    [SerializeField] private PlayfabSave thePlayfabSave; // 플레이팹 저장 및 불러오기 객체
    public bool isWalk = false; // 걷는지 여부
    public bool isGround = true; // 땅에 닿아 있는지 여부
    public bool isCrouch = false; // 앉은 상태인지 여부
    public float moveDirX; // X축 이동 방향
    public float moveDirZ; // Z축 이동 방향
    private float originalPosY; // 원래 카메라 Y 위치
    private float applyCrouchPosY; // 적용되는 앉은 상태의 카메라 Y 위치
    private Vector3 lastPos; // 이전 위치
    private float currentCamRotationX; // 현재 카메라 회전 X 값
    private Rigidbody theRigid; // 리지드바디 컴포넌트
    private CapsuleCollider theCapsuleCol; // 캡슐 콜라이더 컴포넌트
    private GunMainController theGunMain; // 총기 메인 컨트롤러 객체
    public static bool isPause = false; // 일시 정지 상태 여부

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

    // 땅에 닿아 있는지 체크하는 함수
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, TheCapsuleCol.bounds.extents.y + 0.1f);
    }

    // 걷는 소리 재생 함수
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

    // 점프 함수
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            if (isCrouch)
                Crouch();

            theRigid.velocity = transform.up * JumpForce;
        }
    }

    // 달리기 함수
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

    // 체력 소모 함수
    private void Stamina()
    {
        if (isRun == true)
            theStatusController.DecreaseStamina(10);
    }

    // 달리기 취소 함수
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

    // 앉기 함수
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

    // 앉기 코루틴
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

    // 이동 함수
    private void Move()
    {
        moveDirX = Input.GetAxisRaw("Horizontal");
        moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * moveDirX;
        Vector3 moveVertical = transform.forward * moveDirZ;
        Vector3 velocity = (moveHorizontal + moveVertical).normalized * ApplySpeed;

        theRigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    // 카메라 회전 함수
    private void CameraRotation()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = xRotation * LookSensitivity;

        currentCamRotationX -= cameraRotationX;
        currentCamRotationX = Mathf.Clamp(currentCamRotationX, -CamRotationLimit, CamRotationLimit);

        TheCam.transform.localEulerAngles = new Vector3(currentCamRotationX, 0f, 0f);
    }

    // 캐릭터 회전 함수
    private void CharacterRotation()
    {
        float yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 characterRotationY = new Vector3(0f, yRotation, 0f) * LookSensitivity;
        theRigid.MoveRotation(theRigid.rotation * Quaternion.Euler(characterRotationY));
    }

    // 달리기 여부 반환 함수
    public bool GetRun()
    {
        return isRun;
    }
}
