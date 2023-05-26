using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class MainGameLoad : MonoBehaviour
{
    private SaveLoad theSaveLoad;
    public static MainGameLoad instance;
    private string titleId = "타이틀ID";
    private string saveDataKey = "gameSaveData";
    [SerializeField] private GameObject loadErrorPanel;
    [SerializeField] private GameObject gameStartPanel;
    [SerializeField] private GameObject ifDataOn;

    public void Awake()
    {
        // 단일 인스턴스 유지를 위해 객체를 유지하고 다른 씬으로 전환해도 파괴되지 않도록 설정합니다.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 체크박스 버튼 클릭 시 실행되는 함수
    public void CheckboxButton()
    {
        loadErrorPanel.gameObject.SetActive(false);
        gameStartPanel.gameObject.SetActive(true);
    }

    // 게임 로드 버튼 클릭 시 실행되는 함수
    public void GameLoadButton()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            Keys = new List<string>() { saveDataKey },
        }, OnDataReceived, OnError);
    }

    // PlayFab에서 사용자 데이터를 가져온 후 실행되는 콜백 함수
    void OnDataReceived(GetUserDataResult result)
    {
        if (result.Data.ContainsKey(saveDataKey))
        {
            string jsonData = result.Data[saveDataKey].Value;
            File.WriteAllText(Application.persistentDataPath + "/gameSaveData.json", jsonData);
            ifDataOn.SetActive(true);
            Debug.Log("업로드 성공");
        }
        else
        {
            gameStartPanel.SetActive(false);
            loadErrorPanel.SetActive(true);
            Debug.Log("No saved data found in PlayFab.");
            return;
        }

        StartCoroutine(LoadCo());
    }

    // PlayFab에서 데이터를 가져오지 못했을 때 실행되는 콜백 함수
    void OnError(PlayFabError error)
    {
        Debug.LogError("Failed to load game save data from PlayFab: " + error.ErrorMessage);
    }

    // 게임 씬을 비동기적으로 로드하는 코루틴 함수
    IEnumerator LoadCo()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("gamescene");
        while (!operation.isDone)
        {
            yield return null;
        }

        theSaveLoad = FindObjectOfType<SaveLoad>();
        theSaveLoad.LoadData();
        Debug.Log("게임 로드 성공");

        Destroy(gameObject);
    }

    void Start()
    {
        PlayFabSettings.TitleId = titleId;
    }

    void Update()
    {

    }
}
