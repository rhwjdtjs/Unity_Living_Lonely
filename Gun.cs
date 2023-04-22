using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gun : MonoBehaviour
{
    public string gunName;  // ���� �̸�. ���� ������ �������̱� ������.
    public float range;     // ���� ���� �Ÿ�. �Ѹ��� ���� �Ÿ� �ٸ�. �Ѿ��� �ʹ� �ָ����� ������ ���ư��� �ȵǴϱ�.
    public float accuracy;  // ���� ��Ȯ��. ���� �������� ��Ȯ���� �ٸ�.
    public float fireRate;  // ���� �ӵ�. �� �ѹ߰� �ѹ߰��� �ð� ��. ������ ���� ���� ���簡 ������. ���� �������� �ٸ�.
    public float reloadTime;// ������ �ӵ�. ���� �������� �ٸ�.
    public GameObject gunAmmo;
    public int damage;      // ���� ���ݷ�. ���� �������� �ٸ�.
    public int reloadBulletCount;   // ���� ������ ����. ������ �� �� �� �߾� ����. ���� �������� �ٸ�.
    public int currentBulletCount;  // ���� �� ���� źâ�� �����ִ� �Ѿ��� ����.
    public int maxBulletCount;      // �Ѿ��� �ִ� �� ������ ������ �� �ִ���. 
    public int carryBulletCount;    // ���� �� �ٱ����� �����ϰ� �ִ� �Ѿ��� �� ����.

    public float retroActionForce;  // �ݵ� ����. ���� �������� �ٸ�.
    public float retroActionFineSightForce; // �����ؽ� �ݵ� ����. ���� �������� �ٸ�.

    public Vector3 fineSightOriginPos;  // �����ؽ� ���� ���� ��ġ. ������ �� �� ���� ��ġ�� ���ϴϱ� �� ���� ��ġ!

    public Animator anim;   // ���� �ִϸ��̼��� ����� �ִϸ����� ������Ʈ
    public ParticleSystem muzzleFlash;  // ȭ���� ����Ʈ ����� ����� ��ƼŬ �ý��� ������Ʈ
    public ParticleSystem finesightmuzzle;
    public AudioClip fire_Sound;    // �� �߻� �Ҹ� ����� Ŭ��
    public AudioClip Reload_Sound; //������ ����
    public AudioClip HitBody_Sound; //�������� ����
    public AudioClip HitHead_Sound; //��弦 ����
    [SerializeField] public Animator crosshairanim; //��Ŭ�������� ũ�ν���� ������� �ִϸ�����
}
