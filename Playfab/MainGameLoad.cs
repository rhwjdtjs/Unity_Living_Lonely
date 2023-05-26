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
    private SaveLoad thesaveload;
    public static MainGameLoad instance;
    private string titleId = "타이틀ID";
    private string saveDataKey = "gameSaveData";
    [SerializeField] private GameObject loaderrorpanel;
    [SerializeField] private GameObject gamestartpanel;
    [SerializeField] private GameObject ifdataon;

    public void Awake()
    {
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
    public void checkboxbutton()
    {
        loaderrorpanel.gameObject.SetActive(false);
        gamestartpanel.gameObject.SetActive(true);
    }

    public void gameloadbutton()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            Keys = new List<string>() { saveDataKey },
        }, OnDataReceived, OnError);
    }

    void OnDataReceived(GetUserDataResult result)
    {
        if (result.Data.ContainsKey(saveDataKey))
        {
            string jsonData = result.Data[saveDataKey].Value;
            File.WriteAllText(Application.persistentDataPath + "/gameSaveData.json", jsonData);
            ifdataon.SetActive(true);
            Debug.Log("업로드 성공");
        }
        else
        {
            gamestartpanel.SetActive(false);
            loaderrorpanel.SetActive(true);
            Debug.Log("No saved data found in PlayFab.");
            return;
        }

        StartCoroutine(LoadCo());
    }

    void OnError(PlayFabError error)
    {
        Debug.LogError("Failed to load game save data from PlayFab: " + error.ErrorMessage);
    }

    IEnumerator LoadCo()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("gamescene");
        while (!operation.isDone)
        {
            yield return null;
        }

        thesaveload = FindObjectOfType<SaveLoad>();
        thesaveload.LoadData();
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