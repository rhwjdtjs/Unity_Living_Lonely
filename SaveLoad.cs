using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    // 저장할 데이터를 정의하는 클래스입니다.
    public Vector3 playerPos; // 플레이어 위치
    public Vector3 playerRot; // 플레이어 회전값
    public List<int> invenArrayNumber = new List<int>(); // 인벤토리 슬롯 인덱스
    public List<string> invenItemName = new List<string>(); // 아이템 이름
    public List<int> invenItemNumber = new List<int>(); // 아이템 개수
    public int bulletCountrifle1; // 라이플1의 장전된 총알 수
    public int bulletCountrifle2; // 라이플2의 장전된 총알 수
    public int bulletCountrifle3; // 라이플3의 장전된 총알 수
    public int bulletCounttommy; // 토미건의 장전된 총알 수
    public int bulletCountshotgun; // 샷건의 장전된 총알 수
    public int bulletCountpistol1; // 권총1의 장전된 총알 수
    public int bulletCountpistol2; // 권총2의 장전된 총알 수
}

public class SaveLoad : MonoBehaviour
{
    public SaveData savedata = new SaveData();
    private string SAVE_DATA_DIRECTORY; // 데이터 저장 경로
    private string SAVE_FILENAME = "/gameSaveData.json"; // 저장 파일 이름
    private PlayerControllor theplayer; // 플레이어 컨트롤러
    private Rifle1Contollor therifle1; // 라이플1 컨트롤러
    private Rifle2Controller therifle2; // 라이플2 컨트롤러
    private Rifle3Controllor therifle3; // 라이플3 컨트롤러
    private Pistol1Controllor thepistol1; // 권총1 컨트롤러
    private Pistol2Cpmtrpllor thepistol2; // 권총2 컨트롤러
    private TommygunControllor thetommy; // 토미건 컨트롤러
    private ShotgunController theshotgun; // 샷건 컨트롤러
    private Inventory theinven; // 인벤토리

    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.persistentDataPath; // 데이터 저장 경로 설정
        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
    }

    public void SaveData()
    {
        theplayer = FindObjectOfType<PlayerControllor>();
        theinven = FindObjectOfType<Inventory>();
        therifle1 = FindObjectOfType<Rifle1Contollor>();
        therifle2 = FindObjectOfType<Rifle2Controller>();
        therifle3 = FindObjectOfType<Rifle3Controllor>();
        thepistol1 = FindObjectOfType<Pistol1Controllor>();
        thepistol2 = FindObjectOfType<Pistol2Cpmtrpllor>();
        thetommy = FindObjectOfType<TommygunControllor>();
        theshotgun = FindObjectOfType<ShotgunController>();

        savedata.playerPos = theplayer.transform.position; // 플레이어 위치 저장
        savedata.playerRot = theplayer.transform.eulerAngles; // 플레이어 회전값 저장
        savedata.bulletCountrifle1 = therifle1.currentGun.currentBulletCount; // 라이플1의 장전된 총알 수 저장
        savedata.bulletCountrifle2 = therifle2.currentGun.currentBulletCount; // 라이플2의 장전된 총알 수 저장
        savedata.bulletCountrifle3 = therifle3.currentGun.currentBulletCount; // 라이플3의 장전된 총알 수 저장
        savedata.bulletCounttommy = thetommy.currentGun.currentBulletCount; // 토미건의 장전된 총알 수 저장
        savedata.bulletCountshotgun = theshotgun.currentGun.currentBulletCount; // 샷건의 장전된 총알 수 저장
        savedata.bulletCountpistol1 = thepistol1.currentGun.currentBulletCount; // 권총1의 장전된 총알 수 저장
        savedata.bulletCountpistol2 = thepistol2.currentGun.currentBulletCount; // 권총2의 장전된 총알 수 저장

        Slot[] slots = theinven.GetSlots();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                savedata.invenArrayNumber.Add(i); // 인벤토리 슬롯 인덱스 저장
                savedata.invenItemName.Add(slots[i].item.itemName); // 아이템 이름 저장
                savedata.invenItemNumber.Add(slots[i].itemCount); // 아이템 개수 저장
            }
        }

        string json = JsonUtility.ToJson(savedata); // SaveData를 JSON 형식으로 변환
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json); // JSON 파일로 저장
        Debug.Log("저장완료");
        Debug.Log("현재 장전된 총알수: " + savedata.bulletCountrifle1);
        Debug.Log("현재 장전된 총알수: " + savedata.bulletCountrifle2);
        Debug.Log("현재 장전된 총알수: " + savedata.bulletCountrifle3);
        Debug.Log(json);
    }

    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME); // 저장된 JSON 파일 읽기
            savedata = JsonUtility.FromJson<SaveData>(loadJson); // JSON을 SaveData로 변환

            theplayer = FindObjectOfType<PlayerControllor>();
            theinven = FindObjectOfType<Inventory>();
            therifle1 = FindObjectOfType<Rifle1Contollor>();
            therifle2 = FindObjectOfType<Rifle2Controller>();
            therifle3 = FindObjectOfType<Rifle3Controllor>();
            thepistol1 = FindObjectOfType<Pistol1Controllor>();
            thepistol2 = FindObjectOfType<Pistol2Cpmtrpllor>();
            thetommy = FindObjectOfType<TommygunControllor>();
            theshotgun = FindObjectOfType<ShotgunController>();

            theplayer.transform.position = savedata.playerPos; // 저장된 플레이어 위치로 설정
            theplayer.transform.eulerAngles = savedata.playerRot; // 저장된 플레이어 회전값으로 설정
            therifle1.currentGun.currentBulletCount = savedata.bulletCountrifle1; // 저장된 라이플1의 장전된 총알 수로 설정
            therifle2.currentGun.currentBulletCount = savedata.bulletCountrifle2; // 저장된 라이플2의 장전된 총알 수로 설정
            therifle3.currentGun.currentBulletCount = savedata.bulletCountrifle3; // 저장된 라이플3의 장전된 총알 수로 설정
            thepistol1.currentGun.currentBulletCount = savedata.bulletCountpistol1; // 저장된 권총1의 장전된 총알 수로 설정
            thepistol2.currentGun.currentBulletCount = savedata.bulletCountpistol2; // 저장된 권총2의 장전된 총알 수로 설정
            thetommy.currentGun.currentBulletCount = savedata.bulletCounttommy; // 저장된 토미건의 장전된 총알 수로 설정
            theshotgun.currentGun.currentBulletCount = savedata.bulletCountshotgun; // 저장된 샷건의 장전된 총알 수로 설정

            for (int i = 0; i < savedata.invenItemName.Count; i++)
            {
                theinven.LoadToInven(savedata.invenArrayNumber[i], savedata.invenItemName[i], savedata.invenItemNumber[i]); // 인벤토리 아이템 로드
            }

            Debug.Log("로드완료");
        }
        else
        {
            Debug.Log("세이브 파일이 없습니다.");
        }
    }
}
