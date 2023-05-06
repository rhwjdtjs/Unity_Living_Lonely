using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class PlayFabLogin : MonoBehaviour
{
    //public static PlayFabLogin instance;
  
    public InputField ID_Input_login;
    public InputField PW_Input_login;
    public InputField ID_Input;
    public InputField PW_Input;
    public InputField Email_Input;
    public GameObject loginpanel;
    public GameObject gamestartpanel;
    public GameObject registerpanel;
    public Text usertext;
    public Text ErrorText;
    public Text ErrorText2;

    public static string username;
    public static string password;
    private string username_login;
    private string password_login;
    private string email;
    public void Start()
    {
        PlayFabSettings.TitleId = "1C256";
       
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
   
    public void ID_value_Changed()
    {
        username = ID_Input.text.ToString();
        username_login = ID_Input_login.text.ToString();
    }

    public void PW_value_Changed()
    {
        password = PW_Input.text.ToString();
        password_login = PW_Input_login.text.ToString();
    }

    public void Email_value_Changed()
    {
        email = Email_Input.text.ToString();
    }

    public void Login()
    {
        var request = new LoginWithPlayFabRequest { Username = username_login, Password = password_login };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
    }

    public void Register()
    {
        var request = new RegisterPlayFabUserRequest { Username = username, Password = password, Email = email };
        PlayFabClientAPI.RegisterPlayFabUser(request, RegisterSuccess, RegisterFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("로그인 성공");
        //
        
        ErrorText.text = "로그인 성공";
     //   ErrorText2.text = "로그인 성공";
        loginpanel.gameObject.SetActive(false);
        StartCoroutine(loginsuccessco());
        
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