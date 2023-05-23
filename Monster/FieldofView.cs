using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FieldofView : MonoBehaviour
{
    [SerializeField] private float viewAngle=0;  // �þ� ����
    [SerializeField] private float viewDistance=0; // �þ� �Ÿ� 
    [SerializeField] private LayerMask targetMask;  // Ÿ�� ����ũ
    private NavMeshAgent nav;
    private PlayerControllor thePlayer;
   // private GunMainController thegun;
    void Start()
    {
      //  thegun = FindObjectOfType<GunMainController>();
        thePlayer = FindObjectOfType<PlayerControllor>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!TotalGameManager.isPlayerDead)
        {
            if(thePlayer.isWalk && thePlayer.isCrouch)
            {
                viewAngle = 80f;
                viewDistance = 10;
            }
            if(thePlayer.isWalk && thePlayer.isRun)
            {
                viewAngle = 720f;
                viewDistance = 50;
            }
            if(thePlayer.isCrouch)
            {
                viewAngle = 80f;
                viewDistance = 10;
            }
            if(thePlayer.isWalk && !thePlayer.isRun && !thePlayer.isCrouch)
            {
                viewAngle = 100f;
                viewDistance = 30f;
            }
            if(GunMainController.isfire)
            {
                viewAngle = 720f;
                viewDistance = 80f;
            }
            if (GunMainController.isfire &&thePlayer.isWalk && thePlayer.isRun && thePlayer.isCrouch)
            {
                viewAngle = 720f;
                viewDistance = 80f;
            }
            if (GunMainController.isfire && thePlayer.isWalk && thePlayer.isRun)
            {
                viewAngle = 720f;
                viewDistance = 80f;
            }
            if (GunMainController.isfire && thePlayer.isWalk)
            {
                viewAngle = 720f;
                viewDistance = 80f;
            }
            Sight();  // �� �����Ӹ��� �þ� Ž��
        }
        //Debug.Log("isfire"+GunMainController.isfire);
    }

    public Vector3 ReturnPlayerPos()
    {
        return thePlayer.transform.position;
    }

    public bool Sight()
    {

        Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

        for (int i = 0; i < _target.Length; i++)
        {
            Transform TargetTransform = _target[i].transform;
            if (TargetTransform.name == "Player")
            {
                Vector3 _direction = (TargetTransform.position - transform.position).normalized;
                float _angle = Vector3.Angle(_direction, transform.forward);

                if (_angle < viewAngle * 0.5f)
                {
                    RaycastHit _hit;
                    if (Physics.Raycast(transform.position + transform.up, _direction, out _hit, viewDistance))
                    {
                        if (_hit.transform.name == "Player")
                        {
                            return true;
                        }
                    }
                }
            }

         
        }
            return false;
        }
    

    private float PathCalculate(Vector3 _targetPos)
    {
        NavMeshPath _path = new NavMeshPath();
        nav.CalculatePath(_targetPos, _path);

        Vector3[] _wayPoint = new Vector3[_path.corners.Length + 2];

        _wayPoint[0] = transform.position;
        _wayPoint[_path.corners.Length + 1] = _targetPos;

        float _pathLength = 0;  // ��� ���̸� ����
        for (int i = 0; i < _path.corners.Length; i++)
        {
            _wayPoint[i + 1] = _path.corners[i];
            _pathLength += Vector3.Distance(_wayPoint[i], _wayPoint[i + 1]);
        }

        return _pathLength;
    }
}