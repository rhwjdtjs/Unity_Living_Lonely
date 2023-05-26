using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static bool isChangeWeapon = false;  // ���� ���� ���� ������ ���θ� ��Ÿ���� ����

    [SerializeField] private float ChangeDelay;  // ���� ���� ������ �ð�
    [SerializeField] private float ChangeEndDelay;  // ���� ���� �Ϸ� �� ������ �ð�
    [SerializeField] private Gun[] guns;  // �ѱ� �迭
    [SerializeField] private MeleeWeapon[] hands;  // �� ���� �迭
    [SerializeField] private MeleeWeapon[] axes;  // ���� ���� �迭
    [SerializeField] private MeleeWeapon[] Knifes;  // Į ���� �迭
    [SerializeField] private Gun[] rifle2;  // ����2 �迭
    [SerializeField] private Gun[] rifle1;  // ����1 �迭
    [SerializeField] private Gun[] Tommygun;  // �轼������� �迭
    [SerializeField] private Gun[] Pistol1;  // ����1 �迭
    [SerializeField] private Gun[] Pistol2;  // ����2 �迭
    [SerializeField] private Gun[] rifle3;  // ����3 �迭

    // �� ���� ������ ���� ��ųʸ� ������
    private Dictionary<string, Gun> Rifle1List = new Dictionary<string, Gun>();
    private Dictionary<string, Gun> Rifle3List = new Dictionary<string, Gun>();
    private Dictionary<string, Gun> Pistol1List = new Dictionary<string, Gun>();
    private Dictionary<string, Gun> Pistol2List = new Dictionary<string, Gun>();
    private Dictionary<string, Gun> Rifle2List = new Dictionary<string, Gun>();
    private Dictionary<string, Gun> tommygunList = new Dictionary<string, Gun>();
    private Dictionary<string, Gun> GunList = new Dictionary<string, Gun>();
    private Dictionary<string, MeleeWeapon> HandList = new Dictionary<string, MeleeWeapon>();
    private Dictionary<string, MeleeWeapon> AxeList = new Dictionary<string, MeleeWeapon>();
    private Dictionary<string, MeleeWeapon> KnifeList = new Dictionary<string, MeleeWeapon>();

    [SerializeField] private string WeaponTypeNow;  // ���� ������ Ÿ��
    public static Transform WeaponNow;  // ���� ����
    public static Animator WeaponNowAnim; // ���� ������ �ִϸ��̼�

    // �� ���� ��Ʈ�ѷ���� ���õ� ������
    [SerializeField] private GunMainController thegunmain;
    [SerializeField] private HandControllor theHandController;
    [SerializeField] private AxeControllor theAxeController;
    [SerializeField] private KnifeControllor theKnifeControllor;
    [SerializeField] private Rifle2Controller therifle2Controllor;
    [SerializeField] private Rifle1Contollor therifle1controllor;
    [SerializeField] private TommygunControllor thetommygunControllor;
    [SerializeField] private Rifle3Controllor therifle3controllor;
    [SerializeField] private Pistol1Controllor thepistol1controllor;
    [SerializeField] private Pistol2Cpmtrpllor thepistol2controllor;
    [SerializeField] private ShotgunController theshotgun;

    // ���� ���� Ÿ�Կ� ���� ���� ������
    public static bool isHand = false;
    public static bool isPistol = false;
    public static bool isRifle = false;
    public static bool isAxe = false;
    public static bool isKnife = false;
    public static bool isrifle2 = false;

    void Start()
    {
        // �� ���� ����Ʈ ��ųʸ��� ������� �߰�
        for (int i = 0; i < rifle3.Length; i++)
            Rifle3List.Add(rifle3[i].gunName, rifle3[i]);
        for (int i = 0; i < Pistol1.Length; i++)
            Pistol1List.Add(Pistol1[i].gunName, Pistol1[i]);
        for (int i = 0; i < Pistol2.Length; i++)
            Pistol2List.Add(Pistol2[i].gunName, Pistol2[i]);
        for (int i = 0; i < guns.Length; i++)
            GunList.Add(guns[i].gunName, guns[i]);
        for (int i = 0; i < rifle1.Length; i++)
            Rifle1List.Add(rifle1[i].gunName, rifle1[i]);
        for (int i = 0; i < Tommygun.Length; i++)
            tommygunList.Add(Tommygun[i].gunName, Tommygun[i]);
        for (int i = 0; i < rifle2.Length; i++)
            Rifle2List.Add(rifle2[i].gunName, rifle2[i]);
        for (int i = 0; i < hands.Length; i++)
            HandList.Add(hands[i].MeleeWeaponName, hands[i]);
        for (int i = 0; i < axes.Length; i++)
            AxeList.Add(axes[i].MeleeWeaponName, axes[i]);
        for (int i = 0; i < Knifes.Length; i++)
            KnifeList.Add(Knifes[i].MeleeWeaponName, Knifes[i]);

        StartCoroutine(STARTWEAPONCO("HAND", "Hand"));
    }

    // �ʱ� ���� ������ ���� �ڷ�ƾ
    public IEnumerator STARTWEAPONCO(string _type, string _name)
    {
        isChangeWeapon = true;

        yield return new WaitForSeconds(ChangeDelay);

        CancelActions();
        ChangeWeapon(_type, _name);

        yield return new WaitForSeconds(ChangeEndDelay);

        WeaponTypeNow = _type;
        isChangeWeapon = false;
    }

    // ���� ������ ���� �ڷ�ƾ
    public IEnumerator CHANGEWEAPONCO(string _type, string _name)
    {
        isChangeWeapon = true;
        WeaponNowAnim.SetTrigger("Weapon_Out");

        yield return new WaitForSeconds(ChangeDelay);

        CancelActions();
        ChangeWeapon(_type, _name);

        yield return new WaitForSeconds(ChangeEndDelay);

        WeaponTypeNow = _type;
        isChangeWeapon = false;
    }

    // ���� ���⿡ ���� �׼� ���
    private void CancelActions()
    {
        switch (WeaponTypeNow)
        {
            case "Shotgun":
                thegunmain.CancelReload();
                thegunmain.isFindSightMode = false;
                ShotgunController.isActivate = false;
                break;
            case "RIFLE1":
                thegunmain.CancelReload();
                thegunmain.isFindSightMode = false;
                Rifle1Contollor.isActivate = false;
                break;
            case "RIFLE3":
                thegunmain.CancelReload();
                thegunmain.isFindSightMode = false;
                Rifle3Controllor.isActivate = false;
                break;
            case "TOMMYGUN":
                thegunmain.CancelReload();
                thegunmain.isFindSightMode = false;
                TommygunControllor.isActivate = false;
                break;
            case "RIFLE2":
                thegunmain.CancelReload();
                thegunmain.isFindSightMode = false;
                Rifle2Controller.isActivate = false;
                break;
            case "PISTOL1":
                thegunmain.CancelReload();
                thegunmain.isFindSightMode = false;
                Pistol1Controllor.isActivate = false;
                break;
            case "PISTOL2":
                thegunmain.CancelReload();
                thegunmain.isFindSightMode = false;
                Pistol2Cpmtrpllor.isActivate = false;
                break;
            case "HAND":
                theHandController.isActivate = false;
                break;
            case "AXE":
                theAxeController.isActivate = false;
                break;
            case "KNIFE":
                theKnifeControllor.isActivate = false;
                break;
        }
    }

    // ���� ���� �Լ�
    private void ChangeWeapon(string _type, string _name)
    {
        Transform _selectWeapon = null;

        if (_type == "RIFLE1")
            _selectWeapon = GunList[_name].gameObject.transform;
        else if (_type == "RIFLE3")
            _selectWeapon = Rifle3List[_name].gameObject.transform;
        else if (_type == "PISTOL1")
            _selectWeapon = Pistol1List[_name].gameObject.transform;
        else if (_type == "PISTOL2")
            _selectWeapon = Pistol2List[_name].gameObject.transform;
        else if (_type == "RIFLE2")
            _selectWeapon = Rifle2List[_name].gameObject.transform;
        else if (_type == "TOMMYGUN")
            _selectWeapon = tommygunList[_name].gameObject.transform;
        else if (_type == "HAND")
            _selectWeapon = HandList[_name].gameObject.transform;
        else if (_type == "AXE")
            _selectWeapon = AxeList[_name].gameObject.transform;
        else if (_type == "KNIFE")
            _selectWeapon = KnifeList[_name].gameObject.transform;

        _selectWeapon.gameObject.SetActive(true);

        if (_type == "HAND")
            theHandController.selectedWeapon = _selectWeapon.GetComponent<MeleeWeapon>();
        else if (_type == "AXE")
            theAxeController.selectedWeapon = _selectWeapon.GetComponent<MeleeWeapon>();
        else if (_type == "KNIFE")
            theKnifeControllor.selectedWeapon = _selectWeapon.GetComponent<MeleeWeapon>();
        else if (_type == "RIFLE1")
            therifle1controllor.selectedWeapon = _selectWeapon.GetComponent<Gun>();
        else if (_type == "RIFLE3")
            therifle3controllor.selectedWeapon = _selectWeapon.GetComponent<Gun>();
        else if (_type == "TOMMYGUN")
            thetommygunControllor.selectedWeapon = _selectWeapon.GetComponent<Gun>();
        else if (_type == "RIFLE2")
            therifle2Controllor.selectedWeapon = _selectWeapon.GetComponent<Gun>();
        else if (_type == "PISTOL1")
            thepistol1controllor.selectedWeapon = _selectWeapon.GetComponent<Gun>();
        else if (_type == "PISTOL2")
            thepistol2controllor.selectedWeapon = _selectWeapon.GetComponent<Gun>();

        WeaponNow = _selectWeapon;
        WeaponNowAnim = _selectWeapon.GetComponent<Animator>();

        StartCoroutine(theUI.WeaponChangeCoroutine(_selectWeapon.GetComponent<Animator>().runtimeAnimatorController, _name));
    }

}