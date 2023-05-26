using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System;

public class PlayFabLogin : MonoBehaviour
{
    public InputField ID_Input_login; // �α��� �� �Է¹޴� ���̵� �ʵ�
    public InputField PW_Input_login; // �α��� �� �Է¹޴� ��й�ȣ �ʵ�
    public InputField ID_Input; // ȸ������ �� �Է¹޴� ���̵� �ʵ�
    public InputField PW_Input; // ȸ������ �� �Է¹޴� ��й�ȣ �ʵ�
    public InputField Email_Input; // ȸ������ �� �Է¹޴� �̸��� �ʵ�
    public GameObject loginpanel; // �α��� �г�
    public GameObject gamestartpanel; // ���� ���� �г�
    public GameObject registerpanel; // ȸ������ �г�
    public Text usertext; // ȯ�� �޽����� ǥ���ϴ� �ؽ�Ʈ
    public Text ErrorText; // ���� �޽����� ǥ���ϴ� �ؽ�Ʈ
    public Text ErrorText2; // ���� �޽����� ǥ���ϴ� �ؽ�Ʈ

    public static string username; // �Է¹��� ���̵� �����ϴ� ����
    public static string password; // �Է¹��� ��й�ȣ�� �����ϴ� ����
    private string username_login; // �α��� �� �Է¹��� ���̵� �����ϴ� ����
    private string password_login; // �α��� �� �Է¹��� ��й�ȣ�� �����ϴ� ����
    private string email; // �Է¹��� �̸����� �����ϴ� ����

    public void Start()
    {
        PlayFabSettings.TitleId = "Ÿ��ƲID";
       
    }
    private void Update()
    {
        ID_value_Changed();
        PW_value_Changed();
        Email_value_Changed();
    }
    public void registerbutton()
    {
        registerpanel.gameObject.SetActive(true);
        loginpanel.gameObject.SetActive(false);
    }
    public void CancelButton()
    {
        registerpanel.gameObject.SetActive(false);
        loginpanel.gameObject.SetActive(true);
    }
    // Use this for initialization
    public void Startbuton()
    {
        loginpanel.gameObject.SetActive(true);
    }
    public void quitbutton()
    {
        Application.Quit();
    }
    public void gamestartbutton()
    {
        LoadingSceneManager.LoadScene("gamescene");
    }
    // ���̵� �ʵ� ���� ����Ǿ��� �� ȣ��Ǵ� �Լ�
    public void ID_value_Changed()
    {
        username = ID_Input.text.ToString();
        username_login = ID_Input_login.text.ToString();
    }
    // ��й�ȣ �ʵ� ���� ����Ǿ��� �� ȣ��Ǵ� �Լ�
    public void PW_value_Changed()
    {
        password = PW_Input.text.ToString();
        password_login = PW_Input_login.text.ToString();
    }
    // �̸��� �ʵ� ���� ����Ǿ��� �� ȣ��Ǵ� �Լ�
    public void Email_value_Changed()
    {
        email = Email_Input.text.ToString();
    }
    // �α��� ��ư Ŭ�� �� ����Ǵ� �Լ�
    public void Login()
    {
        var request = new LoginWithPlayFabRequest { Username = username_login, Password = password_login };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
    }
    // ȸ������ ��ư Ŭ�� �� ����Ǵ� �Լ�
    public void Register()
    {
        var request = new RegisterPlayFabUserRequest { Username = username, Password = password, Email = email };
        PlayFabClientAPI.RegisterPlayFabUser(request, RegisterSuccess, RegisterFailure);
    }
    // �α��� ���� �� ȣ��Ǵ� �ݹ� �Լ�
    private void OnLoginSuccess(LoginResult result)
    {
        // �������� �޾ƿ�
        var request = new GetAccountInfoRequest { Username = username_login.ToString() };
        PlayFabClientAPI.GetAccountInfo(request, GetAccountSuccess,OnGetAccountInfoFailure);
       
        PhotonNetwork.AuthValues = new Photon.Realtime.AuthenticationValues();
                    PhotonNetwork.AuthValues.UserId = result.PlayFabId;
                    PhotonNetwork.ConnectUsingSettings(); // ���濡 �α���
                    Debug.Log("�÷�����, ���� �α��� ����");

                    ErrorText.text = "�α��� ����";
                    loginpanel.gameObject.SetActive(false);
                    StartCoroutine(loginsuccessco());
             
            
         
    }
    // ȸ������ ��ȸ ���� �� ȣ��Ǵ� �ݹ� �Լ�
    private void GetAccountSuccess(GetAccountInfoResult result)
    {
        Debug.Log("�����");
        // playfab ���� ���ӵǾ����� Ȯ���Ͽ� �Ǹ� ����. �ƴϸ� �����޼��� ���
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = username_login.ToString() };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, DisplayNameUpdateSuccess, DisplayNameUpdateFailure);

    }
  

    private void DisplayNameUpdateFailure(PlayFabError obj)
    {
        throw new NotImplementedException();
    }

    private void DisplayNameUpdateSuccess(UpdateUserTitleDisplayNameResult obj)
    {
        throw new NotImplementedException();
    }

    
    

    private void OnUpdateDisplayNameFailure(PlayFabError error)
    {
        Debug.LogError("Display Name ������Ʈ ����: " + error.GenerateErrorReport());
    }
    private void OnGetAccountInfoFailure(PlayFabError error)
    {
        Debug.LogError("Failed to get account info: " + error.GenerateErrorReport());
    }
    IEnumerator loginsuccessco()
    {
        yield return new WaitForSeconds(1.5f);
        gamestartpanel.gameObject.SetActive(true);
        usertext.text = "Welcome!\n"+username_login;
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("�α��� ����");
        Debug.Log(password_login);
        Debug.LogWarning(error.GenerateErrorReport());
        ErrorText.text = error.GenerateErrorReport();
      //  ErrorText2.text = error.GenerateErrorReport();
    }

    private void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("���� ����");
      //  ErrorText.text = "���� ����";
        ErrorText2.text = "���� ����";
        StartCoroutine(registerco());
    }
    IEnumerator registerco()
    {
        yield return new WaitForSeconds(1.5f);
        registerpanel.gameObject.SetActive(false);
        loginpanel.gameObject.SetActive(true);
    }

    private void RegisterFailure(PlayFabError error)
    {
        Debug.LogWarning("���� ����");
        Debug.Log(password);
        Debug.LogWarning(error.GenerateErrorReport());
       // ErrorText.text = error.GenerateErrorReport();
        ErrorText2.text = error.GenerateErrorReport();
    }
}