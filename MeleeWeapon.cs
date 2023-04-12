using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public string MeleeWeaponName;  // ���� ���� �̸�

    // ���� ���� ����
    public bool isHand;
    public bool isAxe;
    public bool isKnife;

    public float range; // ���� ����. ���� ������ ������ ������ ������
    public int damage; // ���ݷ�
    public float workSpeed; // �۾� �ӵ�
    public float attackDelay;  // ���� ������. ���콺 Ŭ���ϴ� ���� ���� ������ �� �����Ƿ�.
    public float attackDelayA;  // ���� Ȱ��ȭ ����. ���� �ִϸ��̼� �߿��� �ָ��� �� �������� �� ���� ���� �������� ���� �Ѵ�.
    public float attackDelayB;  // ���� ��Ȱ��ȭ ����. ���� �� ������ �ָ��� ���� �ִϸ��̼��� ���۵Ǹ� ���� �������� ���� �ȵȴ�.
    public Animator anim;  // �ִϸ����� ������Ʈ
    public AudioClip HitSound;
}
