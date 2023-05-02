using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using PlayFab;
using PlayFab.ClientModels;
public class Playfabsave : MonoBehaviour
{
    private string titleId = "1C256";
    private string saveDataPath = "Assets/Saves/SaveFile.txt";

    void Start()
    {
        PlayFabSettings.TitleId = titleId;
        UploadSaveData();
    }

    public void UploadSaveData()
    {
        string jsonData = File.ReadAllText(saveDataPath);
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> { { "gameSaveData", jsonData } }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataUploaded, OnError);
    }

    void OnDataUploaded(UpdateUserDataResult result)
    {
        Debug.Log("Save data uploaded to PlayFab.");
    }

    void OnError(PlayFabError error)
    {
        Debug.LogError("Failed to upload save data to PlayFab: " + error.ErrorMessage);
    }
}
