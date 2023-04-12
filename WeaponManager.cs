using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static bool isChangeWeapon = false;  

    [SerializeField] private float ChangeDelay;  
    [SerializeField] private float ChangeEndDelay;  
    [SerializeField] private Gun[] guns;  
    [SerializeField] private MeleeWeapon[] hands;  
    [SerializeField]private MeleeWeapon[] axes; 
    [SerializeField]private MeleeWeapon[] Knifes;
    [SerializeField] private Gun[] rifle2;
    [SerializeField] private Gun[] rifle1;
    [SerializeField] private Gun[] Tommygun;
    [SerializeField] private Gun[] Pistol1;
    [SerializeField] private Gun[] Pistol2;
    [SerializeField] private Gun[] rifle3;
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

    [SerializeField]private string WeaponTypeNow;  // 현재 무기의 타입
    public static Transform WeaponNow;  // 현재 무기
    public static Animator WeaponNowAnim; // 현재 무기의 애니메이션
    [SerializeField] private GunMainController thegunmain;
    [SerializeField]private HandControllor theHandController;
    [SerializeField]private AxeControllor theAxeController; 
    [SerializeField]private KnifeControllor theKnifeControllor;
    [SerializeField] private Rifle2Controller therifle2Controllor;
    [SerializeField] private Rifle1Contollor therifle1controllor;
    [SerializeField] private TommygunControllor thetommygunControllor;
    [SerializeField] private Rifle3Controllor therifle3controllor;
    [SerializeField] private Pistol1Controllor thepistol1controllor;
    [SerializeField] private Pistol2Cpmtrpllor thepistol2controllor;
    [SerializeField] private ShotgunController theshotgun;
    public static bool isHand = false;
    public static bool isPistol = false;
    public static bool isRifle = false;
    public static bool isAxe = false;
    public static bool isKnife = false;
    public static bool isrifle2 = false;
    void Start()
    {
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
    public IEnumerator CHANGEWEAPONCO(string _type, string _name)
    {
       // theGunController.AmmoItemRifill();
        isChangeWeapon = true;
        WeaponNowAnim.SetTrigger("Weapon_Out");

        yield return new WaitForSeconds(ChangeDelay);

        CancelActions();
        ChangeWeapon(_type, _name);

        yield return new WaitForSeconds(ChangeEndDelay);

        WeaponTypeNow = _type;
        isChangeWeapon = false;
    }

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
            case "HAND":
                HandControllor.isActivate = false;
                break;
            case "AXE":
                AxeControllor.isActivate = false;
                break;
            case "KNIFE":
                KnifeControllor.isActivate = false;
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
        }
    }

    private void ChangeWeapon(string _type, string _name)
    {
       
         if (_type == "HAND")
        {
            theHandController.MeleeChangeWeapon(HandList[_name]);
            isRifle = false;
        }
        else if (_type == "AXE")
        {
            theAxeController.MeleeChangeWeapon(AxeList[_name]);
            isRifle = false;
        }
        else if (_type == "KNIFE")
        {
            theKnifeControllor.MeleeChangeWeapon(KnifeList[_name]);
            isRifle = false;
        }
        else if (_type == "RIFLE1")
        {
            therifle1controllor.GunChange(Rifle1List[_name]);
            isRifle = false;
        }
        else if (_type == "RIFLE2")
        {
            therifle2Controllor.GunChange(Rifle2List[_name]);
            isRifle = false;
        }
        else if (_type == "TOMMYGUN")
        {
            thetommygunControllor.GunChange(tommygunList[_name]);
            isRifle = false;
        }
        else if (_type == "RIFLE3")
        {
            therifle3controllor.GunChange(Rifle3List[_name]);
            isRifle = false;
        }
        else if (_type == "PISTOL1")
        {
            thepistol1controllor.GunChange(Pistol1List[_name]);
            isRifle = false;
        }
        else if (_type == "PISTOL2")
        {
            thepistol2controllor.GunChange(Pistol2List[_name]);
            isRifle = false;
        }
        else if (_type == "Shotgun")
        {
            theshotgun.GunChange(GunList[_name]);
            isRifle = false;
        }
    }
}