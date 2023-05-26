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
    public InputField ID_Input_login; // 로그인 시 입력받는 아이디 필드
    public InputField PW_Input_login; // 로그인 시 입력받는 비밀번호 필드
    public InputField ID_Input; // 회원가입 시 입력받는 아이디 필드
    public InputField PW_Input; // 회원가입 시 입력받는 비밀번호 필드
    public InputField Email_Input; // 회원가입 시 입력받는 이메일 필드
    public GameObject loginpanel; // 로그인 패널
    public GameObject gamestartpanel; // 게임 시작 패널
    public GameObject registerpanel; // 회원가입 패널
    public Text usertext; // 환영 메시지를 표시하는 텍스트
    public Text ErrorText; // 에러 메시지를 표시하는 텍스트
    public Text ErrorText2; // 에러 메시지를 표시하는 텍스트

    public static string username; // 입력받은 아이디를 저장하는 변수
    public static string password; // 입력받은 비밀번호를 저장하는 변수
    private string username_login; // 로그인 시 입력받은 아이디를 저장하는 변수
    private string password_login; // 로그인 시 입력받은 비밀번호를 저장하는 변수
    private string email; // 입력받은 이메일을 저장하는 변수

    public void Start()
    {
        PlayFabSettings.TitleId = "타이틀ID";
       
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
    // 아이디 필드 값이 변경되었을 때 호출되는 함수
    public void ID_value_Changed()
    {
        username = ID_Input.text.ToString();
        username_login = ID_Input_login.text.ToString();
    }
    // 비밀번호 필드 값이 변경되었을 때 호출되는 함수
    public void PW_value_Changed()
    {
        password = PW_Input.text.ToString();
        password_login = PW_Input_login.text.ToString();
    }
    // 이메일 필드 값이 변경되었을 때 호출되는 함수
    public void Email_value_Changed()
    {
        email = Email_Input.text.ToString();
    }
    // 로그인 버튼 클릭 시 실행되는 함수
    public void Login()
    {
        var request = new LoginWithPlayFabRequest { Username = username_login, Password = password_login };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
    }
    // 회원가입 버튼 클릭 시 실행되는 함수
    public void Register()
    {
        var request = new RegisterPlayFabUserRequest { Username = username, Password = password, Email = email };
        PlayFabClientAPI.RegisterPlayFabUser(request, RegisterSuccess, RegisterFailure);
    }
    // 로그인 성공 시 호출되는 콜백 함수
    private void OnLoginSuccess(LoginResult result)
    {
        // 계정정보 받아옴
        var request = new GetAccountInfoRequest { Username = username_login.ToString() };
        PlayFabClientAPI.GetAccountInfo(request, GetAccountSuccess,OnGetAccountInfoFailure);
       
        PhotonNetwork.AuthValues = new Photon.Realtime.AuthenticationValues();
                    PhotonNetwork.AuthValues.UserId = result.PlayFabId;
                    PhotonNetwork.ConnectUsingSettings(); // 포톤에 로그인
                    Debug.Log("플레이팹, 포톤 로그인 성공");

                    ErrorText.text = "로그인 성공";
                    loginpanel.gameObject.SetActive(false);
                    StartCoroutine(loginsuccessco());
             
            
         
    }
    // 회원정보 조회 성공 시 호출되는 콜백 함수
    private void GetAccountSuccess(GetAccountInfoResult result)
    {
        Debug.Log("적용됨");
        // playfab 서버 접속되었는지 확인하여 되면 실행. 아니면 에러메세지 출력
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
        Debug.LogError("Display Name 업데이트 실패: " + error.GenerateErrorReport());
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
        Debug.LogWarning("로그인 실패");
        Debug.Log(password_login);
        Debug.LogWarning(error.GenerateErrorReport());
        ErrorText.text = error.GenerateErrorReport();
      //  ErrorText2.text = error.GenerateErrorReport();
    }

    private void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("가입 성공");
      //  ErrorText.text = "가입 성공";
        ErrorText2.text = "가입 성공";
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
        Debug.LogWarning("가입 실패");
        Debug.Log(password);
        Debug.LogWarning(error.GenerateErrorReport());
       // ErrorText.text = error.GenerateErrorReport();
        ErrorText2.text = error.GenerateErrorReport();
    }
}