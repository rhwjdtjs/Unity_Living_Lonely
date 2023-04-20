using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FieldofView : MonoBehaviour
{
    [SerializeField] private float viewAngle = 0;  // 시야 각도
    [SerializeField] private float viewDistance = 0; // 시야 거리 
    [SerializeField] private LayerMask targetMask;  // 타겟 마스크
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
        if (!TotalGameManager.isPlayerDead) //플레이어가 죽지않았을때
        {
            if (thePlayer.isWalk && thePlayer.isCrouch)
            {
                viewAngle = 80f;
                viewDistance = 10;
            }
            if (thePlayer.isWalk && thePlayer.isRun)
            {
                viewAngle = 720f;
                viewDistance = 50;
            }
            if (thePlayer.isCrouch)
            {
                viewAngle = 80f;
                viewDistance = 10;
            }
            if (thePlayer.isWalk && !thePlayer.isRun && !thePlayer.isCrouch)
            {
                viewAngle = 100f;
                viewDistance = 30f;
            }
            if (GunMainController.isfire)
            {
                viewAngle = 720f;
                viewDistance = 80f;
            }
            if (GunMainController.isfire && thePlayer.isWalk && thePlayer.isRun && thePlayer.isCrouch)
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
            //플레이어의 상태에 따라 시야각 시야거리 매프레임마다 수정
            Sight();  // 매 프레임마다 시야 탐색
        }
        //Debug.Log("isfire"+GunMainController.isfire);
    }

    public Vector3 ReturnPlayerPos() //플레이어 위치 반환
    {
        return thePlayer.transform.position;
    }

    public bool Sight()
    {

        Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, targetMask);//view distance 만큼 구를 그리고 그 안에 있는 콜라이더들을 _target에 저장함

        for (int i = 0; i < _target.Length; i++)
        {
            Transform TargetTransform = _target[i].transform; //타겟의 위치를 저장
            if (TargetTransform.name == "Player") //타겟의 이름이 플레이어라면
            {
                Vector3 _direction = (TargetTransform.position - transform.position).normalized; //타겟위치와 좀비위치 사이의 벡터값을 구함
                float _angle = Vector3.Angle(_direction, transform.forward); //구한 벡터값과 전방벡터 사이의 각도를 구함

                if (_angle < viewAngle * 0.5f) //그 각도가 기존의 시야각보다 작으면
                {
                    RaycastHit _hit;
                    if (Physics.Raycast(transform.position + transform.up, _direction, out _hit, viewDistance)) //_direction방향으로 빔을 쏨
                    {
                        if (_hit.transform.name == "Player")
                        {
                            return true; //true 반환하면 시야각내에 플레이어가 있음
                        }
                    }
                }
            }


        }
        return false;
    }

    


}