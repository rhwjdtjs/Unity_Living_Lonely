using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    // ������ �����͸� �����ϴ� Ŭ�����Դϴ�.
    public Vector3 playerPos; // �÷��̾� ��ġ
    public Vector3 playerRot; // �÷��̾� ȸ����
    public List<int> invenArrayNumber = new List<int>(); // �κ��丮 ���� �ε���
    public List<string> invenItemName = new List<string>(); // ������ �̸�
    public List<int> invenItemNumber = new List<int>(); // ������ ����
    public int bulletCountrifle1; // ������1�� ������ �Ѿ� ��
    public int bulletCountrifle2; // ������2�� ������ �Ѿ� ��
    public int bulletCountrifle3; // ������3�� ������ �Ѿ� ��
    public int bulletCounttommy; // ��̰��� ������ �Ѿ� ��
    public int bulletCountshotgun; // ������ ������ �Ѿ� ��
    public int bulletCountpistol1; // ����1�� ������ �Ѿ� ��
    public int bulletCountpistol2; // ����2�� ������ �Ѿ� ��
}

public class SaveLoad : MonoBehaviour
{
    public SaveData savedata = new SaveData();
    private string SAVE_DATA_DIRECTORY; // ������ ���� ���
    private string SAVE_FILENAME = "/gameSaveData.json"; // ���� ���� �̸�
    private PlayerControllor theplayer; // �÷��̾� ��Ʈ�ѷ�
    private Rifle1Contollor therifle1; // ������1 ��Ʈ�ѷ�
    private Rifle2Controller therifle2; // ������2 ��Ʈ�ѷ�
    private Rifle3Controllor therifle3; // ������3 ��Ʈ�ѷ�
    private Pistol1Controllor thepistol1; // ����1 ��Ʈ�ѷ�
    private Pistol2Cpmtrpllor thepistol2; // ����2 ��Ʈ�ѷ�
    private TommygunControllor thetommy; // ��̰� ��Ʈ�ѷ�
    private ShotgunController theshotgun; // ���� ��Ʈ�ѷ�
    private Inventory theinven; // �κ��丮

    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.persistentDataPath; // ������ ���� ��� ����
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

        savedata.playerPos = theplayer.transform.position; // �÷��̾� ��ġ ����
        savedata.playerRot = theplayer.transform.eulerAngles; // �÷��̾� ȸ���� ����
        savedata.bulletCountrifle1 = therifle1.currentGun.currentBulletCount; // ������1�� ������ �Ѿ� �� ����
        savedata.bulletCountrifle2 = therifle2.currentGun.currentBulletCount; // ������2�� ������ �Ѿ� �� ����
        savedata.bulletCountrifle3 = therifle3.currentGun.currentBulletCount; // ������3�� ������ �Ѿ� �� ����
        savedata.bulletCounttommy = thetommy.currentGun.currentBulletCount; // ��̰��� ������ �Ѿ� �� ����
        savedata.bulletCountshotgun = theshotgun.currentGun.currentBulletCount; // ������ ������ �Ѿ� �� ����
        savedata.bulletCountpistol1 = thepistol1.currentGun.currentBulletCount; // ����1�� ������ �Ѿ� �� ����
        savedata.bulletCountpistol2 = thepistol2.currentGun.currentBulletCount; // ����2�� ������ �Ѿ� �� ����

        Slot[] slots = theinven.GetSlots();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                savedata.invenArrayNumber.Add(i); // �κ��丮 ���� �ε��� ����
                savedata.invenItemName.Add(slots[i].item.itemName); // ������ �̸� ����
                savedata.invenItemNumber.Add(slots[i].itemCount); // ������ ���� ����
            }
        }

        string json = JsonUtility.ToJson(savedata); // SaveData�� JSON �������� ��ȯ
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json); // JSON ���Ϸ� ����
        Debug.Log("����Ϸ�");
        Debug.Log("���� ������ �Ѿ˼�: " + savedata.bulletCountrifle1);
        Debug.Log("���� ������ �Ѿ˼�: " + savedata.bulletCountrifle2);
        Debug.Log("���� ������ �Ѿ˼�: " + savedata.bulletCountrifle3);
        Debug.Log(json);
    }

    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME); // ����� JSON ���� �б�
            savedata = JsonUtility.FromJson<SaveData>(loadJson); // JSON�� SaveData�� ��ȯ

            theplayer = FindObjectOfType<PlayerControllor>();
            theinven = FindObjectOfType<Inventory>();
            therifle1 = FindObjectOfType<Rifle1Contollor>();
            therifle2 = FindObjectOfType<Rifle2Controller>();
            therifle3 = FindObjectOfType<Rifle3Controllor>();
            thepistol1 = FindObjectOfType<Pistol1Controllor>();
            thepistol2 = FindObjectOfType<Pistol2Cpmtrpllor>();
            thetommy = FindObjectOfType<TommygunControllor>();
            theshotgun = FindObjectOfType<ShotgunController>();

            theplayer.transform.position = savedata.playerPos; // ����� �÷��̾� ��ġ�� ����
            theplayer.transform.eulerAngles = savedata.playerRot; // ����� �÷��̾� ȸ�������� ����
            therifle1.currentGun.currentBulletCount = savedata.bulletCountrifle1; // ����� ������1�� ������ �Ѿ� ���� ����
            therifle2.currentGun.currentBulletCount = savedata.bulletCountrifle2; // ����� ������2�� ������ �Ѿ� ���� ����
            therifle3.currentGun.currentBulletCount = savedata.bulletCountrifle3; // ����� ������3�� ������ �Ѿ� ���� ����
            thepistol1.currentGun.currentBulletCount = savedata.bulletCountpistol1; // ����� ����1�� ������ �Ѿ� ���� ����
            thepistol2.currentGun.currentBulletCount = savedata.bulletCountpistol2; // ����� ����2�� ������ �Ѿ� ���� ����
            thetommy.currentGun.currentBulletCount = savedata.bulletCounttommy; // ����� ��̰��� ������ �Ѿ� ���� ����
            theshotgun.currentGun.currentBulletCount = savedata.bulletCountshotgun; // ����� ������ ������ �Ѿ� ���� ����

            for (int i = 0; i < savedata.invenItemName.Count; i++)
            {
                theinven.LoadToInven(savedata.invenArrayNumber[i], savedata.invenItemName[i], savedata.invenItemNumber[i]); // �κ��丮 ������ �ε�
            }

            Debug.Log("�ε�Ϸ�");
        }
        else
        {
            Debug.Log("���̺� ������ �����ϴ�.");
        }
    }
}
