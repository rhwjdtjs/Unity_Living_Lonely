using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class SaveData //�����Ҷ� �����͸� �ش� �ν��Ͻ��� �ҷ���
{
    public Vector3 playerPos;
    public Vector3 playerRot;
    public List<int> invenArrayNumber = new List<int>();
    public List<string> invenItemName = new List<string>();
    public List<int> invenItemNumber = new List<int>();
    public int bulletCountrifle1;
    public int bulletCountrifle2;
    public int bulletCountrifle3;
    public int bulletCounttommy;
    public int bulletCountshotgun;
    public int bulletCountpistol1;
    public int bulletCountpistol2;
}

public class SaveLoad : MonoBehaviour
{
    public SaveData savedata = new SaveData();
    private string SAVE_DATA_DIRECTORY; //���
    private string SAVE_FILENAME = "/SaveFile.txt"; //���� �̸�
    private PlayerControllor theplayer;
    private Rifle1Contollor therifle1;
    private Rifle2Controller therifle2;
    private Rifle3Controllor therifle3;
    private Pistol1Controllor thepistol1;
    private Pistol2Cpmtrpllor thepistol2;
    private TommygunControllor thetommy;
    private ShotgunController theshotgun; //�ʿ��� ������Ʈ
    private Inventory theinven;
   
    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/"; //���
        if (!Directory.Exists(SAVE_DATA_DIRECTORY)) //���� ��ΰ� �������� ������
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY); //��� ����
    }

    public void SaveData()
    {
        theplayer = FindObjectOfType<PlayerControllor>();
        theinven = FindObjectOfType<Inventory>();
       // thegunmain = FindObjectOfType<GunMainController>();
        savedata.playerPos = theplayer.transform.position;
        savedata.playerRot = theplayer.transform.eulerAngles;
      //  savedata.bulletCount = thegunmain.Returnbulletcount();
        Slot[] slots= theinven.GetSlots();
        for (int i = 0; i < slots.Length; i++) //�κ��丮�� ������ ����
        {
            if(slots[i].item!=null)
            {
                savedata.invenArrayNumber.Add(i);
                savedata.invenItemName.Add(slots[i].item.itemName);
                savedata.invenItemNumber.Add(slots[i].itemCount);
            }
        }
        string json = JsonUtility.ToJson(savedata);
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);
        Debug.Log("����Ϸ�");
        Debug.Log("���� ������ �Ѿ˼�: "+savedata.bulletCountrifle1);
        Debug.Log("���� ������ �Ѿ˼�: " + savedata.bulletCountrifle2);
        Debug.Log("���� ������ �Ѿ˼�: " + savedata.bulletCountrifle3);
        Debug.Log(json);
    }
    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME)) //���̺� ������ ������ �ε� ����
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            savedata = JsonUtility.FromJson<SaveData>(loadJson);
            theplayer = FindObjectOfType<PlayerControllor>();
            theinven = FindObjectOfType<Inventory>();
            therifle1 = FindObjectOfType<Rifle1Contollor>();
            therifle2 = FindObjectOfType<Rifle2Controller>();
            therifle3 = FindObjectOfType<Rifle3Controllor>();
            thepistol1 = FindObjectOfType<Pistol1Controllor>();
            thepistol2 = FindObjectOfType<Pistol2Cpmtrpllor>();
            thetommy = FindObjectOfType<TommygunControllor>();
            theshotgun = FindObjectOfType<ShotgunController>();
            theplayer.transform.position = savedata.playerPos;
            theplayer.transform.eulerAngles = savedata.playerRot;
            therifle1.currentGun.currentBulletCount = savedata.bulletCountrifle1;
            therifle2.currentGun.currentBulletCount = savedata.bulletCountrifle2;
            therifle3.currentGun.currentBulletCount = savedata.bulletCountrifle3;
            thepistol1.currentGun.currentBulletCount = savedata.bulletCountpistol1;
            thepistol2.currentGun.currentBulletCount = savedata.bulletCountpistol2;
            thetommy.currentGun.currentBulletCount = savedata.bulletCounttommy;
            theshotgun.currentGun.currentBulletCount = savedata.bulletCountshotgun;
            for (int i = 0; i < savedata.invenItemName.Count; i++)
            {
                theinven.LoadToInven(savedata.invenArrayNumber[i], savedata.invenItemName[i], savedata.invenItemNumber[i]);
            }
            Debug.Log("�ε�Ϸ�");
        }
        else
            Debug.Log("���̺� ������ �����ϴ�,");
    }
}
