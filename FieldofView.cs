using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FieldofView : MonoBehaviour
{
    [SerializeField] private float viewAngle = 0;  // �þ� ����
    [SerializeField] private float viewDistance = 0; // �þ� �Ÿ� 
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
        if (!TotalGameManager.isPlayerDead) //�÷��̾ �����ʾ�����
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
            //�÷��̾��� ���¿� ���� �þ߰� �þ߰Ÿ� �������Ӹ��� ����
            Sight();  // �� �����Ӹ��� �þ� Ž��
        }
        //Debug.Log("isfire"+GunMainController.isfire);
    }

    public Vector3 ReturnPlayerPos() //�÷��̾� ��ġ ��ȯ
    {
        return thePlayer.transform.position;
    }

    public bool Sight()
    {

        Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, targetMask);//view distance ��ŭ ���� �׸��� �� �ȿ� �ִ� �ݶ��̴����� _target�� ������

        for (int i = 0; i < _target.Length; i++)
        {
            Transform TargetTransform = _target[i].transform; //Ÿ���� ��ġ�� ����
            if (TargetTransform.name == "Player") //Ÿ���� �̸��� �÷��̾���
            {
                Vector3 _direction = (TargetTransform.position - transform.position).normalized; //Ÿ����ġ�� ������ġ ������ ���Ͱ��� ����
                float _angle = Vector3.Angle(_direction, transform.forward); //���� ���Ͱ��� ���溤�� ������ ������ ����

                if (_angle < viewAngle * 0.5f) //�� ������ ������ �þ߰����� ������
                {
                    RaycastHit _hit;
                    if (Physics.Raycast(transform.position + transform.up, _direction, out _hit, viewDistance)) //_direction�������� ���� ��
                    {
                        if (_hit.transform.name == "Player")
                        {
                            return true; //true ��ȯ�ϸ� �þ߰����� �÷��̾ ����
                        }
                    }
                }
            }


        }
        return false;
    }

    


}