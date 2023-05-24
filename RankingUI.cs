using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class RankingUI : MonoBehaviour
{
    public GameObject rankingPanel;
    public Text killCountText;
    public Text survivalTimeText;
    string countryCode = "";
    string displayName = "";
    [SerializeField] GameObject loginpanel;
    private RealtimeRankingSystem therealtime;
    private void Update()
    {
        

    }
    
   
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        therealtime = FindObjectOfType<RealtimeRankingSystem>();
        rankingPanel.SetActive(false); // �ʱ⿡ ��ŷ �г� ��Ȱ��ȭ
       
    }

    public void ShowRanking()
    {
        
        rankingPanel.SetActive(true); // ��ŷ �г� Ȱ��ȭ
        loginpanel.SetActive(false);
        GetPlayerId();
        GetLeaderboard();
        //UpdateKillCountUI();

    }

    public void HideRanking()
    {
        loginpanel.SetActive(true);
        rankingPanel.SetActive(false); // ��ŷ �г� ��Ȱ��ȭ
    }
    public void GetLeaderboard()
    {
        var killCountRequest = new GetLeaderboardRequest
        {
            StatisticName = "KillCount",
            StartPosition = 0,
            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboard(killCountRequest, OnGetKillCountLeaderboardSuccess, OnGetLeaderboardFailure);

        var survivalTimeRequest = new GetLeaderboardRequest
        {
            StatisticName = "SurvivalTimeSeconds",
            StartPosition = 0,
            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboard(survivalTimeRequest, OnGetSurvivalTimeLeaderboardSuccess, OnGetLeaderboardFailure);
    }

    private void OnGetKillCountLeaderboardSuccess(GetLeaderboardResult result)
    {
        UpdateKillCountUI(result.Leaderboard);
    }

    private void OnGetSurvivalTimeLeaderboardSuccess(GetLeaderboardResult result)
    {
        UpdateSurvivalTimeUI(result.Leaderboard);
    }




    private void OnGetLeaderboardFailure(PlayFabError error)
    {
        Debug.LogError("Failed to get leaderboard: " + error.GenerateErrorReport());
    }
   
    private void OnGetAccountInfoFailure(PlayFabError error)
    {
        Debug.LogError("Failed to get account info: " + error.GenerateErrorReport());
    }
    private void UpdateUIWithPlayerId(string playerId)
    {
        // ���⼭ UI�� ������Ʈ�մϴ�.
        // �ʿ��� ��� currentPlayerId�� ����Ͽ� �÷��̾ ���� �߰� ������ ǥ���� �� �ֽ��ϴ�.
    }

    private string GetPlayerId()
    {
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            var request = new GetAccountInfoRequest();
            PlayFabClientAPI.GetAccountInfo(request, result =>
            {
                string username = result.AccountInfo.Username;
                Debug.Log("Username: " + username);
                // UpdateUIWithPlayerId(username);
            }, OnGetAccountInfoFailure);
        }
        else
        {
            Debug.LogError("Player is not logged in.");
        }
        return string.Empty;
    }
    private void UpdateKillCountUI(List<PlayerLeaderboardEntry> leaderboard)
    {
        string text = "Kill Count Ranking:\n";

        for (int i = 0; i < leaderboard.Count; i++)
        {
            string playerId = leaderboard[i].DisplayName;
            int killCount = leaderboard[i].StatValue;

            // ���� ���� ������� PlayFabId�� playerId�� ��ġ�Ѵٸ�, �߰����� ������ ǥ���մϴ�
         //   if (playerId == GetPlayerId())
         //   {
                text += $"<color=yellow>{leaderboard[i].Position + 1}. {playerId} - {killCount} kill</color>\n";
         //   }
        //    else
        //    {
         //       text += $"{leaderboard[i].Position + 1}. {playerId} - {killCount}\n";
          //  }
        }

        if (killCountText != null)
            killCountText.text = text;
        else
            Debug.LogError("killCountText is null.");
    }

    private void UpdateSurvivalTimeUI(List<PlayerLeaderboardEntry> leaderboard)
    {
        string text = "Survival Time Ranking:\n";
        string currentPlayerId = GetPlayerId();

        for (int i = 0; i < leaderboard.Count; i++)
        {
            string playerId = leaderboard[i].DisplayName;
            int survivalTime = leaderboard[i].StatValue;

            // ���� ���� ������� UserId�� PlayFabId�� ��ġ�Ѵٸ�, �߰����� ������ ǥ���մϴ�
           
                text += $"<color=yellow>{leaderboard[i].Position + 1}. {playerId} - {survivalTime} seconds</color>\n";
            
          //  else
          //  {
         //       text += $"{leaderboard[i].Position + 1}. {playerId} - {survivalTime}\n";
         //   }
        }
        survivalTimeText.text = text;
    }
    
   

    private void OnGetPlayerProfileSuccess(GetPlayerProfileResult result)
    {
        string playFabId = result.PlayerProfile.PlayerId;
        Debug.Log("PlayFab ID: " + playFabId);
    }

    private void OnGetPlayerProfileFailure(PlayFabError error)
    {
        Debug.LogError("Failed to get player profile: " + error.GenerateErrorReport());
    }
}