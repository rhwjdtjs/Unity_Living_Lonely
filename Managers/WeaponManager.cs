using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static bool isChangeWeapon = false;  // 현재 무기 변경 중인지 여부를 나타내는 변수

    [SerializeField] private float ChangeDelay;  // 무기 변경 딜레이 시간
    [SerializeField] private float ChangeEndDelay;  // 무기 변경 완료 후 딜레이 시간
    [SerializeField] private Gun[] guns;  // 총기 배열
    [SerializeField] private MeleeWeapon[] hands;  // 손 무기 배열
    [SerializeField] private MeleeWeapon[] axes;  // 도끼 무기 배열
    [SerializeField] private MeleeWeapon[] Knifes;  // 칼 무기 배열
    [SerializeField] private Gun[] rifle2;  // 소총2 배열
    [SerializeField] private Gun[] rifle1;  // 소총1 배열
    [SerializeField] private Gun[] Tommygun;  // 톰슨기관단총 배열
    [SerializeField] private Gun[] Pistol1;  // 권총1 배열
    [SerializeField] private Gun[] Pistol2;  // 권총2 배열
    [SerializeField] private Gun[] rifle3;  // 소총3 배열

    // 각 무기 종류에 대한 딕셔너리 변수들
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

    [SerializeField] private string WeaponTypeNow;  // 현재 무기의 타입
    public static Transform WeaponNow;  // 현재 무기
    public static Animator WeaponNowAnim; // 현재 무기의 애니메이션

    // 각 무기 컨트롤러들과 관련된 변수들
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

    // 현재 무기 타입에 대한 상태 변수들
    public static bool isHand = false;
    public static bool isPistol = false;
    public static bool isRifle = false;
    public static bool isAxe = false;
    public static bool isKnife = false;
    public static bool isrifle2 = false;

    void Start()
    {
        // 각 무기 리스트 딕셔너리에 무기들을 추가
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

    // 초기 무기 변경을 위한 코루틴
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

    // 무기 변경을 위한 코루틴
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

    // 현재 무기에 대한 액션 취소
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

    // 무기 변경 함수
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