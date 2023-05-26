using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunTextEditor : MonoBehaviour
{
    private Gun theGun; // Gun ��ũ��Ʈ ����
    [SerializeField] private Text pistol1; // �ǽ��� ź���� ǥ���ϴ� �ؽ�Ʈ ���
    [SerializeField] private Text pistol2; // �ǽ��� ź���� ǥ���ϴ� �ؽ�Ʈ ���
    [SerializeField] private Text rifle1; // ������ ź���� ǥ���ϴ� �ؽ�Ʈ ���
    [SerializeField] private Text rifle2; // ������ ź���� ǥ���ϴ� �ؽ�Ʈ ���
    [SerializeField] private Text rifle3; // ������ ź���� ǥ���ϴ� �ؽ�Ʈ ���
    [SerializeField] private Text tommygun; // ��̰� ź���� ǥ���ϴ� �ؽ�Ʈ ���
    [SerializeField] private Text shotgun; // ���� ź���� ǥ���ϴ� �ؽ�Ʈ ���

    // Start is called before the first frame update
    void Start()
    {
        theGun = FindObjectOfType<Gun>(); // Gun ��ũ��Ʈ�� ã�Ƽ� �Ҵ��մϴ�.
    }

    // �ѱ� ź�� ������ ǥ���մϴ�.
    private void UpdateGunAmmo()
    {
        pistol1.text = theGun.currentBulletCount + "/" + theGun.carryBulletCount; // �ǽ��� ź�� ���� ������Ʈ
        pistol2.text = theGun.currentBulletCount + "/" + theGun.carryBulletCount; // �ǽ��� ź�� ���� ������Ʈ
        rifle1.text = theGun.currentBulletCount + "/" + theGun.carryBulletCount; // ������ ź�� ���� ������Ʈ
        rifle2.text = theGun.currentBulletCount + "/" + theGun.carryBulletCount; // ������ ź�� ���� ������Ʈ
        rifle3.text = theGun.currentBulletCount + "/" + theGun.carryBulletCount; // ������ ź�� ���� ������Ʈ
        tommygun.text = theGun.currentBulletCount + "/" + theGun.carryBulletCount; // ��̰� ź�� ���� ������Ʈ
        shotgun.text = theGun.currentBulletCount + "/" + theGun.carryBulletCount; // ���� ź�� ���� ������Ʈ
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGunAmmo(); // �ѱ� ź�� ���� ������Ʈ
    }
}
