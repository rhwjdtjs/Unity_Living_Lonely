using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerControllor : MonoBehaviour
{
    private StatusControllor theStatusController;
    [SerializeField]private float WalkSpeed;
    [SerializeField]private float RunSpeed;
    [SerializeField]private float CrouchSpeed;
    [SerializeField]private float ApplySpeed;
    [SerializeField]private float JumpForce;
    [SerializeField]public bool isRun = false;
    [SerializeField] private float CrPosY;
    [SerializeField] private float LookSensitivity;
    [SerializeField] private float CamRotationLimit;
    [SerializeField] private Camera TheCam;
    [SerializeField] private Animator HandAnim;
    [SerializeField] private Animator Pistol1Anim;
    [SerializeField] private Animator Rifle1Anim;
    [SerializeField] private AudioSource thewalkaudio;
    [SerializeField] private AudioSource therunningaudio;
    [SerializeField] private AudioClip therunningclip;
    public bool isWalk = false;
    public bool isGround = true;
    public bool isCrouch = false;
    public float _moveDirX;
    public float _moveDirZ;
    private float OriginalPosY;
    private float ApplyCrPosY;
    private Vector3 lastPos;
    private float CurrentCamRotationX;
    private Rigidbody TheRigid;
    private CapsuleCollider TheCapsuleCol;
    private GunMainController thegunmain;
    void Start()
    {
        thegunmain = FindObjectOfType<GunMainController>();
        theStatusController = FindObjectOfType<StatusControllor>();
        TheCapsuleCol = GetComponent<CapsuleCollider>();
        TheRigid = GetComponent<Rigidbody>();

        ApplySpeed = WalkSpeed;

        OriginalPosY = TheCam.transform.localPosition.y;
        ApplyCrPosY = OriginalPosY;
        //IswalkingSound();
    }

    void Update()
    {
     //   Debug.Log(GunMainController.isfire);
        if (!TotalGameManager.isPlayerDead)
        {
            IswalkingSound();
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
            stamina();
        }
    }
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, TheCapsuleCol.bounds.extents.y + 0.1f);
    }
    private void IswalkingSound()
    {
        if (isWalk && !isRun)
        {
            if (!thewalkaudio.isPlaying)
            {
                thewalkaudio.Play();

            }
        }

        if (isRun)
        {
            if (!therunningaudio.isPlaying)
            {
                therunningaudio.PlayOneShot(therunningclip);
            }
        }
        else if (!isRun)
            therunningaudio.Stop();

        if (!isWalk)
            therunningaudio.Stop();
        
        
              
                

    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            if (isCrouch)
                Crouch();

            TheRigid.velocity = transform.up * JumpForce;
        }
    }
    private void Running()
    {
        if (!thegunmain.isFindSightMode &&!isCrouch && theStatusController.GetCurrentSP() > 0)
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
    private void stamina()
    {
        if (isRun == true)
            theStatusController.DecreaseStamina(10);
    }
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
    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouch = !isCrouch;
            if (isCrouch)
            {
                ApplySpeed = CrouchSpeed;
                isWalk = false;
                ApplyCrPosY = CrPosY;
            }
            else
            {
                ApplySpeed = WalkSpeed;
                ApplyCrPosY = OriginalPosY;
            }

            StartCoroutine(CROUNCHCO());
        }
    }
    IEnumerator CROUNCHCO()
    {
        float _posY = TheCam.transform.localPosition.y;
        int count = 0;

        while (_posY != ApplyCrPosY)
        {
            count++;
            _posY = Mathf.Lerp(_posY, ApplyCrPosY, 0.2f);
            TheCam.transform.localPosition = new Vector3(0, _posY, 0);
            if (count > 15)
                break;
            yield return null;
        }
        TheCam.transform.localPosition = new Vector3(0, ApplyCrPosY, 0);
    }

    private void Move()
    {
        _moveDirX = Input.GetAxisRaw("Horizontal");
        _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * ApplySpeed;

        TheRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }
    
    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * LookSensitivity;

        CurrentCamRotationX -= _cameraRotationX;
        CurrentCamRotationX = Mathf.Clamp(CurrentCamRotationX, -CamRotationLimit, CamRotationLimit);

        TheCam.transform.localEulerAngles = new Vector3(CurrentCamRotationX, 0f, 0f);
    }

    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * LookSensitivity;
        TheRigid.MoveRotation(TheRigid.rotation * Quaternion.Euler(_characterRotationY));
    }
    public bool GetRun()
    {
        return isRun;
    }
}


