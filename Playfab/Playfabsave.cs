using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabSave : MonoBehaviour
{
    private string titleId = "Ÿ��ƲID"; // PlayFab Ÿ��Ʋ ID
    private string saveDataPath; // ���ÿ� ����� ���� ������ ���� ���

    void Start()
    {
        PlayFabSettings.TitleId = titleId; // PlayFab Ÿ��Ʋ ID ����
        saveDataPath = Application.persistentDataPath + "/gameSaveData.json"; // ���� ������ ���� ��� ����

        UploadSaveData(); // ���� ������ ���ε�
    }

    public void UploadSaveData()
    {
        string jsonData = File.ReadAllText(saveDataPath); // ���� ������ ���Ͽ��� ���� ������(json) �б�

        // PlayFab�� ���� ������ ���ε� ��û ����
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> { { "gameSaveData", jsonData } } // ���� �����͸� "gameSaveData"��� Ű�� ����
        };

        // PlayFab�� ���� ������ ���ε� ��û ����
        PlayFabClientAPI.UpdateUserData(request, OnDataUploaded, OnError);
    }

    void OnDataUploaded(UpdateUserDataResult result)
    {
        Debug.Log("Save data uploaded to PlayFab."); // ���� �����Ͱ� PlayFab�� ���ε�� ��� ���
    }

    void OnError(PlayFabError error)
    {
        Debug.LogError("Failed to upload save data to PlayFab: " + error.ErrorMessage); // ���� ������ ���ε� ���� �� ���� �޽��� ���
    }
}
