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
    private string titleId = "Ÿ��ƲID";
    private string saveDataKey = "gameSaveData";
    [SerializeField] private GameObject loadErrorPanel;
    [SerializeField] private GameObject gameStartPanel;
    [SerializeField] private GameObject ifDataOn;

    public void Awake()
    {
        // ���� �ν��Ͻ� ������ ���� ��ü�� �����ϰ� �ٸ� ������ ��ȯ�ص� �ı����� �ʵ��� �����մϴ�.
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

    // üũ�ڽ� ��ư Ŭ�� �� ����Ǵ� �Լ�
    public void CheckboxButton()
    {
        loadErrorPanel.gameObject.SetActive(false);
        gameStartPanel.gameObject.SetActive(true);
    }

    // ���� �ε� ��ư Ŭ�� �� ����Ǵ� �Լ�
    public void GameLoadButton()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            Keys = new List<string>() { saveDataKey },
        }, OnDataReceived, OnError);
    }

    // PlayFab���� ����� �����͸� ������ �� ����Ǵ� �ݹ� �Լ�
    void OnDataReceived(GetUserDataResult result)
    {
        if (result.Data.ContainsKey(saveDataKey))
        {
            string jsonData = result.Data[saveDataKey].Value;
            File.WriteAllText(Application.persistentDataPath + "/gameSaveData.json", jsonData);
            ifDataOn.SetActive(true);
            Debug.Log("���ε� ����");
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

    // PlayFab���� �����͸� �������� ������ �� ����Ǵ� �ݹ� �Լ�
    void OnError(PlayFabError error)
    {
        Debug.LogError("Failed to load game save data from PlayFab: " + error.ErrorMessage);
    }

    // ���� ���� �񵿱������� �ε��ϴ� �ڷ�ƾ �Լ�
    IEnumerator LoadCo()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("gamescene");
        while (!operation.isDone)
        {
            yield return null;
        }

        theSaveLoad = FindObjectOfType<SaveLoad>();
        theSaveLoad.LoadData();
        Debug.Log("���� �ε� ����");

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
