using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabSave : MonoBehaviour
{
    private string titleId = "타이틀ID"; // PlayFab 타이틀 ID
    private string saveDataPath; // 로컬에 저장된 게임 데이터 파일 경로

    void Start()
    {
        PlayFabSettings.TitleId = titleId; // PlayFab 타이틀 ID 설정
        saveDataPath = Application.persistentDataPath + "/gameSaveData.json"; // 로컬 데이터 파일 경로 설정

        UploadSaveData(); // 게임 데이터 업로드
    }

    public void UploadSaveData()
    {
        string jsonData = File.ReadAllText(saveDataPath); // 로컬 데이터 파일에서 게임 데이터(json) 읽기

        // PlayFab에 게임 데이터 업로드 요청 생성
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> { { "gameSaveData", jsonData } } // 게임 데이터를 "gameSaveData"라는 키로 설정
        };

        // PlayFab에 게임 데이터 업로드 요청 전송
        PlayFabClientAPI.UpdateUserData(request, OnDataUploaded, OnError);
    }

    void OnDataUploaded(UpdateUserDataResult result)
    {
        Debug.Log("Save data uploaded to PlayFab."); // 게임 데이터가 PlayFab에 업로드된 경우 출력
    }

    void OnError(PlayFabError error)
    {
        Debug.LogError("Failed to upload save data to PlayFab: " + error.ErrorMessage); // 게임 데이터 업로드 실패 시 에러 메시지 출력
    }
}
